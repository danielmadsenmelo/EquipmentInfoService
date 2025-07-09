using Confluent.Kafka;

namespace Workers.Installers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InstallWorkers(this IServiceCollection services, CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:<PLAINTEXT PORTS>",
                GroupId = "equipment-info-service-worker",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            const string topic = "equipment-info-updates";

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(topic);

                while (!cancellationToken.IsCancellationRequested)
                {

                    var consumeResult = consumer.Consume(cancellationToken);
                    if (consumeResult.IsPartitionEOF)
                    {
                        continue; // Skip end of partition messages
                    }
                }

                consumer.Close();
            }

            return services;
        }
    }
}
