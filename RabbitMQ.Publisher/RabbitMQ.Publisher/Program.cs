
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://fxcjghbr:mtqXaN5pPGT7OTSMEF1KPij9boAM3fah@cougar.rmq.cloudamqp.com/fxcjghbr");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue: "example-queue", exclusive: false,autoDelete:false,durable:true); //durable true mesajların kalıcı olması 

// properties persistent mesajları kalıcı hale getirir. rabbitmq sunucusunda problem olsa dahi 
IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

for (int i = 0; i < 10; i++)
{
    byte[] messages = Encoding.UTF8.GetBytes("Merhaba");
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: messages,basicProperties:properties);
}

Console.Read();