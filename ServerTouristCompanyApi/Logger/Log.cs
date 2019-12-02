using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public static class Log
{
    private static string dbLogPath;
    private static string webLogPath;
    static Log()
    {
        webLogPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\serverWebLog.txt";
        dbLogPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\serverDbLogPath.txt";
    }

    public static void WriteWebLog(string data)
    {
        File.AppendAllTextAsync(webLogPath, DateTime.Now.ToString() + ": " + data + Environment.NewLine);
    }

    public static void WriteDBLog(string data)
    {

        File.AppendAllTextAsync(dbLogPath, DateTime.Now.ToString() + ": " + data + Environment.NewLine);
    }
}
