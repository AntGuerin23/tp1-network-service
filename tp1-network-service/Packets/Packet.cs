namespace tp1_network_service.Packets;

public abstract class Packet
{
    public string PacketType { get; set; }
    public int ConnexionNumber { get; set; }
    public int? Source { get; set; }
    public int? Destination { get; set; }

    public Packet()
    {
        ConnexionNumber = new Random().Next(1, 1001);
        Source = null;
        Destination = null;
    }
}
