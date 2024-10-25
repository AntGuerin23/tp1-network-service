using System.Text;
using tp1_network_service.External.Exceptions;
using tp1_network_service.Internal.Enums;
using tp1_network_service.Internal.FileManagement;
using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Utils;

internal class TransportLogger
{
    private string? _filePath;

    public void SetFilePath(string filePath)
    {
        _filePath = filePath;
    }

    public void LogNewWaitingConnection(ConnectPrimitive primitive)
    {
        Write($"Connection request ({primitive.ConnectionNumber}) from {primitive.DestinationAddress} to {primitive.SourceAddress} : WAITING");
    }

    public void LogNewConfirmedConnection(ConnectPrimitive primitive)
    {
        Write($"Connection request ({primitive.ConnectionNumber}) from {primitive.DestinationAddress} to {primitive.SourceAddress} : CONFIRMED");
    }

    public void LogDataTransmission(int connectionNumber, byte[] data)
    {
        Write($"Sending data to connection ({connectionNumber}) : {Encoding.UTF8.GetString(data)}");
    }

    public void LogDisconnectIndication(DisconnectPrimitive primitive)
    {
        var reasonText = "";
        switch (primitive.Reason)
        {
            case DisconnectReason.Distant:
                reasonText = "Rejected by distant";
                break;
            case DisconnectReason.NetworkService:
                reasonText = "Network error";
                break;
            case DisconnectReason.Success:
                reasonText = "Data transmission is over.";
                break;
        }
        Write($"Disconnect connection ({primitive.ConnectionNumber}) from {primitive.DestinationAddress} to {primitive.SourceAddress} : {reasonText}");
    }

    private void Write(string content)
    {
        if (_filePath == null)
        {
            throw new FilePathNotSpecifiedException("Transport Layer : DataLink layer paths must be specified.");
        }
        FileManager.WriteWithNewLine(Encoding.UTF8.GetBytes(content), _filePath);
    }
}