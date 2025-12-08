using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    void StopMusic()
    {
        audioSource.Stop();
    }
    void PlayMusic()
    {
        audioSource.Play();
    }

    
}
