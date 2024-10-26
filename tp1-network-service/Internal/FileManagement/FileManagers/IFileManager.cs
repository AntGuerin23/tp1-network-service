using System.Text;

namespace tp1_network_service.Internal.FileManagement.FileManagers;

internal interface IFileManager
{
    public void WriteWithNewLine(byte[] content, string filePath);

    public byte[] ReadAndDeleteFirstLine(string filePath);
}