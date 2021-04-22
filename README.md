# kafkaesque
Fun with Kafka

## Getting started

1. Install Docker desktop
2. run `docker-compose up` in the root directory
3. Wait 10 minutes
4. `dotnet run` in the API project.
5. Navigate to http://localhost:8000

### Tasks

- [X] Create a docker-compose file for Kafka and its DB
- [X] Create an API endpoint that posts messages to a topic
- Create a service that listens for messages on a topic
- [X] Find a GUI for Kafka
- Try a connector out (Dynamo?): https://docs.confluent.io/home/connect/kafka_connectors.html
- Replay all messages in a topic
- Streams in C#
- Look at Brighter if there's time

### Keys and partitions

https://stackoverflow.com/questions/29511521/is-key-required-as-part-of-sending-messages-to-kafka

Better detail: https://medium.com/event-driven-utopia/understanding-kafka-topic-partitions-ae40f80552e8

> Although the messages within a partition are ordered, messages across a topic are not guaranteed to be ordered.
> Reading records from partitions.
> Unlike the other pub/sub implementations, Kafka doesn’t push messages to consumers. Instead, consumers have to pull messages off Kafka topic partitions. A consumer connects to a partition in a broker, reads the messages in the order in which they were written.

> The offset of a message works as a consumer side cursor at this point. The consumer keeps track of which messages it has already consumed by keeping track of the offset of messages. After reading a message, the consumer advances its cursor to the next offset in the partition and continues. Advancing and remembering the last read offset within a partition is the responsibility of the consumer. Kafka has nothing to do with it.

### References

- Intro: https://kafka.apache.org/intro
- Design: https://docs.confluent.io/platform/current/kafka/design.html
- [Redgate tutorial](https://www.red-gate.com/simple-talk/dotnet/net-development/using-apache-kafka-with-net/)
- Topic manager GUI : https://github.com/conduktor/kafka-stack-docker-compose
- Kafka GUI: https://github.com/tchiotludo/akhq#docker (but doesn't open 9092 port)