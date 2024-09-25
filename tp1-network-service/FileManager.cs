using System.Text;

namespace tp1_network_service;

public class FileManager
{
    private string file { get; set; }
    private string path = Environment.CurrentDirectory + "/Resources/";

    public FileManager(string file)
    {
        this.file = file;
    }

    public void Write(byte[] content)
    {
        try
        {
            if (Exist(path + file))
            {
                var instruction = Encoding.UTF8.GetString(content, 0, content.Length);
                File.AppendAllText(path + file, string.Format("{0}{1}", instruction, Environment.NewLine));
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
            if (Exist(path + file))
            {
                var line = File.ReadLines(path + file).FirstOrDefault();
                if (line != null)
                {
                    DeleteFirstLine();
                    return Encoding.UTF8.GetBytes(line);
                }
                return new byte[] { };
            }

            Console.WriteLine("Aucune action");
            return new byte[] { };
        }
        catch (Exception e)
        {
            Console.WriteLine("Une erreur en survenue lors de la lecture");
            return null;
        }
    }

    private bool Exist(string fileName)
    {
        return File.Exists(fileName);
    }
    
    private void DeleteFirstLine()
    {
        string[] lines = File.ReadAllLines(path + file);

        if (lines.Length >= 1)
        {
            string[] newLines = new string[lines.Length - 1];
            Array.Copy(lines, 1, newLines, 0, newLines.Length);

            File.WriteAllLines(path + file, newLines);
        }
    }
}