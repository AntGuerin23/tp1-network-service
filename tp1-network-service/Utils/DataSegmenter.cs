using tp1_network_service.Builders;
using tp1_network_service.Packets;
using tp1_network_service.Primitives;

namespace tp1_network_service.Utils;

public static class DataSegmenter
{
    private const int DataPacketLength = 128;
    public static IEnumerable<DataPacket> SegmentDataPrimitive(DataPrimitive primitive)
    {
        return primitive.Data.Length <= DataPacketLength 
            ? GenerateSinglePacket(primitive) 
            : GenerateSegmentedPackets(primitive);
    }

    private static IEnumerable<DataPacket> GenerateSinglePacket(DataPrimitive primitive)
    {
        var packet = new PacketBuilder()
            .ConnectionNumber(primitive.ConnectionNumber)
            .IsSegmented(false)
            .SequenceNumber(0)
            .NextExpectedSequence(1)
            .Data(primitive.Data)
            .ToDataPacket();
        return [packet];
    }

    private static IEnumerable<DataPacket> GenerateSegmentedPackets(DataPrimitive primitive)
    {
        var packetBuilder = new PacketBuilder()
            .ConnectionNumber(primitive.ConnectionNumber)
            .IsSegmented(true);

        var segments = new List<DataPacket>();

        for (var i = 0; i < primitive.Data.Length; i += DataPacketLength)
        {
            var chunk = GetChunk(primitive.Data, i, DataPacketLength);
            var isLastPacket = i + chunk.Length>= primitive.Data.Length;

            var packet = packetBuilder
                .IsSegmented(!isLastPacket)
                .SequenceNumber(segments.Count % 8)
                .NextExpectedSequence((segments.Count + 1) % 8)
                .Data(chunk)
                .ToDataPacket();
            
            segments.Add(packet);
        }

        return segments;
    }
    
    private static byte[] GetChunk(byte[] data, int offset, int chunkSize)
    {
        var size = Math.Min(chunkSize, data.Length - offset);
        var chunk = new byte[size];
        Array.Copy(data, offset, chunk, 0, size);
        return chunk;
    }
}