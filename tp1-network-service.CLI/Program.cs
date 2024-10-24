using tp1_network_service.External;

var path = Environment.CurrentDirectory + "/Resources";
var enableEdgeCases = false;
var cancellationToken = new CancellationTokenSource();
var communicationManager = new CommunicationManager();


Start();
WriteData();
Stop();

void Start()
{
    new Task(() =>
        communicationManager.StartBSimulation(new FilePaths($"{path}/L_ECR.txt", $"{path}/L_LEC.txt"), cancellationToken.Token, enableEdgeCases)).Start();
    communicationManager.SetDataLinkPaths(new FilePaths($"{path}/L_LEC.txt", $"{path}/L_ECR.txt"));
    communicationManager.SetUpperLayerPaths(new FilePaths($"{path}/S_LEC.txt", $"{path}/S_ECR.txt"));
    communicationManager.StartCommunication();

    AppDomain.CurrentDomain.ProcessExit += (_, _) => Stop();
}

void WriteData()
{
    Thread.Sleep(3000);
    while (true)
    {
        Console.Write("Entrez des données à envoyer (q pour quitter)  : ");
        var data = Console.ReadLine();
        File.WriteAllText($"{path}/S_LEC.txt", data);
        Thread.Sleep(500);
        if (data == "q") return;
    } 
}

void Stop()
{
    communicationManager.Stop();
    cancellationToken.Cancel();
}
