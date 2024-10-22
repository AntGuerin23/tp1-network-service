using tp1_network_service.External;
using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Enums;
using tp1_network_service.Internal.FileManagement;
using tp1_network_service.Internal.Packets;
using tp1_network_service.Internal.Packets.Abstract;
using tp1_network_service.Internal.Packets.Children;
using tp1_network_service.Internal.Packets.Segmentation;

namespace tp1_network_service.Internal;

internal class UserBSimulator
{
    private const byte AcknowledgeSuccessByte = 0b00000001;
    private const byte AcknowledgeFailureByte = 0b00001001; 
    
    private readonly FilePaths _filePaths;
    private bool _edgeCasesEnabled;
    
    public void Simulate(bool enableEdgeCases = true)
    {
        _edgeCasesEnabled = enableEdgeCases;
        var listener = new FileListener(new SyncListeningStrategy());
        while (true)
        {
            listener.Listen(_filePaths.Input, HandlePacket);
            Thread.Sleep(1000);
        }
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
        if (EdgeCase(packet.ConnectionNumber % 15 == 0)) return;
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
        if (EdgeCase(packet.ConnectionNumber % 19 == 0)) return;

        if (EdgeCase(packet.ConnectionNumber % 13 == 0))
        {
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
        FileManager.Write(packet.Serialize(), _filePaths.Output);
    }

    private Packet BuildDisconnectPacket(AddressedPacket packet)
    {
            return new PacketBuilder().SetType(PacketType.Disconnect)
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
            .SetSegmentationInfo(segInfo)
            .ToDataAcknowledgmentPacket();
    }
}