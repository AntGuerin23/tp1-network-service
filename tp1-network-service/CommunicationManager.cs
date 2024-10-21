using tp1_network_service.Layers;
using tp1_network_service.Layers.Network;
using tp1_network_service.Layers.Transport;

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
            TransportLayer.Instance.Stop();
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occured : " + e.Message);
            NetworkLayer.Instance.Start();
            TransportLayer.Instance.Stop();
        } 
    }
}