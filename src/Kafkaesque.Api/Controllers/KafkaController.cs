using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace Kafkaesque.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KafkaController : ControllerBase
    {
        private readonly ProducerConfig _config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };
        private readonly string _topic = "kafkaesque_topic_1";
        
        [HttpPost]
        [Route("Send")]
        public IActionResult Send([FromQuery] string message)
        {
            return Created(string.Empty, SendToKafka(_topic, message));
        }
        
        [HttpPost]
        [Route("SendMany")]
        public IActionResult SendMany([FromQuery] string title, int count)
        {
            var list = new List<object>();
            for (int i = 0; i < count; i++)
            {
                object result = SendToKafka(_topic, $"{title} ({i} of {count})");
                list.Add(result);
            }

            return Created(string.Empty, list);
        }
        
        private Object SendToKafka(string topic, string title)
        {
            var message = new KafkaEsqueMessage()
            {
                Id = Guid.NewGuid(),
                TimeSent = DateTime.UtcNow,
                Title = title
            };
            string json = JsonSerializer.Serialize(message);
            
            // Note: Null is is a Confluent.Kafka.Null
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                try
                {
                    return producer.ProduceAsync(topic, new Message<Null, string>
                        {
                            Value = json
                        })
                        .GetAwaiter()
                        .GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oops, something went wrong: {e}");
                }
            }
            return null;
        }
    }
}
