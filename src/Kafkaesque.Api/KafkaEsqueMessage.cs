using System;

namespace Kafkaesque.Api
{
    public class KafkaEsqueMessage
    {
        public Guid Id { get; set; }
        public DateTime TimeSent { get; set; }
        public string Title { get; set; }
    }
}