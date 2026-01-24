using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private Material _nubicSkin;
    [SerializeField] private SkinnedMeshRenderer[] _skinRenders;
    [SerializeField] private Sprite _Test;

    private void Start()
    {
        ChangeSkin(_Test);
    }

    public void ChangeSkin(Sprite newSkin)
    {
        SetSkin(newSkin);
    }

    private void SetSkin(Sprite skin)
    {
        Texture2D texture = ConvertSpriteToTexture(skin);

        Material newMaterial = new Material(_nubicSkin);
        newMaterial.mainTexture = texture;

        foreach (var renderer in _skinRenders)
            renderer.material = newMaterial;
    }

    private Texture2D ConvertSpriteToTexture(Sprite sprite)
    {
        // Создаем текстуру того же размера, что и спрайт
        Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);

        // Получаем пиксели из спрайта
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.rect.x,
            (int)sprite.rect.y,
            (int)sprite.rect.width,
            (int)sprite.rect.height
        );

        // Применяем пиксели к текстуре
        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
}
