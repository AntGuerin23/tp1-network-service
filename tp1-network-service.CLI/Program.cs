using tp1_network_service.Layers;


var path = Environment.CurrentDirectory + "/Resources";
// var networkLayer = new NetworkLayer(
//     new FilePaths($"{path}/L_LEC.txt", $"{path}/L_ECR.txt"),
//     new FilePaths($"{path}/T_ECR_R_LEC.txt", $"{path}/R_ECR_T_LEC.txt")
// );
// var transportLayer = new TransportLayer(
//     new FilePaths($"{path}/S_LEC.txt", $"{path}/S_ECR.txt"),
//     new FilePaths($"{path}/R_ECR_T_LEC.txt", $"{path}/T_ECR_R_LEC.txt")
// );

NetworkLayer.Instance.DataLinkPaths = new FilePaths($"{path}/L_LEC.txt", $"{path}/L_ECR.txt");
TransportLayer.Instance.UpperLayerPaths = new FilePaths($"{path}/S_LEC.txt", $"{path}/S_ECR.txt");

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