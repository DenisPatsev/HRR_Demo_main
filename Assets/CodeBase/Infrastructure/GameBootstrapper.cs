using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public UIBootstrapper UIBootstrapper;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            _game.GameStateMachine.Enter<BootstrapState>();
        }
    }
}