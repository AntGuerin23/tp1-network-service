namespace tp1_network_service.Internal.Utils;

internal static class ConsoleLogger
{
    public static void ConnectionRequestReceived(int connectionNumber, int sourceAddress, int destinationAddress)
    {
        Separator();
        Console.WriteLine($"Distant : Connection request ({connectionNumber}) received from source ({sourceAddress}) to destination ({destinationAddress}).");
    }

    public static void ConnectionRequestRefused(int connectionNumber)
    {
        Console.WriteLine($"Distant : Connection request ({connectionNumber}) refused.");
    }

    public static void ConnectionRequestIgnored(int connectionNumber)
    {
        Console.WriteLine($"Distant : Connection request ({connectionNumber}) ignored.");
    }

    public static void DataPacketIgnored(int connectionNumber)
    {
        Console.WriteLine($"Distant : Data packet ignored from {connectionNumber}."); 
    }

    private static void Separator()
    {
        Console.WriteLine("===========================================================================================================================================");
    }
}