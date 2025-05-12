using UnityEngine;

namespace CodeBase.Data
{
    public class PlayerHealth : MonoBehaviour, ISavedProgress
    {
        private State _state;
        
        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.PlayerState;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.PlayerState.CurrentHP = _state.CurrentHP;
            progress.PlayerState.MaxHP = _state.MaxHP;
        }

        public void TakeDamage(float damage)
        {
            if(_state.CurrentHP <= 0)
                return;
            
            _state.CurrentHP -= damage;
        }
    }
}