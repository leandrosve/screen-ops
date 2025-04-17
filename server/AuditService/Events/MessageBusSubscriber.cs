
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AuditService.Events
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IEventProcessor _eventProcessor;
        private readonly IConfiguration _configuration;
        private IConnection _connection = null!;
        private IChannel _channel = null!;
        private ILogger<MessageBusSubscriber> _logger;
        private string _queueName = "audit-logs";
        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor, ILogger<MessageBusSubscriber> logger)
        {

            _eventProcessor = eventProcessor;
            _configuration = configuration;
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            try { 
            var host = _configuration.GetValue<string>("RabbitMQ:Host") ?? "localhost";
            var port = int.Parse(_configuration.GetValue("RabbitMQ:Port", "5672"));
            _queueName = _configuration.GetValue("RabbitMQ:QueueName", "audit-logs");

            var factory = new ConnectionFactory() { HostName = host, Port = port };

            _connection = await factory.CreateConnectionAsync(cancellationToken);
            _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
            await _channel.QueueDeclareAsync(queue: _queueName,
                cancellationToken: cancellationToken,
                durable: false,
                exclusive: false,
                autoDelete: false,

                arguments: null);
            await _channel.QueueBindAsync(cancellationToken: cancellationToken, queue: _queueName, exchange: "amq.direct", routingKey: "audit-logs");
            _connection.ConnectionShutdownAsync += OnConnectionShutdown;

             _logger.LogInformation("[MessageBusSubscriber] Listening on the Message Bus");

            } catch (Exception e)
            {
                _logger.LogError($"[MessageBusSubscriber] Error on Start Async: {e}");
                throw;

            }
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("[MessageBusSubscriber] Execute ASync");

            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += HandleAsync;

            await _channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);
        }

        private async Task HandleAsync(object moduleHandler, BasicDeliverEventArgs ea)
        {
            _logger.LogInformation("[MessageBusSubscriber] Event received");
            var body = ea.Body;

            var message = Encoding.UTF8.GetString(body.ToArray());

            await _eventProcessor.ProcessEvent(message);
        }

        private async Task OnConnectionShutdown(object sender, ShutdownEventArgs args)
        {
            _logger.LogWarning($"RabbitMQ -> Conexión cerrada: {args.ReplyText}");
            if (_channel.IsOpen)
            {
                await _channel.CloseAsync();
                await _connection.CloseAsync();
            }
        }

        public async override void Dispose()
        {
            if (_channel.IsOpen)
            {
                await _channel.CloseAsync();
                await _connection.CloseAsync();
            }
            base.Dispose();
        }
    }
}
