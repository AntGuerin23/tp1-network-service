using tp1_network_service.External;

// Application properties ----------------------------------------------------------
const bool enableEdgeCases = true;
const bool readFromConsole = false;
//-----------------------------------------------------------------------------------

var path = Environment.CurrentDirectory + "/Resources";
var cancellationToken = new CancellationTokenSource();
var communicationManager = new CommunicationManager();

Start();
if (readFromConsole)
{
    PromptUserForData();
}
else
{
    Console.ReadLine();
}

Stop();
return;

void Start()
{
    new Task(() =>
        communicationManager.StartBSimulation(new FilePaths($"{path}/L_ECR.bin", $"{path}/L_LEC.bin"), cancellationToken.Token, enableEdgeCases)).Start();
    communicationManager.SetDataLinkPaths(new FilePaths($"{path}/L_LEC.bin", $"{path}/L_ECR.bin"));
    communicationManager.SetUpperLayerPaths(new FilePaths($"{path}/S_LEC.txt", $"{path}/S_ECR.txt"));
    communicationManager.StartCommunication();

    AppDomain.CurrentDomain.ProcessExit += (_, _) => Stop();
}

void PromptUserForData()
{
    Thread.Sleep(3000);
    while (true)
    {
        Console.Write("Entrez des données à envoyer (q pour quitter)  : ");
        var data = Console.ReadLine();
        File.WriteAllText($"{path}/S_LEC.txt", data);
        Thread.Sleep(1000);
        if (data == "q") return;
    } 
}

void Stop()
{
    communicationManager.Stop();
    cancellationToken.Cancel();
}
