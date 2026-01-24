using SanyaBeerExtension;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _lockIcon;

    [field: SerializeField] public Sprite Skin;

    public void Select()
    {

    }

    public void HideLockIcon()
    {
        _lockIcon.DisactiveSelf();
    }
}