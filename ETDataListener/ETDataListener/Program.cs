using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace ETDataListener
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creates a UdpClient for reading incoming data.
            UdpClient receivingUdpClient = new UdpClient(); 
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 44511); // we're listening to port 44511
            receivingUdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            receivingUdpClient.ExclusiveAddressUse = false;

            // join 234.5.6.7
            IPAddress GroupAddress = IPAddress.Parse("234.5.6.7");
            receivingUdpClient.Client.Bind(RemoteIpEndPoint);
            try
            {

                Console.WriteLine("Attempting to join multicast group {0}", GroupAddress.ToString());
                receivingUdpClient.JoinMulticastGroup(GroupAddress);

                Console.WriteLine("listening for retransmitted eye tracking data. Press 'ESC' key to quit.");
                while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
                {


                    // Wait for bytes to come in
                    Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                    // convert to values.
                    int offset = 0;
                    var fileTime = BitConverter.ToUInt64(receiveBytes, 0);
                    offset += 8; // filetime is 64 bits or 8-bytes
                    string Name = Encoding.ASCII.GetString(receiveBytes, offset, 64);
                    offset += 32;
                    string ObjectIntersectionName = Encoding.ASCII.GetString(receiveBytes, offset, 64);
                    offset += 64;
                    var IntersectionIndex = BitConverter.ToInt32(receiveBytes, offset);
                    offset += sizeof(int);
                    var ObjectIntersectionX = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var ObjectIntersectionY = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);


                    var HeadPosX = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var HeadPosY = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var HeadPosZ = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);


                    var LeftGazeOriginX = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var LeftGazeOriginY = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var LeftGazeOriginZ = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);

                    var LeftGazeDirectionX = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var LeftGazeDirectionY = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var LeftGazeDirectionZ = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);

                    var RightGazeOriginX = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var RightGazeOriginY = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var RightGazeOriginZ = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);

                    var RightGazeDirectionX = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var RightGazeDirectionY = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var RightGazeDirectionZ = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);

                    var LeftGazeType = BitConverter.ToInt32(receiveBytes, offset);
                    offset += sizeof(int);
                    var RightGazeType = BitConverter.ToInt32(receiveBytes, offset);
                    offset += sizeof(int);



                    var HeadX = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var HeadY = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var HeadZ = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);


                    var HeadDirectionX = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var HeadDirectionY = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);
                    var HeadDirectionZ = BitConverter.ToSingle(receiveBytes, offset);
                    offset += sizeof(float);


                    Console.WriteLine("Filetime: {0} ObjectIntersectionX:{1} ObjectIntersectionY:{2} objectName: {3}", fileTime, ObjectIntersectionX, ObjectIntersectionY, ObjectIntersectionName);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Quitting.");
        }
    }
}
