using DG.Tweening;
using SanyaBeerExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiMenuWindow : Window
{

}

public class Window : MonoBehaviour
{
    [SerializeField] protected GameObject container;

    [Header("Fade")]
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected float fadeDuration;
    [SerializeField] protected float unfadeDuration;

    private Tween _tween;

    public void Show()
    {
        _tween?.Kill();

        container.ActiveSelf();
    }

    public void ShowWithUnfade()
    {
        _tween?.Kill();

        _tween = canvasGroup
            .DOFade(1, unfadeDuration)
            .OnComplete(Show);
    }

    public void Hide()
    {
        _tween?.Kill();

        container.DisactiveSelf();
    }

    public void HideWithFade()
    {
        _tween?.Kill();

        _tween = canvasGroup
            .DOFade(0, fadeDuration)
            .OnComplete(Hide);
    }
}

public class WindowSwitcher
{
    private readonly Dictionary<Type, Window> _windows;

    public WindowSwitcher(Window[] windows)
    {
        foreach (var window in windows)
            _windows.Add(window.GetType(), window);
    }

    public void Switch<TOld, TNew>(bool isOldFade = false, bool isNewUnfade = false)
        where TOld : Window
        where TNew : Window
    {
        if (_windows.TryGetValue(typeof(TOld), out Window oldWindow) == false)
            throw new System.Exception("WTF");

        if (_windows.TryGetValue(typeof(TNew), out Window newWindow) == false)
            throw new System.Exception("WTF");

        if (isOldFade == true)
            oldWindow.HideWithFade();
        else
            oldWindow.Hide();

        if (isNewUnfade == true)
            newWindow.HideWithFade();
        else
            newWindow.Hide();
    }
}