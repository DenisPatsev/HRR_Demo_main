using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure
{
    public class PauseState : IState
    {
        private UIActionsService _uiActionsService;
        private UIBootstrapper _uiBootstrapper;
        
        private Button _continueButton;
        private Button _exitButton;
        private VisualElement _root;

        public PauseState(UIBootstrapper UiBootstrapper)
        {
            _uiBootstrapper = UiBootstrapper;
            _uiActionsService = UiBootstrapper.UiActionsService;
        }

        public void Enter()
        {
            _root = _uiActionsService._pauseDocument.rootVisualElement;
            Debug.Log("EnterPause");
            Subscribe();
        }

        private void Subscribe()
        {
            _continueButton = _root.Q<Button>("Continue");
            _exitButton = _root.Q<Button>("Exit");
            
            Debug.LogError(_continueButton);

            _continueButton.clicked += OnContinueButtonClicked;
            _exitButton.clicked += ExitToMainMenu;
        }

        private void OnContinueButtonClicked()
        { 
            Debug.Log("OnContinueButtonClicked");
            _uiActionsService.PressEscapeButton();
        }

        private void Unsubscribe()
        {
            _continueButton.clicked -= _uiActionsService.PressEscapeButton;
            _exitButton.clicked -= ExitToMainMenu;
        }

        private void ExitToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Exit()
        {
            Unsubscribe();
        }
    }
}