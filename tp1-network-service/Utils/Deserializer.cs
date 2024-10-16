using tp1_network_service.Builder;
using tp1_network_service.Enums;
using tp1_network_service.Packets;
using tp1_network_service.Packets.Abstract;
using static tp1_network_service.Packets.PacketType;

namespace tp1_network_service.Utils;

internal class Deserializer
{
    public static Packet DeserializePacket(byte[] rawInput)
    {
         var builder = new PacketBuilder();
         var type = FindActualType(rawInput[1]);

         builder
             .SetConnectionNumber(rawInput[0])
             .SetType(type);
        
        //TODO : validation

         switch (type)
         {
             case ConnectConfirmation:
                 builder.SetSourceAddress(rawInput[2])
                     .SetDestinationAddress(rawInput[3]);
                 return builder.ToConnectionConfirmationPacket();
             case DataAcknowledgment:
                 builder.SetSegmentationInfo(new SegmentationInfo(rawInput[1]));
                 return builder.ToDataAcknowledgmentPacket();
             case Disconnect:
                 builder.SetSourceAddress(rawInput[2])
                     .SetDestinationAddress(rawInput[3])
                     .SetReason((DisconnectReason) rawInput[4]);
                 return builder.ToDisconnectPacket();
             case ConnectRequest:
                 throw new UserBNotImplementedException();
             case Data:
                 throw new UserBNotImplementedException();
             default:
                 throw new ArgumentOutOfRangeException();
         }
    }

    private static PacketType FindActualType(byte typeByte)
    {
        var type = (PacketType) typeByte;
        return
            Enum.IsDefined(type)
                ? type
                : SegmentationInfo.GetDataPacketType(typeByte); //Data has a variable "type" byte, so if the enum doesn't find a match, it must be a data packet
    }
}