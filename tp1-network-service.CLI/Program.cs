using tp1_network_service.Layers;


var path = Environment.CurrentDirectory + "/Resources";
var networkLayer = new NetworkLayer(
    new FilePaths($"{path}/L_LEC.txt", $"{path}/L_ECR.txt"),
    new FilePaths($"{path}/T_ECR_R_LEC.txt", $"{path}/R_ECR_T_LEC.txt")
);
var transportLayer = new TransportLayer(
    new FilePaths($"{path}/S_LEC.txt", $"{path}/S_ECR.txt"),
    new FilePaths($"{path}/R_ECR_T_LEC.txt", $"{path}/T_ECR_R_LEC.txt")
);

try
{
    networkLayer.StartListening();
    transportLayer.StartListening();
}
catch (Exception e)
{
    Console.WriteLine("An error occured : " + e.Message);
    networkLayer.StopListening();
    transportLayer.StopListening();
}