using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Play();
    }

    public void Play()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void Stop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
