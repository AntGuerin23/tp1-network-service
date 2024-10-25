using System.Text;
using System.IO;

namespace tp1_network_service.Internal.FileManagement;

internal class FileManager
{
    public const bool EnableLogging = false;
    private static readonly object _fileLock = new();
    
    private static int _newLineLength = Environment.NewLine.Length;
    
    public static void WriteWithNewLine(byte[] content, string filePath)
    {
        if (!Exists(filePath)) throw new FileNotFoundException("Le fichier n'existe pas");
        Log(content, filePath);
        lock (_fileLock)
        {
            using (var stream = new FileStream(filePath, FileMode.Append))
            {
                stream.Write(content, 0, content.Length);
                stream.Write(Encoding.ASCII.GetBytes(Environment.NewLine));
            } 
        }

    }

    private static void Log(byte[] content, string filePath)
    {
        if (!EnableLogging) return;
        var binaryString = string.Join("\n", content.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        Console.WriteLine($"TO {filePath} :\n{binaryString}");
    }

    public static byte[] ReadAndDeleteFirstLineOfFile(string filePath)
    {
        lock (_fileLock)
        {
            if (!Exists(filePath)) throw new FileNotFoundException($"Fichier introuvable : {filePath}");

            var bytes = File.ReadAllBytes(filePath);

            if (bytes.Length <= 0) return [];
            var newLineIndex = FindNewLineIndex(bytes);

            if (newLineIndex != 0)
            {
                File.WriteAllBytes(filePath, bytes.Skip(newLineIndex + 1).ToArray());
                return bytes.Take(newLineIndex - (_newLineLength - 1)).ToArray();
            }

            File.WriteAllText(filePath, null);
            return bytes;
        }
    }

    private static bool Exists(string fileName)
    {
        return File.Exists(fileName);
    }

    private static int FindNewLineIndex(byte[] bytes)
    {
        for (var i = 0; i < bytes.Length; i++)
        {
            if (bytes[i] == '\n')
            {
                return i;
            }
        }
        return 0;
    }
}