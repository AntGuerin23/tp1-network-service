using System.Text;

namespace tp1_network_service;

public class FileManager (string filePath)
{
    public void Write(byte[] content)
    {
        try
        {
            if (Exist(filePath))
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
    
    public byte[] Read()
    {
        try
        {
            if (Exist(filePath))
            {
                var line = File.ReadLines(filePath).FirstOrDefault();
                if (line != null)
                {
                    DeleteFirstLine();
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

    private bool Exist(string fileName)
    {
        return File.Exists(fileName);
    }
    
    private void DeleteFirstLine()
    {
        var lines = File.ReadAllLines(filePath);

        if (lines.Length < 1) return;
        var newLines = new string[lines.Length - 1];
        Array.Copy(lines, 1, newLines, 0, newLines.Length);

        File.WriteAllLines(filePath, newLines);
    }
}