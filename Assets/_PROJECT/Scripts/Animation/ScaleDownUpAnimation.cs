using DG.Tweening;
using SanyaBeerExtension;
using UnityEngine;

public class ScaleDownUpAnimation : MonoBehaviour
{
    [field: SerializeField] public ParametrBase<Transform> _entity;
    [SerializeField] private Vector3 _upScale;
    [SerializeField] private Vector3 _downScale;

    [SerializeField] private bool _isAutoStart = false;

    private void Start()
    {
        if (_isAutoStart == true)
            Animate();
    }

    public void Animate()
    {
        Sequence animationSequence = DOTween.Sequence()
            .Append(_entity.Source
                .DOScale(_downScale, _entity.Duration)
                .SetEase(_entity.Ease))
            .Append(_entity.Source
                .DOScale(_upScale, _entity.Duration)
                .SetEase(_entity.Ease))
            .SetLoops(-1, LoopType.Restart) // Бесконечное повторение
            .SetAutoKill(false); // Не уничтожать автоматически

        // Привязываем к уничтожению объекта
        animationSequence.SetLink(gameObject);
    }
}
