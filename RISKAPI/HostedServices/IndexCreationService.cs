using Microsoft.Graph;
using ModelsAPI.ClassMetier;
using Redis.OM;

namespace RISKAPI.HostedServices
{
    public class IndexCreationService : IHostedService
    {
        private readonly RedisConnectionProvider _provider;
        public IndexCreationService(RedisConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Vérifie si l'annulation a été demandée avant de continuer
            cancellationToken.ThrowIfCancellationRequested();

            await _provider.Connection.CreateIndexAsync(typeof(Profil));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
