using tp1_network_service.Packets;
using tp1_network_service.Primitives;

namespace tp1_network_service;

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

    public static Primitive DeserializePrimitive(byte[] rawPrimitive)
    {
        throw new NotImplementedException();
    }

    // private static PrimitiveType FindType(byte typeByte)
    // {
    //     var type = (PrimitiveType) typeByte;
    //     return Enum.IsDefined(type) ? type : PrimitiveType.Data; //Data has a variable "type" byte, so if the enum doesn't find a match, it must be a data packet
    // }
}