namespace RabbitEventHubTest.Consumers
{
    public record DigestFileReceived
    {
        public string Url { get; set; }
    }
}