using tp1_network_service.Primitives.Abstract;
using tp1_network_service.Utils;

namespace tp1_network_service.Layers;

public interface ILayer
{
    public void SetPaths(FilePaths? paths);
    public void Start();
    public void Stop();
    public void HandleFromFile(byte[] data);
    public void HandleFromLayer(Primitive primitive);
}