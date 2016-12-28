namespace diydistdb
{
    public class Thing
    {
        public int Id { get; }

        public string Value { get; }

        public long Timestamp { get; }

        public Thing(int id, string value, long timestamp)
        {
            Id = id;
            Value = value;
            Timestamp = timestamp;
        }
    }
}
