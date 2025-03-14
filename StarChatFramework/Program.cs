using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChatFramework
{
    public class Program
    {

        public static Messenger msgStream = new Messenger();

        public static int PORT = Constants.PORT;

        public static IPAddress Address = Constants.Address;

        public static void Main()
        {
            //Messenger inStream = new Messenger();
            
            //Messenger outStream = new Messenger();

        }
    }
}