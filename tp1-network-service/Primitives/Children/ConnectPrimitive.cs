namespace tp1_network_service.Primitives.Children;

internal class ConnectPrimitive : Primitive
{
    public int? SourceAddress { get; }
    public int? DestinationAddress { get; }

    public int? ResponseAddress
    {
        get
        {
            if (Type is PrimitiveType.Resp or PrimitiveType.Conf)
            {
                return DestinationAddress;
            }
            return default;
        }
    }

    public ConnectPrimitive(Primitive primitive, int source, int destination) : base(primitive)
    {
        SourceAddress = source;
        DestinationAddress = destination;
    }

    public override void Handle(bool isHandledByTransport = false)
    {
        // if (isHandledByTransport)
        // {
        //     HandleTransport();
        //     return;
        // }
        // HandleNetwork();
    }

    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }

    // private void HandleTransport()
    // {
    //     TransportLayer.Instance.ConnectionsHandler.ConfirmWaitingConnection(ConnectionNumber);
    //     var pendingData = TransportLayer.Instance.ConnectionsHandler.GetPendingData(ConnectionNumber);
    //     
    //     if (pendingData.Length <= 0) return;
    //     
    //     var messageBuilder = new MessageBuilder();
    //     // TODO : Remove Segmentation in building DataMesage From Transport
    //     var segInfo = new SegmentationInfo(2);
    //     var dataMessage = messageBuilder.SetConnectionNumber((byte) ConnectionNumber)
    //         .SetSource((byte) Source)
    //         .SetDestination((byte) Destination)
    //         .ToDataMessage(segInfo, pendingData)
    //         .GetResult();
    //     TransportLayer.Instance.SendMessageToNetworkLayer(dataMessage);
    // }
    //
    // private void HandleNetwork()
    // {
    //     switch (Primitive)
    //     {
    //         case MessagePrimitive.Req:
    //             HandleConnectRequest();
    //             break;
    //         case MessagePrimitive.Resp:
    //             HandleConnectResponse();
    //             break;
    //     }
    // }
    //
    // private void HandleConnectRequest()
    // {
    //     var messageBuilder = new MessageBuilder();
    //     if (IsNetworkServiceError())
    //     {
    //         var disconnectMessage = messageBuilder.SetConnectionNumber(ConnectionNumber)
    //             .SetSource(Source)
    //             .SetDestination(Destination)
    //             .SetPrimitive(MessagePrimitive.Ind)
    //             .ToDisconnectMessage(DisconnectReason.NetworkService)
    //             .GetResult();
    //         NetworkLayer.Instance.SendMessageToTransportLayer(disconnectMessage);
    //         return;
    //     }
    //     NetworkLayer.Instance.SetWaitingConnectionNumberAndDestination(ConnectionNumber, (byte)Destination);
    //     var connectIndMessage = messageBuilder.SetConnectionNumber(ConnectionNumber)
    //         .SetSource(Source)
    //         .SetDestination(Destination)
    //         .SetPrimitive(MessagePrimitive.Ind)
    //         .ToConnectMessage()
    //         .GetResult();
    //     NetworkLayer.Instance.SendMessageToDataLinkLayer(connectIndMessage);
    // }
    //
    // private void HandleConnectResponse()
    // {
    //     var messageBuilder = new MessageBuilder();
    //     var waitingConnection = NetworkLayer.Instance.GetAndResetWaitingConnectionNumberAndDestination();
    //     if (waitingConnection != null && waitingConnection.Value.Item1 != ConnectionNumber)
    //     {
    //         var disconnectMessage = messageBuilder.SetConnectionNumber((byte)waitingConnection.Value.Item1)
    //             .SetSource(waitingConnection.Value.Item2)
    //             .SetPrimitive(MessagePrimitive.Ind)
    //             .ToDisconnectMessage(DisconnectReason.Self)
    //             .GetResult();
    //         NetworkLayer.Instance.SendMessageToTransportLayer(disconnectMessage);
    //     }
    //     var connectResponseMessage = messageBuilder.SetConnectionNumber((byte)ConnectionNumber)
    //         .SetSource(Source)
    //         .SetDestination(Destination)
    //         .SetPrimitive(MessagePrimitive.Conf)
    //         .ToConnectMessage()
    //         .GetResult();
    //     NetworkLayer.Instance.SendMessageToTransportLayer(connectResponseMessage);
    // }

    //private bool IsNetworkServiceError() => (Source & 27) == 0;
}