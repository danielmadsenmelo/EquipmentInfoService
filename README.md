# EquipmentInfoService

This service is comprised of two microsservices: an Api and a Worker.

The Worker listens to kafka topic "test-equipment" and consumes two types of messages:
equipment status updates:
headers: header1=value1
{
    "eventType": 1,
    "equipmentId": "{{random.number(10)}}",
    "status": {{random.number(4)}},
    "sector": "A"
}

orders list updates:
headers: header1=value1
{
    "eventType": 2,
    "equipmentId": "{{random.number(10)}}",
    "currentOrders":[
        {
            "description": "first order",
            "orderId": "{{random.number(10)}}",
            "status": {{random.number(3)}}
        }
    ]
}

Both messages are published in the same topic.

To test the service:
1. Run docker compose to run a mongoDB and a kafka servers.
2. Create a topic named "test-equipment" in the kafka cluster.
2. Run Worker and publish messages.
3. Run Api and use Swagger documentation to consume persisted information using the GET request.

TIP: Use VS Code with package Tools for Apache Kafka to create topic and easily produce messages.