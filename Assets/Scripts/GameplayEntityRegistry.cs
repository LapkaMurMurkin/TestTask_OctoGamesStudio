using System.Collections.Generic;

namespace TestTask_OctoGames
{
    public class GameplayEntityRegistry
    {
        private readonly List<GameplayEntity> _entities = new();

        public IReadOnlyList<GameplayEntity> GetActiveEntities() => _entities.AsReadOnly();

        public void Register(GameplayEntity entity)
        {
            if (entity == null || _entities.Contains(entity))
                return;

            _entities.Add(entity);

            entity.OnActivated += OnEntityActivated;
            entity.OnDeactivated += OnEntityDeactivated;
            entity.OnDestroyed += OnEntityDestroyed;
        }

        private void OnEntityActivated(GameplayEntity entity)
        {
            if (!_entities.Contains(entity))
                _entities.Add(entity);
        }

        private void OnEntityDeactivated(GameplayEntity entity)
        {
            _entities.Remove(entity);
        }

        private void OnEntityDestroyed(GameplayEntity entity)
        {
            _entities.Remove(entity);

            // отписка на всякий случай
            entity.OnActivated -= OnEntityActivated;
            entity.OnDeactivated -= OnEntityDeactivated;
            entity.OnDestroyed -= OnEntityDestroyed;
        }
    }
}