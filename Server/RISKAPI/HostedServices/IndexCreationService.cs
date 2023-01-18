using ModelsAPI;
using ModelsAPI.ClassMetier.GameStatus;
using ModelsAPI.ClassMetier.Map;
using ModelsAPI.ClassMetier.Player;
using Redis.OM;
using Redis.OM.Modeling;

namespace RISKAPI.HostedServices
{
    public class IndexCreationService : IHostedService
    {
        private readonly RedisConnectionProvider _provider;
        public IndexCreationService(RedisConnectionProvider provider)
        {
            _provider = provider;
            DocumentAttribute.RegisterIdGenerationStrategy(nameof(StaticIncrementStrategy), new StaticIncrementStrategy());
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Vérifie si l'annulation a été demandée avant de continuer
            cancellationToken.ThrowIfCancellationRequested();

            await _provider.Connection.CreateIndexAsync(typeof(Profil));
            await _provider.Connection.CreateIndexAsync(typeof(Carte));
            await _provider.Connection.CreateIndexAsync(typeof(Lobby));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
