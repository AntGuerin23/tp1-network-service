using tp1_network_service.External;
using tp1_network_service.Internal.Primitives.Abstract;

namespace tp1_network_service.Internal.Layers;

internal interface ILayer
{
    public void SetPaths(FilePaths? paths);
    public void Start();
    public void Stop();
    public void HandleFromFile(byte[] data);
    public void HandleFromLayer(Primitive primitive);
}