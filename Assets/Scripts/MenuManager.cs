using UnityEngine;

public class MenuManager : MonoBehaviour
{
    AudioSource _mAudioSource;
    public AudioClip mStartMusic;
    
    /// <summary>
    /// Plays the main menu music.
    /// </summary>
    private void Awake()
    {
        _mAudioSource = GetComponent<AudioSource>();
        _mAudioSource.clip = mStartMusic;
        _mAudioSource.Play();
    }
}
