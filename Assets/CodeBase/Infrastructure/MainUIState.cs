using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class MainUIState : IState
    {
        private UIActionsService _uiActionsService;
        private UIBootstrapper _uiBootstrapper;
        
        public MainUIState(UIBootstrapper UiBootstrapper)
        {
            _uiBootstrapper = UiBootstrapper;
            _uiActionsService = UiBootstrapper.UiActionsService;
        }
        
        public void Enter()
        {
            _uiBootstrapper.StartCoroutine(InitializeCompass());
        }

        private IEnumerator InitializeCompass()
        {
            while(_uiBootstrapper.Hud == null)
                yield return null;
            
            _uiActionsService.Compass.InitializeCompass(_uiBootstrapper.Hud);
            Debug.Log("Compass initialized");

        }

        public void Exit()
        {
            
        }
    }
}