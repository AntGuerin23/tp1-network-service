using System.Text;
using System.Threading.Channels;
using tp1_network_service.External;
using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Enums;
using tp1_network_service.Internal.FileManagement;
using tp1_network_service.Internal.FileManagement.FileManagers;
using tp1_network_service.Internal.Packets;
using tp1_network_service.Internal.Packets.Abstract;
using tp1_network_service.Internal.Packets.Children;
using tp1_network_service.Internal.Packets.Segmentation;
using tp1_network_service.Internal.Utils;

namespace tp1_network_service.Internal;

internal class UserBSimulator
{
    private const byte AcknowledgeSuccessByte = 0b00000001;
    private const byte AcknowledgeFailureByte = 0b00001001;
    
    private readonly FilePaths _filePaths;
    private bool _edgeCasesEnabled;
    
    public void Simulate(CancellationToken cancellationToken, bool enableEdgeCases = true)
    {
        _edgeCasesEnabled = enableEdgeCases;
        var listener = new FileListener(new SyncListeningStrategy(new BinaryFileManager()));
        listener.Listen(_filePaths.Input, HandlePacket);
        while (!cancellationToken.IsCancellationRequested) { } 
        listener.Stop();
    }

    public UserBSimulator(FilePaths filePaths)
    {
        _filePaths = filePaths;
    }

    private void HandlePacket(byte[] rawPacket)
    {
        var packet = PacketDeserializer.Deserialize(rawPacket);
        switch (packet)
        {
            case DataPacket dataPacket:
                OnDataPacket(dataPacket);
                break;
            case ConnectionRequestPacket connectionPacket:
                OnConnectRequestPacket(connectionPacket);
                break;
        }
    }

    private void OnDataPacket(DataPacket packet)
    {
        if (EdgeCase(packet.ConnectionNumber % 15 == 0))
        {
            ConsoleLogger.DataPacketIgnored(packet.ConnectionNumber);
            return;
        }
        ConsoleLogger.DataPacketReceived(packet);
        WriteToOutput(BuildDataAckPacket(packet, GetAcknowledgementType(packet.SegInfo.CurrentSegmentNumber)));
    }

    private SegmentationInfo GetAcknowledgementType(byte segmentNumber)
    {
        var leftShiftedSegmentNumber = (byte) (segmentNumber << 5);       
        
        return EdgeCase(new Random().Next(0, 8) == segmentNumber)
            ? new SegmentationInfo((byte) (AcknowledgeFailureByte & leftShiftedSegmentNumber)) 
            : new SegmentationInfo((byte) (AcknowledgeSuccessByte & leftShiftedSegmentNumber));
    }
    
    private void OnConnectRequestPacket(ConnectionRequestPacket packet)
    {
        ConsoleLogger.ConnectionRequestReceived(packet.ConnectionNumber, packet.SourceAddress, packet.DestinationAddress);
        if (EdgeCase(packet.SourceAddress % 19 == 0))
        {
            ConsoleLogger.ConnectionRequestIgnored(packet.ConnectionNumber);
            return;
        }

        if (EdgeCase(packet.SourceAddress % 13 == 0))
        {
            ConsoleLogger.ConnectionRequestRefused(packet.ConnectionNumber);
            WriteToOutput(BuildDisconnectPacket(packet));
            return;
        }

        WriteToOutput(BuildConnectResponsePacket(packet));
    }
    
    private bool EdgeCase(bool edgeCase)
    {
        return edgeCase && _edgeCasesEnabled;
    }

    private void WriteToOutput(Packet packet)
    {
        new BinaryFileManager().WriteWithNewLine(packet.Serialize(), _filePaths.Output);
    }

    private Packet BuildDisconnectPacket(AddressedPacket packet)
    {
            return new PacketBuilder().SetType(PacketType.Disconnect).SetConnectionNumber(packet.ConnectionNumber)
            .SetReason(DisconnectReason.Distant)
            .SetDestinationAddress(packet.SourceAddress)
            .SetSourceAddress(packet.DestinationAddress)
            .ToDisconnectPacket();
    }

    private Packet BuildConnectResponsePacket(AddressedPacket packet)
    {
        return new PacketBuilder()
            .SetType(PacketType.ConnectConfirmation)
            .SetConnectionNumber(packet.ConnectionNumber)
            .SetDestinationAddress(packet.SourceAddress)
            .SetSourceAddress(packet.DestinationAddress)
            .ToConnectionConfirmationPacket();
    }

    private Packet BuildDataAckPacket(Packet packet, SegmentationInfo segInfo)
    {
        return new PacketBuilder()
            .SetConnectionNumber(packet.ConnectionNumber)
            .SetType(PacketType.DataAcknowledgment)
            .SetSegmentationInfo(segInfo)
            .ToDataAcknowledgmentPacket();
    }
}