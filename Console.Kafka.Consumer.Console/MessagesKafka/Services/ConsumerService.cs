using Confluent.Kafka;
using Console.Kafka.Consumer.Console.MessagesKafka.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApp.Kafka.Infrastructure.MessageKafka.Serializador;

namespace Console.Kafka.Consumer.Console.MessagesKafka.Services
{
    public class ConsumerService<T> : BackgroundService
    {
        private readonly ILogger<ConsumerService<T>> _logger;
        private readonly ConsumerConfig _consumerConfig;
        private readonly IConsumer<Ignore, T> _consumer;

        public ConsumerService(ILogger<ConsumerService<T>> logger)
        {
            _logger = logger;

            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = ParametersConfig.BOOTSTRAP_SERVER,
                // Definindo um grupo de consumer para ser usado quando for inscrever na fila do Kafka
                GroupId = ParametersConfig.GROUP_ID,
                // Define que a mensagem quando for consumida foi lida
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            // Inicializando a classe consumer com as configurações passada acima
            _consumer = new ConsumerBuilder<Ignore, T>(_consumerConfig)
                .SetValueDeserializer(new DeserializerConsumer<T>())                                                
                .Build();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Iniciando serviço de recebimento da mensagem");

            // Método responsável por se inscrever na fila do Kafka para observar a fila passada no parametro e consumir a mensagem quando tiver
            _consumer.Subscribe(ParametersConfig.TOPIC_NAME);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() =>
                {
                    var result = _consumer.Consume(stoppingToken);

                    _logger.LogInformation($"GroupId: {ParametersConfig.GROUP_ID} - {result.Message.Value.ToString()}");
                });
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            _logger.LogInformation($"Aplicação parou, conexão fechada");
            return Task.CompletedTask;
        }
    }
}
