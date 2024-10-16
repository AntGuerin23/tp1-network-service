using System.Text;

namespace tp1_network_service.Utils;

internal class FileManager ()
{
    public static void Write(byte[] content, string filePath)
    {
        try
        {
            if (Exists(filePath))
            {
                var instruction = Encoding.UTF8.GetString(content, 0, content.Length);
                File.AppendAllText(filePath, string.Format("{0}{1}", instruction, Environment.NewLine));
                return;
            }
            
            throw new Exception();
        }
        catch (Exception e)
        {
            Console.WriteLine("Une erreur en survenue lors de l'écriture");
            throw;
        }
    }
    
    public static byte[] Read(string filePath)
    {
        try
        {
            if (Exists(filePath))
            {
                var line = File.ReadLines(filePath).FirstOrDefault();
                if (line != null)
                {
                    DeleteFirstLine(filePath);
                    return Encoding.UTF8.GetBytes(line);
                }
                return [];
            }

            Console.WriteLine($"Aucune action {filePath}");
            return [];
        }
        catch (Exception e)
        {
            Console.WriteLine("Une erreur en survenue lors de la lecture");
            return [];
        }
    }

    private static bool Exists(string fileName)
    {
        return File.Exists(fileName);
    }
    
    private static void DeleteFirstLine(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        if (lines.Length < 1) return;
        var newLines = new string[lines.Length - 1];
        Array.Copy(lines, 1, newLines, 0, newLines.Length);

        File.WriteAllLines(filePath, newLines);
    }
}