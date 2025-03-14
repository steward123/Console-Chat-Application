using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatFramework
{
    public static class Constants
    {
        // choose a port that's not already occupied
        public static int PORT = 5567;

        // we're using loopback for testing, and  .Any for when making it public
        public static IPAddress Address = IPAddress.Loopback;
    }
}
