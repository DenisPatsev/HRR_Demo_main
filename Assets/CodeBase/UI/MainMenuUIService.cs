using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUIService : MonoBehaviour
{
    [SerializeField] private UIDocument _uiDocument;
    [SerializeField] private Color _highlightColor;

    private VisualElement _root;

    private Button _newGameButton;
    private Button _continueGameButton;
    private Button _exitGameButton;

    private Color _newGameStartButtonColor;
    private Color _continueStartGameButtonColor;
    private Color _exitGameStartButtonColor;

    private void Start()
    {
        _root = _uiDocument.rootVisualElement;

        InitializeUIElements();
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void InitializeUIElements()
    {
        _newGameButton = _root.Q<Button>("NewGame");
        _continueGameButton = _root.Q<Button>("Continue");
        _exitGameButton = _root.Q<Button>("Exit");

        _newGameStartButtonColor = _newGameButton.resolvedStyle.backgroundColor;
        _continueStartGameButtonColor = _continueGameButton.resolvedStyle.backgroundColor;
        _exitGameStartButtonColor = _exitGameButton.resolvedStyle.backgroundColor;
    }

    private void Subscribe()
    {
        _newGameButton.RegisterCallback<MouseOverEvent>(evt => { HighlightElement(_newGameButton); },
            TrickleDown.TrickleDown);
        _continueGameButton.RegisterCallback<MouseOverEvent>(evt => { HighlightElement(_continueGameButton); }, TrickleDown.TrickleDown);
        _exitGameButton.RegisterCallback<MouseOverEvent>(evt => { HighlightElement(_exitGameButton); }, TrickleDown.TrickleDown);
        
        _newGameButton.RegisterCallback<MouseOutEvent>(evt => { SetDefaultColor(_newGameButton, _newGameStartButtonColor); }, TrickleDown.TrickleDown);
        _continueGameButton.RegisterCallback<MouseOutEvent>(evt => { SetDefaultColor(_continueGameButton, _continueStartGameButtonColor); }, TrickleDown.TrickleDown);
        _exitGameButton.RegisterCallback<MouseOutEvent>(evt => { SetDefaultColor(_exitGameButton, _exitGameStartButtonColor); }, TrickleDown.TrickleDown);

        _newGameButton.clicked += StartGame;
        _exitGameButton.clicked += ExitGame;
    }

    private void Unsubscribe()
    {
        _newGameButton.UnregisterCallback<MouseOverEvent>(evt => { HighlightElement(_newGameButton); },
            TrickleDown.TrickleDown);
        _continueGameButton.UnregisterCallback<MouseOverEvent>(evt => { HighlightElement(_continueGameButton); }, TrickleDown.TrickleDown);
        _exitGameButton.UnregisterCallback<MouseOverEvent>(evt => { HighlightElement(_exitGameButton); }, TrickleDown.TrickleDown);
        
        _newGameButton.UnregisterCallback<MouseOutEvent>(evt => { SetDefaultColor(_newGameButton, _newGameStartButtonColor); }, TrickleDown.TrickleDown);
        _continueGameButton.UnregisterCallback<MouseOutEvent>(evt => { SetDefaultColor(_continueGameButton, _continueStartGameButtonColor); }, TrickleDown.TrickleDown);
        _exitGameButton.UnregisterCallback<MouseOutEvent>(evt => { SetDefaultColor(_exitGameButton, _exitGameStartButtonColor); }, TrickleDown.TrickleDown);
        
        _newGameButton.clicked -= StartGame;
        _exitGameButton.clicked -= ExitGame;
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Initial");
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void HighlightElement(VisualElement visualElement)
    {
        visualElement.style.backgroundColor = _highlightColor;
    }

    private void SetDefaultColor(VisualElement visualElement, Color color)
    {
        visualElement.style.backgroundColor = color;
    }
}