using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AudioGet
{
    // 音频列表
    class AudioList
    {
        public Dictionary<string, AudioDetailInfo> AudioDict { get; set; } = new Dictionary<string, AudioDetailInfo>();

        public void Add(string id, AudioDetailInfo info)
        {
            AudioDict.Add(id, info);
        }

        public void Del(int index)
        {
            AudioDict.Remove(AudioDict.ElementAt(index).Key);
        }

        public void DelAll()
        {
            AudioDict = new Dictionary<string, AudioDetailInfo>();
        }
    }

    // 音频详情类
    class AudioDetailInfo
    {
        // 音频名称
        public string name;
        // 音频作者
        public string artist;
        // 所属合辑
        public string collection;

        public AudioDetailInfo(string name, string artist, string collection)
        {
            this.name = name;
            this.artist = artist;
            this.collection = collection;
        }

        // 返回当前音频的详细信息
        public string Info()
        {
            return collection + " / " + name + " / " + artist;
        }
    }
}
