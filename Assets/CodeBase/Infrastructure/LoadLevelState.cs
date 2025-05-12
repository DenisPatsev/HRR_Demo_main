using CodeBase.Data.PersistentProgress;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadState<string>
    {
        private SceneLoader _sceneLoader;
        private IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private GameStateMachine _gameStateMachine;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory,
            IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter(string sceneName)
        {
            Debug.Log("Loading level: " + sceneName);
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
        }

        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_persistentProgressService.Progress);
            }
        }

        private void CreateHud()
        {
            GameObject hudPrefab =  _gameFactory.CreateHud();
            UIDocument hud = hudPrefab.GetComponent<UIDocument>();

            _gameStateMachine.GameBootstrapper.UIBootstrapper.Hud = hud;
        }

        private void CreatePlayer()
        {
            var initialPosition = GameObject.FindWithTag("InitialPointTag");
            Debug.Log(initialPosition.transform.position);
            _gameFactory.CreatePlayer(initialPosition.transform.position);
            Debug.Log("PlayerCreated");
        }
        
        private void EnableUIBootstrapper()
        {
            _gameStateMachine.GameBootstrapper.UIBootstrapper.gameObject.SetActive(true);
        }

        private void InitGameWorld()
        {
            CreatePlayer();
            EnableUIBootstrapper();
            CreateHud();
        }
    }
}