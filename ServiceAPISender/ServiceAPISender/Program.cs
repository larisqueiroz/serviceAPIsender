// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using ServiceAPISender.Models;

Dictionary<string, double> Products = new Dictionary<string, double>();
Products.Add("0", 2.00);
Products.Add("1", 7.72);
Products.Add("2", 35.00);
Products.Add("3", 5.00);
Products.Add("4", 32.00);
Products.Add("5", 25.00);
Products.Add("6", 42.00);
Products.Add("7", 40.00);

List<string> ProductNames = new List<string>()
{
    "Lápis", "Feijão", "Caderno", "Água", "Cacau em Pó", "Amaciante",
    "Azeite", "Chinelo"
};

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

while (true)
{
    Random random = new Random();

    int qty = random.Next(1, 7);
    List<ItemDto> itens = new List<ItemDto>();

    for (int i = 0; i < qty; i++)
    {
        var name = ProductNames[random.Next(0, 7)];
        var index = ProductNames.IndexOf(name).ToString();
        var item = new ItemDto()
        {
            Produto = name,
            Quantidade = random.Next(1, 10),
            Preco = Products[index]
        };
        var added = itens.Select(i => i.Produto);
        if (!added.Contains(item.Produto))
            itens.Add(item);
    }
    var message = new Message()
    {
        CodigoCliente = random.Next(1,100),
        CodigoPedido = random.Next(1, 99000),
        Itens = itens
    };

    var messageToSend = JsonSerializer.Serialize(message);

    var body = Encoding.UTF8.GetBytes(messageToSend);
    
    channel.BasicPublish(exchange: string.Empty, routingKey:"serviceAPI", basicProperties:null, body: body);
    
    Console.WriteLine("[NOVOENVIO] " + messageToSend);

    Thread.Sleep(120000);
}

