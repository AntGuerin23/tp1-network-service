namespace tp1_network_service;

internal abstract class CommunicationEntity
{
    protected CommunicationEntity(int connectionNumber)
    {
        ConnectionNumber = connectionNumber;
    }
    
    public int ConnectionNumber { get; set; }
}