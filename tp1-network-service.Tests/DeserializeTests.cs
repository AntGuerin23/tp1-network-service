using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.FileManagement;
using tp1_network_service.Internal.FileManagement.FileManagers;
using tp1_network_service.Internal.Packets;
using tp1_network_service.Internal.Packets.Segmentation;

namespace tp1_network_service.Tests;

public class DeserializeTests
{
    [Test]
    public void TestSerializeOne()
    {
        var packet =  new PacketBuilder()
            .SetConnectionNumber(1)
            .SetType(PacketType.DataAcknowledgment)
            .SetSegmentationInfo(new SegmentationInfo(0b0001001))
            .ToDataAcknowledgmentPacket();
        File.Create("output").Dispose();
        new BinaryFileManager().WriteWithNewLine(packet.Serialize(), "output");

        var data = new BinaryFileManager().ReadAndDeleteFirstLine("output");

        var deserializedPacket = PacketDeserializer.Deserialize(data);
        
        Assert.That(deserializedPacket, Is.Not.Null);
        Assert.That(deserializedPacket.Type, Is.EqualTo(PacketType.DataAcknowledgment));
    }
    
    [Test]
    public void TestSerializeMultiple()
    {
        var packet =  new PacketBuilder()
            .SetConnectionNumber(1)
            .SetType(PacketType.DataAcknowledgment)
            .SetSegmentationInfo(new SegmentationInfo(0b0001001))
            .ToDataAcknowledgmentPacket();
        File.Create("output").Dispose();
        
        List<Task> tasks = new List<Task>();
        
        var listener = new FileListener(new AsyncListeningStrategy(new BinaryFileManager()));
        listener.Listen("output", HandleAckPacket);

        for (var i = 0; i < 10; i++)
        {
            var task = new Task(() => new BinaryFileManager().WriteWithNewLine(packet.Serialize(), "output"));
            tasks.Add(task);
            task.Start();
        }
        
        Thread.Sleep(3000);
        
        listener.Stop();
    }

    private void HandleAckPacket(byte[] data)
    {
        var deserializedPacket = PacketDeserializer.Deserialize(data);
        Assert.That(deserializedPacket, Is.Not.Null);
        Assert.That(deserializedPacket.Type, Is.EqualTo(PacketType.DataAcknowledgment));
    }
}