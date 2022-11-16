using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileManager
{

    public static string ReadFile(string path)
    {
        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }

        return string.Empty;
    }

    public static void CreateFile(string path)
    {
        if(!File.Exists(path))
        {
            File.Create(path);
        }
    }

    public static bool Exists(string path)
    {
        return File.Exists(path);
    }

}
