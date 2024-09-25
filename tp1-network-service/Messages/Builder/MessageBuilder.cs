using tp1_network_service.Enums;

namespace tp1_network_service.Messages.Builder;

internal class MessageBuilder : IBuilder
{
    private Message _message;

    public void Reset()
    {
        _message = new Message();
    }

    public void SetType(MessageType type)
    {
        _message.Type = type;
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
