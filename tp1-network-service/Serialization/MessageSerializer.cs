using tp1_network_service.Enums;
using tp1_network_service.Messages;
using tp1_network_service.Messages.Builder;

namespace tp1_network_service.Serialization;

internal class MessageSerializer
{
    public static Message Deserialize(byte[] rawInput, bool messageWasReceivedByTransportLayer)
    {
        var builder = new MessageBuilder();

        var type = FindType(rawInput[1]);

        builder
            .SetConnectionNumber(rawInput[0])
            .SetType(type)
            .SetSource(rawInput[2])
            .SetDestination(rawInput[3]);
        
        //TODO : validation

        switch (type)
        {
            case MessageType.ConnectRequest :
                builder
                    .SetPrimitive(MessagePrimitive.Req)
                    .ToConnectMessage();
                break;
            
            case MessageType.ConnectConfirmation :
                builder
                    .SetPrimitive(messageWasReceivedByTransportLayer ? MessagePrimitive.Conf : MessagePrimitive.Resp)
                    .ToConnectMessage();
                break;
            
            case MessageType.Disconnect :
                var reason  = rawInput[4];
                builder
                    .SetPrimitive(messageWasReceivedByTransportLayer ? MessagePrimitive.Ind : MessagePrimitive.Req)
                    .ToDisconnectMessage((DisconnectReason) reason);
                break;
            
            case MessageType.Data:
                var data = rawInput[5..];
                builder
                    .SetPrimitive(MessagePrimitive.Req)
                    .ToDataMessage(new SegmentationInfo(rawInput[1]), data);
                break;
        }
        
        return builder.GetResult();
    }

    public static byte[] Serialize(Message message)
    {
        return Array.Empty<byte>();
    }

    private static MessageType FindType(byte typeByte)
    {
        var type = (MessageType) typeByte;
        return Enum.IsDefined(type) ? type : MessageType.Data; //Data has a variable "type" byte, so if the enum doesn't find a match, it must be a data packet
    }
}