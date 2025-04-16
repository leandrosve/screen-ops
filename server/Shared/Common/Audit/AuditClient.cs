 using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace Common.Audit
{
    public class AuditClient : IAuditClient
    {

        private IConnection _connection = null!;
        private IChannel _channel = null!;
        private readonly ILogger<AuditClient> _logger = null!;

        private readonly AuditClientConfig _config;

        public AuditClient(IOptions<AuditClientConfig> config, ILogger<AuditClient> logger)
        {
            _config = config.Value;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = _config.HostName,
                Port = _config.Port,
                AutomaticRecoveryEnabled = true
            };

            try
            {
                _connection = await factory.CreateConnectionAsync();
                _connection.ConnectionShutdownAsync += OnConnectionShutdown;

                _channel = await _connection.CreateChannelAsync();

                await _channel.QueueDeclareAsync(
                    queue: _config.QueueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                await _channel.QueueBindAsync(queue: _config.QueueName, exchange: "amq.direct", routingKey: "audit-logs");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"RabbitMQ -> Error al conectar: {ex.Message}");
            }
        }

        private async Task OnConnectionShutdown(object sender, ShutdownEventArgs args)
        {
            _logger.LogWarning($"RabbitMQ -> Conexión cerrada: {args.ReplyText}");
            if (_channel.IsOpen) { 
                await _channel.CloseAsync();
                await _connection.CloseAsync();
            }
        }

        public async Task Log(AuditLogDto log)
        {
            var message = "";
            try
            {
                message = JsonSerializer.Serialize(log);
                var body = System.Text.Encoding.UTF8.GetBytes(message);
                var properties = new BasicProperties
                {
                    Persistent = true
                };
                if (_connection.IsOpen)
                {
                    _logger.LogInformation($"Sending Audit Log");

                    await _channel.BasicPublishAsync(exchange: "amq.direct",  routingKey: "audit-logs", mandatory: false,
                         basicProperties: properties, body: body);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while sending Audit Log: {message} - Exception: {ex}");
            }
        }


    }
}
