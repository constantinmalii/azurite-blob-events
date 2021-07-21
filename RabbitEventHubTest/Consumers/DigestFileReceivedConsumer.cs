using System.Diagnostics;
using System.Threading.Tasks;
using MassTransit;

namespace RabbitEventHubTest.Consumers
{
    public class DigestFileReceivedConsumer:IConsumer<DigestFileReceived>
    {
        public Task Consume(ConsumeContext<DigestFileReceived> context)
        {
            Debug.WriteLine("Costea in {consumerName}", nameof(DigestFileReceivedConsumer));
            throw new System.NotImplementedException();
        }
    }
}