using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AudioGet
{
    class AppService
    {
        public delegate void UpdateStatus();
        public event UpdateStatus Update;

        private readonly ApiClient ApiClient = new ApiClient();
        private readonly AudioList AudioList = new AudioList();

        public string DownloadStatus { get; set; } = "";
        public int DownloadProgress { get; set; } = 0;

        static readonly char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

        private async Task<int> DownloadAudio(string audioId, AudioDetailInfo audioDetailInfo, string basePath, string index)
        {
            string endpoint = "http://api.bilibili.com/audio/music-service-c/url?mid=1&mobi_app=iphone&platform=ios&privilege=2&quality=2&songid=";
            string audioInfoApiLink = endpoint + audioId;
            string audioInfoJson = await ApiClient.GetFromUrl(audioInfoApiLink);

            JObject audioInfo = JObject.Parse(audioInfoJson);

            if (audioInfo["code"].ToString() == "0")
            {
                string downloadLink = audioInfo["data"]["cdns"][0].ToString();

                string i = "";

                // 不合法路径转换
                string validCollectionName = new string(audioDetailInfo.collection.Select(ch => invalidFileNameChars.Contains(ch) ? '_' : ch).ToArray());
                string validAudioName = new string(audioDetailInfo.name.Select(ch => invalidFileNameChars.Contains(ch) ? '_' : ch).ToArray());

                if (!Directory.Exists(basePath + validCollectionName + "/"))
                {
                    Directory.CreateDirectory(basePath + validCollectionName + "/");
                }

                string path = basePath + validCollectionName + "/" + validAudioName + i + ".m4a";
                if (File.Exists(path))
                {
                    Console.WriteLine("SKIP: " + path + index);
                    DownloadStatus = "[SKIP] " + audioDetailInfo.Info() + index;
                    Update();
                }
                else
                {
                    Console.WriteLine("DOWNLOAD: " + path + index);
                    DownloadStatus = "[DOWNLOAD] " + path + index;
                    Update();
                    await ApiClient.DownloadFormUrl(downloadLink, path);
                }
            }
            else
            {
                return 404;
            }
            return 200;
        }

        public async Task<bool> DownloadAll(string basePath)
        {
            try
            {
                int Status = 0;
                Dictionary<string, AudioDetailInfo> dict = AudioList.AudioDict;
                int i = 1;
                int all = dict.Count();
                string downloadBasePath = basePath + "/Music/";

                if (!Directory.Exists(downloadBasePath))
                {
                    Directory.CreateDirectory(downloadBasePath);
                }

                foreach (var item in dict)
                {
                    string index = " ( " + i.ToString() + " / " + all.ToString() + " )";
                    string id = item.Key;
                    AudioDetailInfo songDetailInfo = item.Value;
                    DownloadStatus = "[START] " + songDetailInfo.Info() + index;
                    Update();
                    Status = await DownloadAudio(id, songDetailInfo, downloadBasePath, index);

                    if (Status == 200)
                    {
                        DownloadStatus = "[COMPLETE] " + songDetailInfo.Info() + index;
                    }
                    else if (Status == 404)
                    {
                        DownloadStatus = "[ERROR] " + songDetailInfo.Info() + " has been removed." + index;
                    }
                    else if (Status == 503)
                    {
                        DownloadStatus = "[ERROR] " + index;
                    }
                    DownloadProgress = (int)(((double)i / (double)all) * 100);
                    Update();
                    i = i + 1;
                }
                return true;
            }
            catch (Exception e)
            {
                DownloadStatus = e.Message;
                Update();
                return false;
            }
        }

        public async Task<string> GetSingleAudioInfo(string audioId)
        {
            string endpoint = "https://www.bilibili.com/audio/music-service-c/web/song/info?sid=";
            string apiLink = endpoint + audioId;
            string singleAudioInfoJson = await ApiClient.GetFromUrl(apiLink);
            string audioName = "";
            JObject singleAudioInfo = JObject.Parse(singleAudioInfoJson);
            if (singleAudioInfo["code"].ToString() == "0")
            {
                audioName = singleAudioInfo["data"]["title"].ToString();
                string artist = singleAudioInfo["data"]["author"].ToString();
                if (!AudioList.AudioDict.ContainsKey(audioId))
                {
                    AudioList.Add(audioId, new AudioDetailInfo(audioName, artist, "Single"));
                    audioName = "Single / " + audioName + " / " + artist;
                    return audioName;
                }
            }
            else
            {
                DownloadStatus = "[ERROR] Audio Not Found";
                Update();
            }
            return "";
        }

        public async Task<List<string>> GetPlayListInfo(string playListId)
        {
            string endpoint = "https://www.bilibili.com/audio/music-service-c/web/menu/info?sid=";
            string apiLink = endpoint + playListId;
            string playListInfoJson = await ApiClient.GetFromUrl(apiLink);
            JObject playListInfo = JObject.Parse(playListInfoJson);
            if (playListInfo["code"].ToString() == "0")
            {
                string title = playListInfo["data"]["title"].ToString();
                DownloadStatus = "[ADD] PlayList: " + title;
            }
            else if (playListInfo["code"].ToString() == "72000000")
            {
                DownloadStatus = "[INFO] PlayList Not Found";
            }
            Update();

            List<string> audioNameList = new List<string>();
            string playListDetailEndpoint = "https://www.bilibili.com/audio/music-service-c/web/song/of-menu?pn=1&ps=1000&sid=";
            string playListDetailLink = playListDetailEndpoint + playListId;
            string playListDetailJson = await ApiClient.GetFromUrl(playListDetailLink);
            JObject playListDetail = JObject.Parse(playListDetailJson);

            if (playListDetail["code"].ToString() == "0")
            {
                int count = playListDetail["data"]["data"].Count();
                for (int i = 0; i < count; i++)
                {
                    string audioId = playListDetail["data"]["data"][i]["id"].ToString();
                    string audioName = playListDetail["data"]["data"][i]["title"].ToString();
                    string audioArtist = playListDetail["data"]["data"][i]["author"].ToString();

                    string audioInfoEndpoint = "https://www.bilibili.com/audio/music-service-c/web/menu/6?sid=";
                    string audioInfoLink = audioInfoEndpoint + audioId;
                    string audioInfoJson = await ApiClient.GetFromUrl(audioInfoLink);

                    string collectionName = "Unknown";
                    JObject audioInfo = JObject.Parse(audioInfoJson);
                    if (audioInfo["code"].ToString() == "0")
                    {
                        // 尝试取得音频所属合辑
                        if (audioInfo["data"].Type == JTokenType.Null)
                        {
                            // 无法取得的情况使用老 API 取得
                            Console.WriteLine("Get audio info from old API.");
                            string oldAudioInfoEndpoint = "https://www.bilibili.com/audio/music-service-c/web/song/info?sid=";
                            string oldAudioInfoApiLink = oldAudioInfoEndpoint + audioId;
                            string oldAudioInfoJson = await ApiClient.GetFromUrl(oldAudioInfoApiLink);
                            JObject oldAudioInfo = JObject.Parse(oldAudioInfoJson);
                            if (oldAudioInfo["code"].ToString() == "0")
                            {
                                collectionName = oldAudioInfo["data"]["title"].ToString();
                            }
                        }
                        else
                        {
                            collectionName = audioInfo["data"]["title"].ToString();
                        }
                    }

                    if (AudioList.AudioDict.ContainsKey(audioId) == false)
                    {
                        Console.WriteLine("ADD： " + audioId + " / " + collectionName + " / " + audioName + " / " + audioArtist);
                        DownloadStatus = "[ADD] " + audioId + " / " + collectionName + " / " + audioName + " / " + audioArtist;
                        Update();
                        audioNameList.Add(audioId + " / " + collectionName + " / " + audioName + " / " + audioArtist);
                        AudioDetailInfo info = new AudioDetailInfo(audioName, audioArtist, collectionName);
                        AudioList.Add(audioId, info);
                    }
                }
            }
            return audioNameList;
        }

        public void RemoveAll()
        {
            AudioList.DelAll();
        }

        public void Remove(int index)
        {
            AudioList.Del(index);
        }
    }
}
