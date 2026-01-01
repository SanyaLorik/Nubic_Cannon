using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

public class WindowSwitcher
{
    private readonly Dictionary<Type, Window> _windows;

    public WindowSwitcher(IEnumerable<Window> windows)
    {
        _windows = new();
        foreach (var window in windows)
            _windows.Add(window.GetType(), window);
    }

    public (TOld, TNew) Switch<TOld, TNew>(bool isOldFade = false, bool isNewUnfade = false)
        where TOld : Window
        where TNew : Window
    {
        if (_windows.TryGetValue(typeof(TOld), out Window oldWindow) == false)
            throw new System.Exception("WTF");

        if (_windows.TryGetValue(typeof(TNew), out Window newWindow) == false)
            throw new System.Exception("WTF");

        if (isOldFade == true)
            oldWindow.HideWithFade().Forget();
        else
            oldWindow.Hide();

        if (isNewUnfade == true)
            newWindow.ShowWithUnfade().Forget();
        else
            newWindow.Show();

        return ((TOld)oldWindow, (TNew)newWindow);
    }

    public void Show<TWindow>()
        where TWindow : Window
    {
        if (_windows.TryGetValue(typeof(TWindow), out Window window) == false)
            throw new System.Exception("WTF");

        window.Show();
    }

    public void ShowWithUnfade<TWindow>()
        where TWindow : Window
    {
        if (_windows.TryGetValue(typeof(TWindow), out Window window) == false)
            throw new System.Exception("WTF");

        window.ShowWithUnfade().Forget();
    }

    public void Hide<TWindow>()
        where TWindow : Window
    {
        if (_windows.TryGetValue(typeof(TWindow), out Window window) == false)
            throw new System.Exception("WTF");

        window.Hide();
    }

    public void HideWithFade<TWindow>()
        where TWindow : Window
    {
        if (_windows.TryGetValue(typeof(TWindow), out Window window) == false)
            window.HideWithFade().Forget();

        window.HideWithFade().Forget();
    }

    public TWindow GetWindow<TWindow>()
        where TWindow : Window
    {
        if (_windows.TryGetValue(typeof(TWindow), out Window window) == false)
            throw new Exception("Does not have this window: " + nameof(TWindow));

        return (TWindow)window;
    }
}