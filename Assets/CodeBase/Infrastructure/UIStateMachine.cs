using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure
{
    public class UIStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;
        
        private UIBootstrapper _bootstrapper;
        public UIBootstrapper UiBootstrapper => _bootstrapper;

        public UIStateMachine(UIBootstrapper uiBootstrapper)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(MainUIState)] = new MainUIState(uiBootstrapper),
                [typeof(PauseState)] = new PauseState(uiBootstrapper)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
           IState state = ChangeState<TState>();
           
           state.Enter();
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