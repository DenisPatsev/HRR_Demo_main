using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Infrastructure
{
    public class UIBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public UIDocument Hud;
        public UIActionsService UiActionsService;
        
        private UIStateMachine _uiStateMachine;
        public UIStateMachine UIStateMachine => _uiStateMachine;

        public void Awake()
        {
            _uiStateMachine = new UIStateMachine(this);

            _uiStateMachine.Enter<MainUIState>();
        }
    }
}