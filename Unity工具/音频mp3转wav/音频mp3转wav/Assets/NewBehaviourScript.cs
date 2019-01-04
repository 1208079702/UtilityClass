using System.Collections;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    
	// Use this for initialization
	void Start ()
	{
	    string path = @"D:\Desktop\常规项目\魔墙\05_上海站人才魔墙\资料\软件资料\0劳模\2017六十佳\六十佳录音";
        DirectoryInfo info = new DirectoryInfo(path);
	    foreach (FileInfo fileInfo in info.GetFiles())
	    {
	        if (fileInfo.Extension != ".meta")
	        {
	            string name = fileInfo.FullName.Replace(".mp3", ".wav");
	            StartCoroutine(LoadMusic(fileInfo.FullName, name));
	        }
	    }
        print("完成");
        //foreach (DirectoryInfo directoryInfo in info.GetDirectories())
        //{
        //    foreach (FileInfo fileInfo in directoryInfo.GetFiles())
        //    {
        //        if (fileInfo.Extension != ".meta")
        //        {
        //            string name = fileInfo.FullName.Replace(".mp3", ".wav");
        //            StartCoroutine(LoadMusic(fileInfo.FullName, name));
        //        }
        //    }
        //}
       // StartCoroutine(LoadMusic(Application.streamingAssetsPath + @"/上南行包音频/吕全玺.MP3", @""));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private IEnumerator LoadMusic(string filepath, string savepath)
    {
        var stream = File.Open(filepath, FileMode.Open);
        var reader = new Mp3FileReader(stream);
        WaveFileWriter.CreateWaveFile(savepath, reader);
        var www = new WWW("file://" + savepath);
        yield return www;
    }
}
