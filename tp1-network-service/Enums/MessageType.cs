namespace tp1_network_service.Enums;

internal enum MessageType
{
    Connect = 0b00001111,
    Data = 0b00001011,
    Disconnect = 0b00010011
}