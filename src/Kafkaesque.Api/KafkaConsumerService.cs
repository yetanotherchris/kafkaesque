using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace Kafkaesque.Api
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly string _topic = "kafkaesque_topic_1";
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            using (var builder = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                builder.Subscribe(_topic);
                Console.WriteLine($"Subscribed to topic {_topic}");

                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        var result = builder.Consume(cancelToken.Token);
                        Console.WriteLine($"Message: {result.Message.Value} received from {result.TopicPartitionOffset}");
                    }
                }
                catch (Exception)
                {
                    builder.Close();
                }
            }
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}