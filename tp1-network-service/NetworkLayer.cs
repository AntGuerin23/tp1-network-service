using tp1_network_service.Messages;

namespace tp1_network_service;

internal class NetworkLayer : Layer
{
    public override void StartListening(string filesPath)
    {
        
    }

    internal override void HandleNewMessage(Message message)
    {
        //TODO : Faudrait faire du polymorphisme avec message, créer genre fonction Message.HandleNetwork qui est override dans chacun des enfants avec un comportement différent
        //sinon faut faire un switch...
    }
}