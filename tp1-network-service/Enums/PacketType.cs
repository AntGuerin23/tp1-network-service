namespace tp1_network_service.Enums;

internal enum PacketType
{
    ConnectRequest = 0b00001011,
    ConnectConfirmation = 0b00001111,
    Disconnect = 0b00010011,
    Data = -1, //The "type" byte of a data packet is never the same
}