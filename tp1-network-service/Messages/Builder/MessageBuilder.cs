namespace tp1_network_service.Messages;

public class MessageBuilder : IBuilder
{
    private Message _message;

    public void Reset()
    {
        _message = new Message();
    }

    public void SetType()
    {
        throw new NotImplementedException();
    }

    public void SetSource(int source)
    {
        _message.Source = source;
    }

    public void SetDestination(int destination)
    {
        _message.Destination = destination;
    }

    public Message GetResult()
    {
        return _message;
    }
}
