using UnityEngine;
using UnityEngine.InputSystem;


public class CheatController : MonoBehaviour
{

    private string _currentInput;
    [SerializeField] private float _inputTimeToLive = 15;
    [SerializeField] private CheatsItem[] _cheats;
    private float _inputTime;

    private void Awake()
    {
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char InputChar)
    {
        _currentInput += InputChar;
        _inputTime = _inputTimeToLive;
        FindAnyCheats();
    }

    private void FindAnyCheats()
    {
        foreach (var cheat in _cheats)
        {
            if (_currentInput.Contains(cheat.Name))
            {
                cheat.Action?.Invoke();
                _currentInput = string.Empty;
            }
        }
    }

    private void Update()
    {
        if (_inputTime < 0)
        {
            _currentInput = string.Empty;
        }
        else
        {
            _inputTime -= Time.deltaTime;
        }
    }
}
