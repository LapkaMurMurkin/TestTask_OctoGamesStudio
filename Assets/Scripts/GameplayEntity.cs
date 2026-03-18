using System;

using UnityEngine;

namespace TestTask_OctoGames
{
    public class GameplayEntity : MonoBehaviour
    {
        public event Action<GameplayEntity> OnActivated;
        public event Action<GameplayEntity> OnDeactivated;
        public event Action<GameplayEntity> OnDestroyed;

        public bool IsActive => gameObject.activeInHierarchy;

        private void OnEnable()
        {
            OnActivated?.Invoke(this);
        }

        private void OnDisable()
        {
            OnDeactivated?.Invoke(this);
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}