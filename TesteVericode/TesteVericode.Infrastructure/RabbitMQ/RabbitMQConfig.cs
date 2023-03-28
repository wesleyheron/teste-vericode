namespace TesteVericode.Infrastructure.RabbitMQ
{
    public class RabbitMQConfig
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
    }
}
