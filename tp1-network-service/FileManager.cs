using System.Net.Mime;
using System.Text;

namespace tp1_network_service.Messages;

public class FileManager: FileAbstract
{
    private string path = Directory.GetCurrentDirectory() + "/File/";
    private string fileInput;
    private string fileOutput;

    public FileManager(string input, string output)
    {
        this.fileInput = input;
        this.fileOutput = output;
    }
    
    public override void Write(byte[] content)
    {
        try
        {
            if (Exist(path + fileInput))
            {
                var instruction = Encoding.UTF8.GetString(content, 0, content.Length);
                File.AppendAllText(path + fileInput, string.Format("{0}{1}", instruction, Environment.NewLine));
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

    public override byte[] Read()
    {
        try
        {
            if (Exist(path + fileOutput))
            {
                var line = File.ReadLines(path + fileOutput).FirstOrDefault();
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

    private void DeleteFirstLine()
    {
        string[] lines = File.ReadAllLines(path + fileOutput);

        if (lines.Length >= 1)
        {
            string[] newLines = new string[lines.Length - 1];
            Array.Copy(lines, 1, newLines, 0, newLines.Length);

            File.WriteAllLines(path + fileOutput, newLines);
        }
    }
}