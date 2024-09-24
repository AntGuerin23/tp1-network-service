namespace tp1_network_service.Messages;

public class Message
{
    public int ConnexionNumber { get; set; }
    public int? Source { get; set; }
    public int? Destination { get; set; }

    public Message()
    {
        ConnexionNumber = new Random().Next(1, 1001);
        Source = null;
        Destination = null;
    }
}
