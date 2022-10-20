using System;
using System.Threading.Tasks;
using MassTransit;

namespace MessageBroker
{
    public class ReportConsumer : IConsumer<Report>
    {
        public async Task Consume(ConsumeContext<Report> context)
        {
            var value = context.Message.Description; await Console.Out.WriteLineAsync(value);
            return ;
        }
    }
}