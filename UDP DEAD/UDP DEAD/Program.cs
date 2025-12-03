using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class UdpIPv4Sender
{
    static void Main()
    {
        // ASCII banner at the top (KABOOM)
        Console.WriteLine(@" _  __    _    ____   ___   ___   __  __ ");
        Console.WriteLine(@"| |/ /   / \  | __ ) / _ \ / _ \ |  \/  |");
        Console.WriteLine(@"| ' /   / _ \ |  _ \| | | | | | || |\/| |");
        Console.WriteLine(@"| . \  / ___ \| |_) | |_| | |_| || |  | |");
        Console.WriteLine(@"|_|\_\/_/   \_\____/ \___/ \___/ |_|  |_|");
        Console.WriteLine();

        // Ask user for IP and Port
        Console.Write("Enter target IP (e.g., 127.0.0.1): ");
        string targetIp = Console.ReadLine();

        Console.Write("Enter target Port (e.g., 9000): ");
        string portInput = Console.ReadLine();
        int targetPort = int.TryParse(portInput, out int parsedPort) ? parsedPort : 9000;

        using (UdpClient udpClient = new UdpClient(AddressFamily.InterNetwork))
        {
            udpClient.Connect(IPAddress.Parse(string.IsNullOrEmpty(targetIp) ? "127.0.0.1" : targetIp), targetPort);

            Console.WriteLine("Sending UDP packets to {0}:{1}", targetIp, targetPort);

            for (int i = 0; i < 100000000; i++)
            {
                string message = $"KABOOM #{i + 1}";
                byte[] data = Encoding.UTF8.GetBytes(message);

                udpClient.Send(data, data.Length);
                Console.WriteLine("Sent: " + message);

                Thread.Sleep(0);
            }
        }

        Console.WriteLine("Done.");
    }
}