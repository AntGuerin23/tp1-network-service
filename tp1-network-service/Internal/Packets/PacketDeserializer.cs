using tp1_network_service.External.Exceptions;
using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Enums;
using tp1_network_service.Internal.Packets.Abstract;
using tp1_network_service.Internal.Packets.Segmentation;
using static tp1_network_service.Internal.Packets.PacketType;

namespace tp1_network_service.Internal.Packets;

internal class PacketDeserializer
{
    public static Packet Deserialize(byte[] rawInput)
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
                 builder.SetSourceAddress(rawInput[2])
                     .SetDestinationAddress(rawInput[3]);
                 return builder.ToConnectionRequestPacket();
             case Data:
                 throw new UserBNotImplementedException();
             default:
                 throw new ArgumentOutOfRangeException();
         }
    }

    private static PacketType FindActualType(byte typeByte)
    {
        // TODO : Checker le type de retour DataAcknowledgment
        var type = (PacketType) typeByte;
        return
            Enum.IsDefined(type)
                ? type
                : SegmentationInfo.GetDataPacketType(typeByte); //Data has a variable "type" byte, so if the enum doesn't find a match, it must be a data packet
    }
}