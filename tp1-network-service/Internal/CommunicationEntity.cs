namespace tp1_network_service.Internal;

internal abstract class CommunicationEntity
{
    protected CommunicationEntity(int connectionNumber)
    {
        ConnectionNumber = connectionNumber;
    }
    
    public int ConnectionNumber { get; set; }
}