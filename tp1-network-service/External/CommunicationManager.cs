using tp1_network_service.Internal.Layers;
using tp1_network_service.Internal.Layers.Network;
using tp1_network_service.Internal.Layers.Transport;

namespace tp1_network_service;

public class CommunicationManager
{
    public void SetUpperLayerPaths(FilePaths paths)
    {
        TransportLayer.Instance.SetPaths(paths);
    }

    public void SetDataLinkPaths(FilePaths paths)
    {
        NetworkLayer.Instance.SetPaths(paths);
    }

    public void StartCommunication()
    {
        try
        {
            NetworkLayer.Instance.Start();
            TransportLayer.Instance.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occured : " + e.Message);
            NetworkLayer.Instance.Stop();
            TransportLayer.Instance.Stop();
        } 
    }
}