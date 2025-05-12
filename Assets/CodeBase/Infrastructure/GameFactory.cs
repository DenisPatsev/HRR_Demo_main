using System.Collections.Generic;
using CodeBase.AssetManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private IAssetProvider _assetProvider;
        private GameObject _player;
        
        public List<ISavedProgressReader> ProgressReaders { get;  }= new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get;  } = new List<ISavedProgress>();

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider; 
        }
        
        public GameObject CreatePlayer(Vector3 position)
        {
            _player = InstantiateRegistered(AssetPath.PlayerPath, position);
           
            return _player;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject prefab = _assetProvider.Instantiate(prefabPath, position);

            RegisterProgressWatchers(prefab);
            
            return prefab;
        }

        private void RegisterProgressWatchers(GameObject player)
        {
            foreach (ISavedProgressReader saveProgressReader in player.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(saveProgressReader);
            }
        }

        private void Register(ISavedProgressReader savedProgressReader)
        {
            if (savedProgressReader is ISavedProgress savedProgress)
            {
                ProgressWriters.Add(savedProgress);
            }
            
            ProgressReaders.Add(savedProgressReader);
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        public GameObject CreateHud()
        {
            InteractionChecker checker = _player.GetComponentInChildren<InteractionChecker>();
            NoiseChecker noiseChecker = _player.GetComponent<NoiseChecker>();

            var hud = InstantiateRegistered(AssetPath.HudPath, Vector3.zero);
            UIDocument hudDocument = hud.GetComponent<UIDocument>();
            noiseChecker.InitializeIndicator(hudDocument);
            checker.SetHud(hudDocument);
            return hud;
        }
    }
}
