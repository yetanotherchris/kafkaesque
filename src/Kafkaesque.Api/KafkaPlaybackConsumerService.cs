using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace Kafkaesque.Api
{
    public class KafkaPlaybackConsumerService : BackgroundService
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
                GroupId = "KafkaPlaybackConsumerService",
                BootstrapServers = "localhost:9092",
                
                // If you don't set these, Kafka (or the .NET Client) appears
                // to make it impossible to reset the offset for your consumer group
                EnableAutoCommit = false,
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            
            using (var consumer = new ConsumerBuilder<Ignore, string>(conf)
                                        .Build())
            {
                try
                {
                    var partition = new TopicPartition(_topic, 0);
                    consumer.Assign(partition);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    consumer.Close();
                    return;
                }
                
                Console.WriteLine($"Assigned to topic {_topic}");
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        var result = consumer.Consume(cancelToken.Token);
                        Console.WriteLine($"Message: {result.Message.Value} received, offset: {result.TopicPartitionOffset.Offset}, partition: {result.TopicPartitionOffset.Partition}");
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