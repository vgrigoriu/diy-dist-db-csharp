using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diydistdb
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodeUrls = new[]
            {
                "http://localhost:9999",
                "http://localhost:9998",
                "http://localhost:9997"
            };
            WriteAsync(nodeUrls, 3, 2, new Thing(3, "foo")).Wait();
            WriteAsync(nodeUrls, 3, 2, new Thing(7, "bar")).Wait();
            var thing3 = ReadAsync(nodeUrls, 3, 2, 3).Result;
            var thing7 = ReadAsync(nodeUrls, 3, 2, 7).Result;
            Console.WriteLine(thing3);
            Console.WriteLine(thing7);
        }

        private static async Task WriteAsync(
            string[] nodeUrls,
            int replicationFactor,
            int writeConsistency,
            Thing thing)
        {
            int successCount = 0;
            foreach (var nodeUrl in nodeUrls.Take(replicationFactor))
            {
                try
                {
                    await Node.PutThingAsync(nodeUrl, thing);
                    successCount++;
                }
                catch (Exception)
                {
                    // ignore
                }
            }

            if (successCount < writeConsistency)
            {
                throw new Exception($"Only wrote to {successCount} nodes, needed at least {writeConsistency}");
            }
        }

        private static async Task<Thing> ReadAsync(
            string[] nodeUrls,
            int replicationFactor,
            int readConsistency,
            int id)
        {
            var things = new List<Thing>();

            foreach (var nodeUrl in nodeUrls.Take(replicationFactor))
            {
                try
                {
                    var thing = await Node.GetThingAsync(nodeUrl, id);
                    things.Add(thing);
                    if (readConsistency <= things.Count)
                    {
                        // we got enough successful replies
                        break;
                    }
                }
                catch (Exception)
                {
                    // ignore
                }
            }

            if (things.Count < readConsistency)
            {
                throw new Exception($"Only read from {things.Count} nodes, needed at least {readConsistency}");
            }

            return things.OrderBy(thing => thing.Timestamp).Last();
        }
    }
}
