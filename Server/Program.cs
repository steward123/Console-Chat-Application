using ChatFramework;
using System.Net.Sockets;
using System.Threading.Tasks.Dataflow;

class Program {

    static Messenger inStream = new Messenger();
    static Messenger outStream = new Messenger();

static List<TcpClient> _clients = new List<TcpClient>();

static TcpListener listener = new TcpListener(Constants.Address, Constants.PORT);

    static async Task Main(string[] args)
    {
        listener.Start();

        Console.WriteLine("Server started!");

        while (true)
        {
            AcceptClients();

            ReceiveMessages();
        }
    }



static async Task AcceptClients()
{
    for (int i = 0; i < 5; i++)
    {
        if (!listener.Pending()) continue;

        var client = listener.AcceptTcpClient();
        _clients.Add(client);
        Console.WriteLine("Client connected!");
    }
}

static async Task ReceiveMessages()
{
    foreach (var client in _clients)
    {
        NetworkStream stream = client.GetStream();

        if(stream.DataAvailable)
        {
            byte[] buffer = new byte[client.ReceiveBufferSize];
            int bytesRead =  stream.Read(buffer,0,buffer.Length);
            (int opcode, string message) = inStream.ParseMessagePacket(buffer.Take(bytesRead).ToArray());
            Console.WriteLine($"Received message: {message}");

            Broadcast(client, message);
        }
    }
}

static async Task Broadcast(TcpClient sender, string message)
{
    foreach( var client in _clients.Where(c => c != sender))
    {
        var packet = outStream.CreateMessagePacket(10, message);
        client.GetStream().Write(packet);
    }
}
}