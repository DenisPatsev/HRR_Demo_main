using System;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public State PlayerState;
        
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            PlayerState = new State();
        }
    }

    [Serializable]
    public class State
    {
        public float CurrentHP;
        public float MaxHP;
        
        public void ResetHP()
        {
            CurrentHP = MaxHP;
        }
    }
}