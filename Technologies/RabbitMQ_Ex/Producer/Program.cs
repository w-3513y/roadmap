﻿using System;
using System.Text;
using RabbitMQ.Client;

namespace Technologies.RabbitMQ_Ex.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: $"send message: {DateTime.Now}",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                int count = 0;
                while (true)
                {
                    string message = $"{count++} - Testando uma estrutura Producer x Consumer!";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                    System.Threading.Thread.Sleep(400);
                }
            }
        }

    }
}