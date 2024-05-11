using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://fxcjghbr:mtqXaN5pPGT7OTSMEF1KPij9boAM3fah@cougar.rmq.cloudamqp.com/fxcjghbr");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue: "example-queue", exclusive: false, autoDelete: false, durable: true);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false,consumer);

consumer.Received += Consumer_Received;

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
    //channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
   
    channel.BasicNack(deliveryTag: e.DeliveryTag, multiple: false, requeue: true);

}

Console.Read();