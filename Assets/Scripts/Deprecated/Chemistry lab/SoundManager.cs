using UnityEngine;


public class SoundManager : MonoBehaviour {


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip audioClip2;
    private bool clipPlayed = false;
    public AudioClip[] audioClips;


    private void PlaySoundClip(AudioClip audioClip)
    {
        if (clipPlayed == false)
        {
            audioSource.PlayOneShot(audioClip, 0.9f);
            clipPlayed = true;
        }
    }

    public void PlaySoundOnce(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip, 0.9f);
    }
}
