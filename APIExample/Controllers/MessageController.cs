using APIExample.Models;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;

namespace APIExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost()]
        public IActionResult Post([FromForm]User model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://qwrxqkei:CzmI2f2m5Gr0OcxIaS3U2j91KsuoQKqu@sparrow.rmq.cloudamqp.com/qwrxqkei");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("messageQueue", false, false, false);

            string serializeData = JsonSerializer.Serialize(model);

            byte[] data = Encoding.UTF8.GetBytes(serializeData);
            channel.BasicPublish("", "messageQueue", body: data);

            return Ok();
        }
    }
}
