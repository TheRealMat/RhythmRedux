using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class FileHandler : MonoBehaviour
{
    Conductor conductor;

    List<MusicMetaPair> musicMetaPairs = new List<MusicMetaPair>();

    // game should wait for this to be done
    async void Start()
    {
        conductor = FindObjectOfType<Conductor>();

        var directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RhythmRedux Music");
        Directory.CreateDirectory(directoryPath);

        await LoadSongs(directoryPath);
        DeleteLoneMetaFiles();


        //debug
        conductor.musicMetaPair = musicMetaPairs[0];
        conductor.StartMusic();
    }

    async Task LoadSongs(string directoryPath)
    {
        string[] musicPaths = Directory.GetFiles(directoryPath, "*.wav");

        // load files into memory
        foreach (string path in musicPaths)
        {
            AudioClip clip = await LoadClip(path);


            MusicMeta meta;

            // get or create meta file
            if (File.Exists(path + "json"))
            {
                meta = GetMeta(path);
            }
            else
            {
                meta = MakeMetaObject();
                SaveMetaFile(meta, path);

            }

            MusicMetaPair musicMetaPair = new MusicMetaPair() { music = clip, meta = meta };

            musicMetaPairs.Add(musicMetaPair);
        }
    }

    MusicMeta GetMeta(string path)
    {
        string metaPath = path + ".json";

        // read file
        StreamReader reader = new StreamReader(metaPath);
        string json = reader.ReadToEnd();
        reader.Close();

        // json to object
        MusicMeta meta = JsonUtility.FromJson<MusicMeta>(json);

        return meta;

    }

    void DeleteLoneMetaFiles()
    {
        // this should delete meta files that don't share name with a music file
    }

    MusicMeta MakeMetaObject()
    {
        MusicMeta song = new MusicMeta();
        song.BPM = 105;
        song.firstBeatOffset = 0;
        return song;
    }
    void SaveMetaFile(MusicMeta levelMusic, string path)
    {
        string metaPath = path + ".json";
        string json = JsonUtility.ToJson(levelMusic);
        File.WriteAllText(metaPath, json);
    }

    async Task<AudioClip> LoadClip(string path)
    {
        AudioClip clip = null;
        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            uwr.SendWebRequest();

            // wrap tasks in try/catch, otherwise it'll fail silently
            try
            {
                while (!uwr.isDone) await Task.Delay(5);

                if (uwr.result == UnityWebRequest.Result.ProtocolError || uwr.result == UnityWebRequest.Result.ConnectionError) Debug.Log($"{uwr.error}");
                else
                {
                    clip = DownloadHandlerAudioClip.GetContent(uwr);
                }
            }
            catch (Exception err)
            {
                Debug.Log($"{err.Message}, {err.StackTrace}");
            }
        }

        return clip;
    }
}