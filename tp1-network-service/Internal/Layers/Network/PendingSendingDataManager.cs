using tp1_network_service.Internal.Packets;
using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.Network;

internal class PendingSendingDataManager
{
    private const int AcknowledgementTimeoutSeconds = 5;
    
    private PacketSegmenter? _currentPacketSegmenter; 
    private Mutex? _acknowledgementMutex;

    public bool StartSendingData(DataPrimitive primitive)
    {
        _currentPacketSegmenter = new PacketSegmenter(primitive);
        _acknowledgementMutex = new Mutex();
        while (_currentPacketSegmenter.HasNextSegment)
        {
            var packet = _currentPacketSegmenter.ConstructNextPacket();
            NetworkLayer.Instance.SendPacket(packet);
            var success = WaitForAcknowledgement();
            if (!success) return false;
        }
        return true;
    }
    
    public void AcknowledgeLastPacket()
    {
        _acknowledgementMutex?.ReleaseMutex();
    }

    private bool WaitForAcknowledgement()
    {
        var timeout = new CancellationTokenSource();
        var timeoutTask = new Task(() => WaitForTimeout(timeout.Token));
        _acknowledgementMutex!.WaitOne();
        if (timeoutTask.IsCompleted) return false;
        timeout.Cancel();
        return true;
    }

    private void WaitForTimeout(CancellationToken timeoutToken)
    {
        Thread.Sleep(AcknowledgementTimeoutSeconds * 1000);
        if (timeoutToken.IsCancellationRequested) return;
        _acknowledgementMutex!.ReleaseMutex();
    }
}