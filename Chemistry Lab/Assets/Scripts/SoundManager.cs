using UnityEngine;


public class SoundManager : MonoBehaviour {


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip audioClip2;
    [SerializeField] private StageDisplayManager stageDisplayMng;
    [SerializeField] private GameGuide guide;
    private bool clipPlayed = false;
    public AudioClip[] audioClips;


    // Update is called once per frame
    void Update () {
        if (GameManager.instance.currentStage == GameManager.Stage.stage2 && stageDisplayMng.formulaSubmited == true)
        {
            PlaySoundClip(audioClip);
        }
        else if (GameManager.instance.currentStage == GameManager.Stage.stage3 && GameManager.instance.chosenMolecule.GetConstruction() == false)
        {
            clipPlayed = false;
        }
        else if (GameManager.instance.currentStage == GameManager.Stage.stage3 && GameManager.instance.chosenMolecule.GetConstruction()==true)
        {
            PlaySoundClip(audioClip);    
        }
        else if(GameManager.instance.currentStage == GameManager.Stage.stage4)
        {
            clipPlayed = false;
        }
        else if (guide.gameGuideCompletion == true)
        {
            PlaySoundClip(audioClip2);
        }
    }

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
