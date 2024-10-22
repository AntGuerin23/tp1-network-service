using tp1_network_service;
using tp1_network_service.External;
using tp1_network_service.Internal.Layers;

var path = Environment.CurrentDirectory + "/Resources";
var enableEdgeCases = true;

var communicationManager = new CommunicationManager();
communicationManager.StartBSimulation(new FilePaths($"{path}/L_ECR.txt", $"{path}/L_LEC.txt"), enableEdgeCases);
communicationManager.SetDataLinkPaths(new FilePaths($"{path}/L_LEC.txt", $"{path}/L_ECR.txt"));
communicationManager.SetUpperLayerPaths(new FilePaths($"{path}/S_LEC.txt", $"{path}/S_ECR.txt"));
communicationManager.StartCommunication();