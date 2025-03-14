using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatFramework
{

        public class Messenger
        {
            // Function to create a message packet
            // This function takes in an opcode (operation code) and a message,
            // and returns a byte array that can be sent over the network

            public byte[] CreateMessagePacket(int opcode, string message)
            {
                // Use a memory stream to hold the data
                // BinaryWriter is then used to write the opcode and the message to this stream

                using MemoryStream ms = new MemoryStream();
                using BinaryWriter bw = new BinaryWriter(ms);

                bw.Write(opcode); // write the opcode to the stream
                bw.Write(message); // write the message to the stream

                return ms.ToArray();
            }

            public (int opcode, string message) ParseMessagePacket(byte[] data)
            {
                // Use a memory stream to read the data
                // BinaryReader is then used to read the opcode and the message from this stream
                using MemoryStream ms = new MemoryStream(data);
                using BinaryReader br = new BinaryReader(ms, Encoding.ASCII);
                int opcode = br.ReadInt32(); // read the opcode from the stream
                string message = br.ReadString(); // read the message from the stream
                return (opcode, message);
            }
        }
}
