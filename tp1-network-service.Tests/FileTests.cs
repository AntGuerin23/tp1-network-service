using System.Text;
using tp1_network_service.Internal.FileManagement;

namespace tp1_network_service.Tests;

public class FileTests
{
    [SetUp]
    public void Setup()
    {
        File.Create("test.txt").Dispose();
    }
    
    [Test]
    public void TestWriteOne()
    {
        var input = new byte[] { 1, 2, 3 };
        
        FileManager.WriteWithNewLine(input, "test.txt");
        var result = File.ReadAllBytes("test.txt");
        
        var newline = Environment.NewLine.ToCharArray();
        foreach (var newLineChar in newline)
        {
            input = input.Append((byte) newLineChar).ToArray();
        }
        
        Assert.That(result, Is.EqualTo(input));
    }

    [Test]
    public void TestReadOne()
    {
        var input = new byte[] { 1, 2, 3 };
        
        FileManager.WriteWithNewLine(input, "test.txt");

        var result = FileManager.ReadAndDeleteFirstLineOfFile("test.txt");
        Assert.That(result, Is.EqualTo(input));
    }
    
    [Test]
    public void TestReadMultiple()
    {
        var input = new byte[] { 1, 2, 3 };
        
        FileManager.WriteWithNewLine(input, "test.txt");
        FileManager.WriteWithNewLine(input, "test.txt");
        FileManager.WriteWithNewLine(input, "test.txt");

        var result = FileManager.ReadAndDeleteFirstLineOfFile("test.txt");
        Assert.That(result, Is.EqualTo(input));
        result = FileManager.ReadAndDeleteFirstLineOfFile("test.txt");
        Assert.That(result, Is.EqualTo(input));
        result = FileManager.ReadAndDeleteFirstLineOfFile("test.txt");
        Assert.That(result, Is.EqualTo(input));
        result = FileManager.ReadAndDeleteFirstLineOfFile("test.txt");
        Assert.That(result, Is.EqualTo(Array.Empty<byte>()));
    }

    [TearDown]
    public void TearDown()
    {
        File.Delete("test.txt");
    }
}