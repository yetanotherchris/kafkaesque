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

### Keys

https://stackoverflow.com/questions/29511521/is-key-required-as-part-of-sending-messages-to-kafka

Keys are used for denoting a partition of the message, and state machines.

### References

- Intro: https://kafka.apache.org/intro
- Design: https://docs.confluent.io/platform/current/kafka/design.html
- [Redgate tutorial](https://www.red-gate.com/simple-talk/dotnet/net-development/using-apache-kafka-with-net/)
- Topic manager: https://github.com/conduktor/kafka-stack-docker-compose
- Kafka GUI: https://github.com/tchiotludo/akhq#docker (but doesn't open 9092 port)