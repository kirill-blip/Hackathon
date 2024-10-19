using System.Collections;

using TMPro;

using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _newsText;
    [SerializeField] private float _timeToDisappear;

    private void Awake()
    {
        _newsText.gameObject.SetActive(false);

        Broadcast.Subscribe<NPCDestoroyed>(OnNPCDestoroyedHandler);
    }

    private void OnDestroy()
    {
        Broadcast.Unsubscribe<NPCDestoroyed>(OnNPCDestoroyedHandler);
    }

    private void OnNPCDestoroyedHandler(NPCDestoroyed destoroyed)
    {
        string message = destoroyed.Message.Replace("(Clone)", "");
        _newsText.text = $"Упс. {message} сбили";
        StartCoroutine(ActivateNewsText());
    }

    private IEnumerator ActivateNewsText()
    {
        _newsText.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(_timeToDisappear);

        _newsText.gameObject.SetActive(false);
    }
}
