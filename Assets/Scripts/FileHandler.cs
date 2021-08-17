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

    public List<MusicMetaPair> musicMetaPairs = new List<MusicMetaPair>();
    string directoryPath;
    public Config config;

    public async Task Setup()
    {
        conductor = FindObjectOfType<Conductor>();

        directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "RhythmRedux Music");
        Directory.CreateDirectory(directoryPath);
        DeleteLoneMetaFiles();

        await LoadSongs();
        config = LoadConfig();
    }

    Config LoadConfig()
    {
        string configPath = Path.Combine(directoryPath, "config.json");

        // make config if doesn't exist
        if (!File.Exists(configPath))
        {
            SaveConfigFile(MakeConfigObject(), configPath);
        }


        // read file
        StreamReader reader = new StreamReader(configPath);
        string json = reader.ReadToEnd();
        reader.Close();

        // json to object
        Config config = JsonUtility.FromJson<Config>(json);

        return config;
    }
    void SaveConfigFile(Config config, string path)
    {
        string metaPath = path;
        string json = JsonUtility.ToJson(config);
        File.WriteAllText(metaPath, json);
    }

    Config MakeConfigObject()
    {
        Config config = new Config()
        {
            up = KeyCode.W,
            down = KeyCode.S,
            left = KeyCode.A,
            right = KeyCode.D,
            pause = KeyCode.Escape
        };
        return config;
    }

    public async Task LoadSongs()
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
