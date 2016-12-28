using System;
using System.Threading.Tasks;

namespace diydistdb
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodeUrls = new[] { "http://localhost:9999" };
            WriteAsync(nodeUrls, new Thing(3, "foo")).Wait();
            WriteAsync(nodeUrls, new Thing(7, "bar")).Wait();
            var thing3 = ReadAsync(nodeUrls, 3).Result;
            var thing7 = ReadAsync(nodeUrls, 7).Result;
            Console.WriteLine(thing3);
            Console.WriteLine(thing7);
        }

        private static async Task WriteAsync(string[] nodeUrls, Thing thing)
        {
            await Node.PutThingAsync(nodeUrls[0], thing);
        }

        private static async Task<Thing> ReadAsync(string[] nodeUrls, int id)
        {
            return await Node.GetThingAsync(nodeUrls[0], id);
        }
    }
}
