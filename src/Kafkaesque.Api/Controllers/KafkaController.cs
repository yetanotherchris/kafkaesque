using System;
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
        private readonly string topic = "kafkaesque_topic_1";
        
        [HttpPost]
        public IActionResult Post([FromQuery] string message)
        {
            return Created(string.Empty, SendToKafka(topic, message));
        }
        
        private Object SendToKafka(string topic, string message)
        {
            using (var producer = 
                 new ProducerBuilder<string, string>(_config).Build())
            {
                try
                {
                    return producer.ProduceAsync(topic, new Message<string, string>
                        {
                            Key = "foo",
                            Value = message
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
