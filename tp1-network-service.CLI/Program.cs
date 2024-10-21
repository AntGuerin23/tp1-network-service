// using tp1_network_service;
// using tp1_network_service.Layers;

// var path = Environment.CurrentDirectory + "/Resources";
//
// var communicationManager = new CommunicationManager();
// communicationManager.SetDataLinkPaths(new FilePaths($"{path}/L_LEC.txt", $"{path}/L_ECR.txt"));
// communicationManager.SetUpperLayerPaths(new FilePaths($"{path}/S_LEC.txt", $"{path}/S_ECR.txt"));
// communicationManager.StartCommunication();

using tp1_network_service.Builders;
using tp1_network_service.Layers.Network;
using tp1_network_service.Layers.Transport;
using tp1_network_service.Utils;

var path = Environment.CurrentDirectory + "/Resources";

NetworkLayer.Instance.SetPaths(new FilePaths($"{path}/L_LEC.txt", $"{path}/L_ECR.txt"));
TransportLayer.Instance.SetPaths(new FilePaths($"{path}/S_LEC.txt", $"{path}/S_ECR.txt"));


try
{
    NetworkLayer.Instance.Start();
    TransportLayer.Instance.Start();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    NetworkLayer.Instance.Stop();
    TransportLayer.Instance.Stop();
}