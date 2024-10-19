using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    [Space(10f)]
    [SerializeField] private TextMeshProUGUI _versionText;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartButtonHandler);
        _exitButton.onClick.AddListener(OnExitButtonHandler);

        _versionText.text = $"{Application.version}";
    }

    private void OnStartButtonHandler()
    {
        SceneManager.LoadScene(1);
    }

    private void OnExitButtonHandler()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else 
            Application.Quit();
        #endif
    }
}
