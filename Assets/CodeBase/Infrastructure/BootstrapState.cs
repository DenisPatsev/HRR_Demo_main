using System;
using CodeBase.AssetManagement;
using CodeBase.Data.PersistentProgress;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState
    {
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private AllServices _allServices;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _allServices = allServices;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load("Initial", OnSceneLoaded);
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _allServices.RegisterSingle<IAssetProvider>(new AssetProvider());
            _allServices.RegisterSingle<IGameFactory>(new GameFactory(_allServices.Single<IAssetProvider>()));
            _allServices.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _allServices.RegisterSingle<ISaveLoadService>(new SaveLoadService( _allServices.Single<IGameFactory>(), _allServices.Single<IPersistentProgressService>()));
        }

        private void OnSceneLoaded()
        {
            _gameStateMachine.Enter<LoadProgressState>();
            Debug.Log("OnSceneLoaded");
        }
    }
}