using Newtonsoft.Json;
using System;

namespace diydistdb
{
    public class Thing
    {
        private const int TicksPerMillisecond = 10000;

        public int Id { get; }

        public string Value { get; }

        public long Timestamp { get; }

        public Thing(int id, string value)
            : this(id, value, DateTime.Now.Ticks / TicksPerMillisecond)
        {
        }

        [JsonConstructor]
        public Thing(int id, string value, long timestamp)
        {
            Id = id;
            Value = value;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
