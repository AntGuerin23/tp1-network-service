using tp1_network_service.Messages;

namespace tp1_network_service;

internal class MessageSerializer
{
    public Message Deserialize(byte[] rawInput)
    {
        var type = rawInput[1];
        //TODO : Créer message dépendamment du type
        return null;
    }

    public byte[] Serialize(Message message)
    {
        return Array.Empty<byte>();
    }
}