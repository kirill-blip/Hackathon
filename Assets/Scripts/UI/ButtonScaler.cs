using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _scale = 1.1f;
    [SerializeField] private float _time = 0.1f;

    private Vector3 _originalScale;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_originalScale * _scale, _time).SetEase(Ease.InOutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_originalScale, _time).SetEase(Ease.InOutBack);
    }
}
