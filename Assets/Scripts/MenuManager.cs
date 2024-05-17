using UnityEngine;

public class MenuManager : MonoBehaviour
{
    AudioSource m_AudioSource;
    public AudioClip m_StartMusic;
    
    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.clip = m_StartMusic;
        m_AudioSource.Play();
    }
}
