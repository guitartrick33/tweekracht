using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    private static SaveLoadManager instance;
    public List<ResultClass> results;

    public void SaveToJSON<T>(List<T> toSave, string filename)
    {
        Debug.Log(GetPath(filename));
        string content = JsonHelper.ToJson<T>(toSave.ToArray());
        WriteFile(GetPath(filename), content);
    }

    public void ReadFromJSON()
    {
        
    }

    private string GetPath(string filename)
    {
        return Application.persistentDataPath + "/" + filename;
    }

    private void WriteFile(string path, string content)
    {
        FileStream filestream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(filestream))
        {
            writer.Write(content);
        }
    }

    private string ReadFile()
    {
        return "";
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
