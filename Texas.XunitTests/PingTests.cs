using System.Net.NetworkInformation;
using System.Text;

namespace Texas.XunitTests;

public class PingItem
{
    public DateTime EventDate { get; set; }
    public string Destination { get; set; } = "";
    public int DurationInMilliseconds { get; set; }

}

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
        Ping pingSender = new();
        PingOptions options = new();
        PingItem highScore = new();

        CheckForExistingHighScore("ping.txt");

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
                if (reply.RoundtripTime > highScore.DurationInMilliseconds)
                {
                    // new high score
                    highScore.Destination = destination;
                    highScore.DurationInMilliseconds = Convert.ToInt32(reply.RoundtripTime);
                    highScore.EventDate = DateTime.Now;
                }
            }
        }
        if (roundTripTimes.Any())
        {
            long minimum = roundTripTimes.Min();
            long maximum = roundTripTimes.Max();
            double average = roundTripTimes.Average();
            Console.WriteLine("Minimum ping was {0}", minimum);
            Console.WriteLine("Maximum ping was {0}", maximum);
            Console.WriteLine("Average ping was {0}", average);
            if (totalCount > roundTripTimes.Count)
            {
                Console.WriteLine("{0} ping requests sent, {1} failed to return", totalCount, totalCount - roundTripTimes.Count);
            }
            if (highScore != null && highScore.DurationInMilliseconds > 0)
            {
                Console.WriteLine("Current high score is {0} for destination {1} at {2} local time",
                    highScore.DurationInMilliseconds, highScore.Destination, highScore.EventDate);
                File.WriteAllText("ping.txt",
                    $"{highScore.DurationInMilliseconds}|{highScore.Destination}|{highScore.EventDate}");
            }
        }
    }

    private PingItem CheckForExistingHighScore(string path)
    {
        if (File.Exists("ping.txt"))
        {
            var pings = File.ReadAllLines("ping.txt");
            if (pings == null || pings.Length == 0)


        }
    }
}