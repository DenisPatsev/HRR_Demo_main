using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game : MonoBehaviour
    {
        public GameStateMachine GameStateMachine;
        
        private SceneLoader _sceneLoader;

        public Game(GameBootstrapper gameBootstrapper)
        {
            _sceneLoader = new SceneLoader(gameBootstrapper);
            GameStateMachine = new GameStateMachine(gameBootstrapper, _sceneLoader, AllServices.Container);
        }
    }
}