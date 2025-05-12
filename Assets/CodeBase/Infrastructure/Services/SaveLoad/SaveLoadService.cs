using CodeBase.Data;
using CodeBase.Data.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;

        public SaveLoadService(IGameFactory gameFactory, IPersistentProgressService persistentProgressService)
        {
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
        }
        
        public PlayerProgress LoadProgress()
        {
          return PlayerPrefs.GetString("Progress")?.ToDeserialized<PlayerProgress>();
        }

        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters)
            {
                progressWriter.UpdateProgress(_persistentProgressService.Progress);
            }
            
            PlayerPrefs.SetString(ProgressKey, _persistentProgressService.Progress.ToJson());
        }
    }
}