using UnityEngine;

public class SimpleAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _clickButton;

    public void PlayClickButton()
    {
        _clickButton.Play(); 
    }
}