﻿using System.Collections.Generic;
using System.IO;

public class FileUtil
{
    public readonly string[] vedioPattern = { "*.flv", "*.mp4", "*.mov", "*.qt", "*.avi" };
    public readonly string[] imagePattern = { "*.png", "*.jpg", "*.jpeg", "*.bmp" };
    public static FileInfo[] getVedios(string directory)
    {
        return getFiles(directory, "*.flv", "*.mp4", "*.mov", "*.qt", "*.avi");
    }
    public static FileInfo[] getExcel(string directory)
    {
        return getFiles(directory, "*.json");
    }
    private static FileInfo[] getFiles(string directory, params string[] pattern)
    {
        List<FileInfo> list = new List<FileInfo>();
        if (Directory.Exists(directory))
        {
            DirectoryInfo direction = new DirectoryInfo(directory);
            foreach (string ptn in pattern)
            {
                FileInfo[] files = direction.GetFiles(ptn);
                foreach (FileInfo fi in files)
                {
                    list.Add(fi);
                }
            }
        }
        return list.ToArray();
    }
    public static string storeFile(string fullpath, string rootPath)
    {
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }

        FileInfo fi = new FileInfo(fullpath);
        fi.CopyTo(rootPath + "/" + fi.Name);
        return rootPath + "/" + fi.Name;
    }
}