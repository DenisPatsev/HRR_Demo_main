using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure
{
    public class UIActionsService : MonoBehaviour
    {
        public UIBootstrapper UiBootstrapper;

        public GameCompass Compass;
        public UIDocument _pauseDocument;

        private int _pausePressCount;

        private void Start()
        {
            _pausePressCount = 0;
        }

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.Escape))
        //     {
        //         _pausePressCount++;
        //         SetPauseState();
        //     }
        // }

        public void PressEscapeButton()
        {
            _pausePressCount++;
            SetPauseState();
        }

        private void SetPauseState()
        {
            if (_pausePressCount % 2 != 0)
            {
                _pauseDocument.gameObject.SetActive(true);
                UiBootstrapper.UIStateMachine.Enter<PauseState>();
            }
            else
            {
                _pauseDocument.gameObject.SetActive(false);
                UiBootstrapper.UIStateMachine.Enter<MainUIState>();
            }
        }
    }
}