using tp1_network_service.Layers;

namespace tp1_network_service;

public class CommunicationManager
{
    public void SetUpperLayerPaths(FilePaths paths)
    {
        TransportLayer.Instance.UpperLayerPaths = paths;
    }

    public void SetDataLinkPaths(FilePaths paths)
    {
        NetworkLayer.Instance.DataLinkPaths = paths;
    }

    public void StartCommunication()
    {
        try
        {
            NetworkLayer.Instance.StartListening();
            TransportLayer.Instance.StartListening();
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occured : " + e.Message);
            NetworkLayer.Instance.StopListening();
            TransportLayer.Instance.StopListening();
        } 
    }
}