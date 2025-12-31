using DG.Tweening;
using SanyaBeerExtension;
using UnityEngine;

public class MovingAsOffsetAnimation : MonoBehaviour
{
    [field: SerializeField] public ParametrBase<Transform> _entity;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private bool _isAutoStart = false;

    private void Start()
    {
        if (_isAutoStart == true)
            Animate();
    }

    public void Animate()
    {
        Vector3 initial = _entity.Source.localPosition;
        Vector3 target = initial + _offset;

        Sequence animationSequence = DOTween.Sequence()
            .Append(_entity.Source
                .DOLocalMove(target, _entity.Duration)
                .SetEase(_entity.Ease))
            .Append(_entity.Source
                .DOLocalMove(initial, _entity.Duration)
                .SetEase(_entity.Ease))
            .SetLoops(-1, LoopType.Restart) // Бесконечное повторение
            .SetAutoKill(false); // Не уничтожать автоматически

        // Привязываем к уничтожению объекта
        animationSequence.SetLink(gameObject);
    }
}