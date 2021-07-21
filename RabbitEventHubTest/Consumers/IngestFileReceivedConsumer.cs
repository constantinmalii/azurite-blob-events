using System.Diagnostics;
using System.Threading.Tasks;
using MassTransit;

namespace RabbitEventHubTest.Consumers
{
    public class IngestFileReceivedConsumer:IConsumer<IngestFileReceived>
    {
        public Task Consume(ConsumeContext<IngestFileReceived> context)
        {
            Debug.WriteLine("Costea in {consumerName}", nameof(IngestFileReceivedConsumer));
            throw new System.NotImplementedException();
        }
    }
}