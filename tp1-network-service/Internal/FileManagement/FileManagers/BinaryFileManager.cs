using System.Text;
using tp1_network_service.Internal.Packets;

namespace tp1_network_service.Internal.FileManagement.FileManagers;

public class BinaryFileManager : IFileManager
{
    private static readonly object _fileLock = new();
    
    public void WriteWithNewLine(byte[] content, string filePath)
    {
        if (!Exists(filePath)) throw new FileNotFoundException("Le fichier n'existe pas");
        lock (_fileLock)
        {
            using (var stream = new FileStream(filePath, FileMode.Append))
            {
                stream.Write(content, 0, content.Length);
            } 
        }

    }

    public byte[] ReadAndDeleteFirstLine(string filePath)
    {
        lock (_fileLock)
        {
            if (!Exists(filePath)) throw new FileNotFoundException($"Fichier introuvable : {filePath}");

            var bytes = File.ReadAllBytes(filePath);

            if (bytes.Length == 0) return [];
            
            var packetType = PacketDeserializer.FindActualType(bytes[1]);
            int packetLength;

            packetLength = (packetType == PacketType.Data) ? bytes[2] + 3 : GetPacketLengthFromType(packetType);
            File.WriteAllBytes(filePath, bytes.Skip(packetLength).ToArray());
            return bytes.Take(packetLength).ToArray();
        }
    }

    private static bool Exists(string fileName)
    {
        return File.Exists(fileName);
    }

    private int GetPacketLengthFromType(PacketType packetType)
    {
        return packetType switch
        {
            PacketType.ConnectRequest => 4,
            PacketType.ConnectConfirmation => 4,
            PacketType.Disconnect => 5,
            PacketType.DataAcknowledgment => 2,
            _ => 0
        };
    }
}