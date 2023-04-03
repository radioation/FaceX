using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace JSONDataListener
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creates a UdpClient for reading incoming data.

            UdpClient receivingUdpClient = new UdpClient();
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 44515); // we're listening to port 44511
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
                    byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
                    string json = Encoding.UTF8.GetString(receiveBytes);
                    if (json != null)
                    {
                        Console.WriteLine(json);

                        // Parse the JSON data into a C# object
                        FXEyeGazeData gazeData = JsonConvert.DeserializeObject<FXEyeGazeData>(json);

                        // Extract the researcher name and x/y values
                        string subjectName = gazeData.Info.Subject;
                        string timestamp = gazeData.Timestamp;
                        var combined = gazeData.Combined;
                        if( combined != null )
                        {
                            FXEyeIntersection closest = gazeData.Combined.Closest;


                            // Do something with the data
                            Console.WriteLine("Subject: " + subjectName);
                            Console.WriteLine("Timestamp: " + timestamp);
                            if (closest != null)
                            { 
                                Console.WriteLine("Closest Intersection: ({0},{1}) name: {2} index: {3}",
                                    closest.X,
                                    closest.Y,
                                    closest.object_name,
                                    closest.ObjectIndex);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Quitting.");
        }
    }

    class FXEyeGazeData
    {
        public FXEyeGazeInfo Info { get; set; }
        public string Timestamp { get; set; }
        public Int64 Timestamp64 { get; set; }
        public FXEyeGazeIntersectionData Combined { get; set; }
        public FXEyeGazeIntersectionData Left { get; set; }
        public FXEyeGazeIntersectionData Right { get; set; }
        public FXEyeGazeIntersectionData Head { get; set; }
    }


    class FXEyeGazeInfo
    {
        public string Subject { get; set; }
        public string Version { get; set; }
        public string Calibration { get; set; }
        public string WorldModel { get; set; }
    }


    class FXEyeGazeIntersectionData
    {
        public bool Validity { get; set; }
        public FXEyeIntersection Closest { get; set; }
        public FXEyeIntersection[] AllIntersection { get; set; }

        public FXPoint3D Direction3d { get; set; }
        public FXPoint3D Origin3d { get; set; }
    }

    class FXEyeIntersection
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string object_name { get; set; }
        public int ObjectIndex { get; set; }
    }
    class FXPoint3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

    }


}