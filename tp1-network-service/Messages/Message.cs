using tp1_network_service.Enums;

namespace tp1_network_service.Messages;

internal class Message
{
    public string? Primitive { get; set; }
    public int ConnexionNumber { get; set; }
    public MessageType Type { get; set; }
    public int? Source { get; set; }
    public int? Destination { get; set; }

    public Message()
    {
        ConnexionNumber = new Random().Next(1, 1001);
        Source = null;
        Destination = null;
    }
}