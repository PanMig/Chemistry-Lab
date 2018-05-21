using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using goedle_sdk;

public class ContentAdaptationButton : MonoBehaviour {

    //sprites
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite greenSprite;

    private Animator animator;
    public AudioClip clip;

    [SerializeField] private ContentAdaptationManager CAManager;

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = normalSprite;
        if (GameManager.currentLevel == GameManager.Levels.moleculeNaming)
        {
            SlotSpawner.MolCompleted += ChangeSprite;
        }
        else if (GameManager.currentLevel == GameManager.Levels.moleculeConstruction)
        {
            EmptyParentMolecule.MolConstructed += ChangeSprite;
        }

        //initialize the animator.
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
    }

    private void OnDisable()
    {
        SlotSpawner.MolCompleted -= ChangeSprite;
        EmptyParentMolecule.MolConstructed -= ChangeSprite;
    }

    public void OnClick()
    {
        SoundManager.instance.PlaySingle(clip);
        Camera.main.orthographicSize = 10;

        //set parameters to initial.
        gameObject.GetComponent<Image>().sprite = normalSprite;
        animator.enabled = false;
        GoedleAnalytics.instance.track("continue.quiz", GameManager.currentLevel.ToString());
        GameManager.chosenMolecule = null;

        //Load next molecule
        CAManager.LoadNextMol();
    }


    public void ChangeSprite()
    {
        gameObject.GetComponent<Image>().sprite = greenSprite;
        animator.enabled = true;
    }


}
