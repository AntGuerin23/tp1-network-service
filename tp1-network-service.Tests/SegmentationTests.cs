using tp1_network_service.Internal.Packets.Segmentation;

namespace tp1_network_service.Tests;

public class SegmentationTests
{
    [Test]
    public void TestSegInfoCreationFromByte()
    {
        var info = new SegmentationInfo(0b01010010);
        Assert.That(info.CurrentSegmentNumber, Is.EqualTo(1));
        Assert.That(info.OtherSegmentsAreToCome, Is.EqualTo(true));
        Assert.That(info.NextSegmentNumber, Is.EqualTo(2));
    }
    
    [Test]
    public void TestSegInfoSerialize()
    {
        var info = new SegmentationInfo(1, true, 3);
        Assert.That(info.Serialize(), Is.EqualTo(0b00110110));
    }

    [Test]
    public void TestSegInfoSerializeFromByte()
    {
        var info = new SegmentationInfo(0b01010010);
        Assert.That(info.Serialize(), Is.EqualTo(0b01010010));
    }

    [Test]
    public void TestSegInfoSerializeAck()
    {
        var b = SegmentationInfo.SerializeAcknowledgment(false, 0b010);
        Assert.That(b, Is.EqualTo(0b01001001));
    }
}