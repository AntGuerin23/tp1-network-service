using System.Text;
using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.FileManagement;
using tp1_network_service.Internal.FileManagement.FileManagers;
using tp1_network_service.Internal.Packets;
using tp1_network_service.Internal.Packets.Segmentation;

namespace tp1_network_service.Tests;

public class FileTests
{
    [SetUp]
    public void Setup()
    {
        File.Create("test.txt").Dispose();
    }
    
    [Test]
    public void TestWriteConReq()
    {
        var packet = new PacketBuilder().SetConnectionNumber(1)
            .SetType(PacketType.ConnectRequest)
            .SetSourceAddress(2)
            .SetDestinationAddress(2)
            .ToConnectionRequestPacket();  
        
        new BinaryFileManager().WriteWithNewLine(packet.Serialize(), "test.txt");
        var result = File.ReadAllBytes("test.txt");
        
        
        Assert.That(result[0], Is.EqualTo(1));
        Assert.That(result[2], Is.EqualTo(2));
        Assert.That(result[3], Is.EqualTo(2));
    }
    
    [Test]
    public void TestWriteAck()
    {
        var packet = new PacketBuilder().SetConnectionNumber(1)
            .SetType(PacketType.DataAcknowledgment)
            .SetSegmentationInfo(new SegmentationInfo(SegmentationInfo.SerializeAcknowledgment(true, 0b010)))
            .ToDataAcknowledgmentPacket();  
        
        Assert.That(packet.Serialize(), Is.EqualTo(new byte[]{1, 0b01000001}));
        
        new BinaryFileManager().WriteWithNewLine(packet.Serialize(), "test.txt");
        var result = File.ReadAllBytes("test.txt");
        
        
        Assert.That(result[0], Is.EqualTo(1));
        Assert.That(result[1], Is.EqualTo(0b01000001));
        Assert.That(result.Length, Is.EqualTo(2));
    }
}