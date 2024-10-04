// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using ServiceAPISender.Models;

Dictionary<string, double> Products = new Dictionary<string, double>();
Products.Add("Lápis", 2.00);
Products.Add("Feijão", 7.72);
Products.Add("Caderno", 35.00);
Products.Add("Água", 5.00);
Products.Add("Cacau em Pó", 32.00);
Products.Add("Amaciante", 25.00);
Products.Add("Azeite", 42.00);
Products.Add("Chinelo", 40.00);

IConnection? connection = null;
IModel channel = null;

var factory = new ConnectionFactory
{
    HostName = "rabbitmq",
    Port = 5672,
    UserName = "guest",
    Password = "guest",
    ClientProvidedName = "serviceAPI",
    RequestedHeartbeat = TimeSpan.FromMinutes(1)
};

while (true)
{
    try
    {
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
    }
    catch (ConnectFailureException e)
    {
        Console.WriteLine("Tentativa de conexão falhou.");
    }
    
    if (channel is null)
        continue;
    break;
}

channel.QueueDeclare(queue: "serviceAPI", durable: default, exclusive: false, autoDelete: false, arguments: null);

// TODO: LÓGICA DE MONTAGEM DE PACOTE
//while (true)
//{
    var message = new Message()
    {
        CodigoCliente = 1,
        CodigoPedido = 1,
        Itens = new List<ItemDto>()
        {
            new ItemDto()
            {
                Preco = 2,
                Produto = "lapis",
                Quantidade = 5
            }
        }
    };

    var messageToSend = JsonSerializer.Serialize(message);

    var body = Encoding.UTF8.GetBytes(messageToSend);
    
    channel.BasicPublish(exchange: string.Empty, routingKey:"serviceAPI", basicProperties:null, body: body);
    
    Console.WriteLine("[NOVOENVIO] " + messageToSend);
//}

