using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Utils;

internal class Deserializer
{
    public static Packet DeserializePacket(byte[] rawInput)
    {
        // var builder = new MessageBuilder();
        //
        // var type = FindType(rawInput[1]);
        //
        // builder
        //     .SetConnectionNumber(rawInput[0])
        //     .SetType(type)
        //     .SetSource(rawInput[2])
        //     .SetDestination(rawInput[3]);
        
        //TODO : validation

        // switch (type)
        // {
        //     case PrimitiveType.ConnectRequest :
        //         builder
        //             .SetPrimitive(MessagePrimitive.Req)
        //             .ToConnectMessage();
        //         break;
        //     
        //     case PrimitiveType.ConnectConfirmation :
        //         builder
        //             .SetPrimitive(messageWasReceivedByTransportLayer ? MessagePrimitive.Conf : MessagePrimitive.Resp)
        //             .ToConnectMessage();
        //         break;
        //     
        //     case PrimitiveType.Disconnect :
        //         var reason  = rawInput[4];
        //         builder
        //             .SetPrimitive(messageWasReceivedByTransportLayer ? MessagePrimitive.Ind : MessagePrimitive.Req)
        //             .ToDisconnectMessage((DisconnectReason) reason);
        //         break;
        //     
        //     case PrimitiveType.Data:
        //         var data = rawInput[5..];
        //         builder
        //             .SetPrimitive(MessagePrimitive.Req)
        //             .ToDataMessage(new SegmentationInfo(rawInput[1]), data);
        //         break;
        // }
        
        //return builder.GetResult();
        throw new NotImplementedException();
    }

}