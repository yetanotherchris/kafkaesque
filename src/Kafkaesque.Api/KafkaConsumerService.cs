using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace Kafkaesque.Api
{
    public class KafkaConsumerService : BackgroundService
    {
        private readonly string _topic = "kafkaesque_topic_1";
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(Poll, stoppingToken);
        }

        public void Poll()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "st_consumer_group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            using (var consumer = new ConsumerBuilder<Ignore, string>(conf)
                                        .Build())
            {
                consumer.Subscribe(_topic);
                Console.WriteLine($"Subscribed to topic {_topic}");

                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        var result = consumer.Consume(cancelToken.Token);
                        Console.WriteLine($"Message: {result.Message.Value} received from {result.TopicPartitionOffset}");
                    }
                }
                catch (Exception)
                {
                    consumer.Close();
                }
            }
        }
        
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}