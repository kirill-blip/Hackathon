using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private TextMeshProUGUI _newsText;
    [SerializeField] private TextMeshProUGUI _npcInteractText;
    [SerializeField] private float _timeToDisappear;

    [Header("Mobile Input")]
    [SerializeField] private FixedJoystick _joystickMovement;
    [SerializeField] private FixedJoystick _joystickRotation;
    [SerializeField] private Button _interactButton;

    private void Awake()
    {
        _newsText.gameObject.SetActive(false);

        _joystickMovement.gameObject.SetActive(Application.isMobilePlatform);
        _joystickRotation.gameObject.SetActive(Application.isMobilePlatform);
        _interactButton.gameObject.SetActive(Application.isMobilePlatform);

        _interactButton.onClick.AddListener(OnInteractButtonHandler);
        _exitButton.onClick.AddListener(OnExitButtonHandler);

        Broadcast.Subscribe<NPCDestoroyedMessage>(OnNPCDestoroyedHandler);
        Broadcast.Subscribe<NPCInteractedMessage>(OnNPCInteractedHandler);
    }

    private void OnDestroy()
    {
        Broadcast.Unsubscribe<NPCDestoroyedMessage>(OnNPCDestoroyedHandler);
        Broadcast.Unsubscribe<NPCInteractedMessage>(OnNPCInteractedHandler);
    }

    private void OnExitButtonHandler()
    {
        SceneManager.LoadScene(0);
    }

    private void OnInteractButtonHandler()
    {
        Broadcast.Send(new InteractMessage());
    }

    private void OnNPCInteractedHandler(NPCInteractedMessage message)
    {
        StartCoroutine(ActivateText(_npcInteractText));
    }

    private void OnNPCDestoroyedHandler(NPCDestoroyedMessage destoroyed)
    {
        string message = destoroyed.Message.Replace("(Clone)", "");
        _newsText.text = $"Упс. {message} сбили";
        StartCoroutine(ActivateText(_newsText));
    }

    private IEnumerator ActivateText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);

        yield return new WaitForSeconds(_timeToDisappear);

        text.gameObject.SetActive(false);
    }
}
