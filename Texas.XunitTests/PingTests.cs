using System.Net.NetworkInformation;
using System.Text;

namespace Texas.XunitTests;

public class PingTests
{
    [Theory]
    [InlineData("8.8.8.8")]
    [InlineData("1.1.1.1")]
    [InlineData("8.8.4.4")]
    [InlineData("1.0.0.1")]
    [InlineData("192.168.12.1")]
    public void TestPing(string destination)
    {
        int totalCount = 20;
        Ping pingSender = new Ping();
        PingOptions options = new PingOptions();

        // Use the default Ttl value which is 128,
        // but change the fragmentation behavior.
        options.DontFragment = true;

        // Create a buffer of 32 bytes of data to be transmitted.
        string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        byte[] buffer = Encoding.ASCII.GetBytes(data);
        int timeout = 120;
        List<long> roundTripTimes = new();
        Console.WriteLine("For {0}: ", destination);
        for (int i = 0; i < totalCount; i++)
        {
            PingReply reply = pingSender.Send(destination, timeout, buffer, options);
            if (reply?.Status == IPStatus.Success)
            {
                // Console.WriteLine("Address: {0}", reply.Address.ToString());
                Console.WriteLine("RoundTrip time in attempt{0}: {1}", i, reply.RoundtripTime);
                // Console.WriteLine("Time to live: {0}", reply?.Options?.Ttl);
                // Console.WriteLine("Don't fragment: {0}", reply?.Options?.DontFragment);
                // Console.WriteLine("Buffer size: {0}", reply?.Buffer.Length);
                roundTripTimes.Add(reply.RoundtripTime);
            }
        }
        long minimum = roundTripTimes.Min();
        long maximum = roundTripTimes.Max();
        double average = roundTripTimes.Average();
        Console.WriteLine("Minimum ping was {0}", minimum);
        Console.WriteLine("Maximum ping was {0}", maximum);
        Console.WriteLine("Minimum ping was {0}", average);
        if (totalCount > roundTripTimes.Count)
        {
            Console.WriteLine("{0} ping requests sent, {1} failed to return", totalCount, totalCount - roundTripTimes.Count);
        }
    }
}