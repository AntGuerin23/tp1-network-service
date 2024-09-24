namespace tp1_network_service;

public abstract class FileAbstract
{
    
    public abstract void Write(byte[] content);

    public abstract byte[] Read();

    protected bool Exist(string fileName)
    {
        return File.Exists(fileName);
    }
}