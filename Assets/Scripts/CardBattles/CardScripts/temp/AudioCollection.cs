using System;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class AudioCollection : MonoBehaviour
{
    [ResizableTextArea]
    public string json = @"
    {
        ""A"": {
            ""B"": ""Sound1"",
            ""C"": ""Sound2""
        },
        ""D"": ""Sound3""
    }";
    private string jsonFilePath;
    private string jsonBackupPath;

    public static AudioCollection Instance;
    
    
    public List<AudioEntry> audioMap = new List<AudioEntry>();

    private void Awake() {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
        
        jsonFilePath = Path.Combine(Application.persistentDataPath, "audioMap.json");
        string todayString = DateTime.Now.ToString("yyyy-MM-dd");
        string backupAudioFileName = "backupAudi_" + todayString + ".json";
        jsonBackupPath = Path.Combine(Application.persistentDataPath, "backupAudioMap.json");
        LoadJsonFromFile();

    }


    void Start()
    {
        DoLoad();
    }
    public void DoLoad() {
        var result = FlattenJson(JObject.Parse(json));

        audioMap.Clear(); 
        foreach (var entry in result)
        {
            AudioClip clip = Resources.Load<AudioClip>(entry.Value);
            audioMap.Add(new AudioEntry { key = entry.Key, clip = clip });

            if (clip != null)
            {
                Debug.Log($"{entry.Key} : Loaded AudioClip {entry.Value}");
            }
            else
            {
                Debug.LogWarning($"AudioClip '{entry.Value}' could not be found for key '{entry.Key}'");
            }
        }
    }
    public void OverwriteJson()
    {
        var nestedDictionary = new Dictionary<string, object>();

        foreach (var entry in audioMap)
        {
            AddToNestedDictionary(nestedDictionary, entry.key.Split('.'), entry.clip ? entry.clip.name : "");
        }

        json = JsonConvert.SerializeObject(nestedDictionary, Formatting.Indented);
        Debug.Log("JSON overwritten with current audio map:\n" + json);
        
        SaveJsonToFile();
    }
    private void AddToNestedDictionary(Dictionary<string, object> dict, string[] keys, string value)
    {
        var currentDict = dict;
        for (int i = 0; i < keys.Length - 1; i++)
        {
            if (!currentDict.ContainsKey(keys[i]))
            {
                currentDict[keys[i]] = new Dictionary<string, object>();
            }

            currentDict = currentDict[keys[i]] as Dictionary<string, object>;
        }

        currentDict[keys[keys.Length - 1]] = value;
    }
     static Dictionary<string, string> FlattenJson(JObject json, string parentKey = "")
    {
        var result = new Dictionary<string, string>();

        foreach (var property in json.Properties())
        {
            string currentKey = string.IsNullOrEmpty(parentKey) ? property.Name : $"{parentKey}.{property.Name}";

            if (property.Value is JObject childObject)
            {
                var childResult = FlattenJson((JObject)property.Value, currentKey);
                foreach (var child in childResult)
                {
                    result.Add(child.Key, child.Value);
                }
            }
            else
            {
                result[currentKey] = property.Value.ToString();
            }
        }

        return result;
    }
    public AudioClip GetClip(string key)
    {
        // Try to find the audio entry by the key
        var entry = audioMap.Find(e => e.key.Equals(key, StringComparison.OrdinalIgnoreCase));
        if (entry != null)
        {
            return entry.clip;
        }

        // If the entry is not found, add a new one with a null AudioClip
        var newEntry = new AudioEntry { key = key, clip = null };
        audioMap.Add(newEntry);
        Debug.Log($"Added new key '{key}' to the audio map with a null AudioClip. You can assign a clip later.");

        return null;
    }
    public void SaveJsonToFile()
    {
        try
        {
            File.WriteAllText(jsonFilePath, json);
            Debug.Log($"JSON saved to {jsonFilePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save JSON to file: {e.Message}");
        }
    }
    public void SaveJsonToBackupFile()
    {
        try
        {
            File.WriteAllText(jsonFilePath, json);
            Debug.Log($"JSON saved to {jsonFilePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save JSON to file: {e.Message}");
        }
    }
    private void LoadJsonFromFile()
    {
        if (File.Exists(jsonFilePath))
        {
            try
            {
                json = File.ReadAllText(jsonFilePath);
                Debug.Log($"Loaded JSON from {jsonFilePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load JSON from file: {e.Message}");
            }
        }
        else
        {
            Debug.Log("No saved JSON file found; using default JSON.");
        }
    }
}

