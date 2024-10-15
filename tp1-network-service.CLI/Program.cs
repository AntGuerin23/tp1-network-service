using tp1_network_service;
using tp1_network_service.Layers;

var path = Environment.CurrentDirectory + "/Resources";

var communicationManager = new CommunicationManager();
communicationManager.SetDataLinkPaths(new FilePaths($"{path}/L_LEC.txt", $"{path}/L_ECR.txt"));
communicationManager.SetUpperLayerPaths(new FilePaths($"{path}/S_LEC.txt", $"{path}/S_ECR.txt"));
communicationManager.StartCommunication();