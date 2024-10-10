namespace tp1_network_service.Primitives.Children;

internal class DataPrimitive : Primitive
{
    public byte[] Data { get; set; }

    public DataPrimitive(Primitive primitive, byte[] data) : base(primitive)
    {
        Data = data;
    }

    public override void Handle(bool isHandledByTransport = false)
    {
        throw new NotImplementedException();
    }

    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }
}