using System;
using System.Collections.Generic;
using CodeBase.Data.PersistentProgress;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using Unity.VisualScripting;

namespace CodeBase.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;
        
        private GameBootstrapper _bootstrapper;
        public GameBootstrapper GameBootstrapper => _bootstrapper;

        public GameStateMachine(GameBootstrapper gameBootstrapper, SceneLoader sceneLoader, AllServices allServices)
        {
            _bootstrapper = gameBootstrapper;
            
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, allServices),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, allServices.Single<IGameFactory>(), allServices.Single<IPersistentProgressService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, allServices.Single<IPersistentProgressService>(), allServices.Single<ISaveLoadService>()),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();

            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}