//Define the TCP Client
using ChatFramework;
using System.Net.Sockets;

class Program
{

    static TcpClient client = new TcpClient();

    static List<string> outgoingMessages = new List<string>();

    //
    static Messenger inStream = new Messenger();
    static Messenger outStream = new Messenger();

    static void Main()
    {

        // connect the client to the Server using defined Address and Port in Constants
        client.Connect(Constants.Address, Constants.PORT);
        Console.WriteLine("Connected to the server");


        new TaskFactory().StartNew(() =>
        {
            while (true)
            {
                // read the message from the console
                string message = Console.ReadLine();
                // add the message to the outgoing messages list
                outgoingMessages.Add(message);
            }
        });

        while (true)
        {
            ReadPackets();
            SendPackets();
        }
    }

    static async Task ReadPackets()
    {
        var stream = client.GetStream();

        for (int i = 0; i < 10; i++)
        {
            if (stream.DataAvailable)
            {
                byte[] data = new byte[client.Available];
                int bytesRead = stream.Read(data, 0, data.Length);
                (int opcode, string message) = inStream.ParseMessagePacket(data.Take(bytesRead).ToArray());
                Console.WriteLine($"Received message: {message}");
            }
        }
    }

    static async Task SendPackets()
    {
        if (outgoingMessages.Count > 0)
        {
            string message = outgoingMessages[0];
            var packet = outStream.CreateMessagePacket(10, message);
            client.GetStream().Write(packet);
            outgoingMessages.RemoveAt(0);
        }
    }
}




