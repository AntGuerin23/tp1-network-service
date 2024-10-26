using tp1_network_service.External;
using tp1_network_service.Internal;
using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.FileManagement;
using tp1_network_service.Internal.FileManagement.FileManagers;
using tp1_network_service.Internal.Packets;

namespace tp1_network_service.Tests;

public class BSimulationTests
{
    [Test]
    public void TestBSimulationConnectionResponse()
    {
        File.Create("b-input").Dispose();
        File.Create("b-output").Dispose();

        var token = new CancellationTokenSource();
        var task = new Task(() => new UserBSimulator(new FilePaths("b-input", "b-output")).Simulate(token.Token, false));
        task.Start();

        var packet = new PacketBuilder()
            .SetType(PacketType.ConnectRequest)
            .SetConnectionNumber(1)
            .SetDestinationAddress(1)
            .SetSourceAddress(2)
            .ToConnectionRequestPacket();
        new BinaryFileManager().WriteWithNewLine(packet.Serialize(), "b-input");
        
        Thread.Sleep(100);
        var bytes = new BinaryFileManager().ReadAndDeleteFirstLine("b-output");
        Assert.That(bytes, Is.EqualTo(new byte[]{0b00000001, 0b00001111, 0b00000001, 0b00000010}));
        var newPacket = PacketDeserializer.Deserialize(bytes);
        
        Assert.That(newPacket.Type, Is.EqualTo(PacketType.ConnectConfirmation));
        Assert.That(newPacket.ConnectionNumber, Is.EqualTo(1));
        
        token.Cancel();
        task.Wait();
    }
}