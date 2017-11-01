using TMPro;
using UnityEngine;

public class GameGuide : MonoBehaviour
{

    public TextMeshProUGUI text;
    public TextMeshProUGUI wrongText;
    public Canvas scientistCanvas;

    private bool buttonStage0 = false;
    public StageDisplayManager displayManager;

    public bool gameGuideCompletion = false;


    private void Start()
    {
        scientistCanvas.enabled = false;
        wrongText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        if (GameManager.instance.currentStage == GameManager.Stage.stage0 && buttonStage0 == false && gameGuideCompletion == false)
        {
            text.text = "Hello, welcome to <b><i> Chemistry lab simulator </b></i>." +
                        " In this game you will go throught the complete proccess of constructing a molecule." +
                        " To continue press the 'TAB' key.";
            text.enabled = true;
            scientistCanvas.enabled = true;
        }
        else if (GameManager.instance.currentStage == GameManager.Stage.stage0 && buttonStage0 == true && gameGuideCompletion == false)
        {
            text.text = "First we will go through the process of choosing a molecule to create and name it's formula." +
                        " Search around the lab to find the molecules textbook. Press the WASD keyboard keys to move and use the mouse to rotate the camera.";
            text.enabled = true;
        }
        else if (GameManager.instance.currentStage == GameManager.Stage.stage0 && gameGuideCompletion == true)
        {
            text.text = "Great, the molecule creation process is finished successfully. Now head over to the textbook to create new molecules.";
            text.enabled = true;
        }
        else if (GameManager.instance.currentStage == GameManager.Stage.stage1)
        {
            text.text = "Congrats, you found the textbook. Now try to choose a molecule to create." +
                        "You can open the menu screen by pressing the ' I ' button.";
            text.enabled = true;
            wrongText.enabled = false;
        }
        else if (GameManager.instance.currentStage == GameManager.Stage.stage2 && displayManager.formulaSubmited == false)
        {
            text.text = "Ok good, now you must choose the correct formula." +
                        " If you don't know the answer don't worry, press the ' I ' button and start searching around for the answer. ";
            text.enabled = true;
        }
        else if (GameManager.instance.currentStage == GameManager.Stage.stage2 && displayManager.formulaSubmited == true)
        {
            text.text = "Well done, you found the correct formula for the molecule. Now search around to find the correct microscope to create the molecule.";
            text.enabled = true;
            wrongText.enabled = false;
        }
        else if (GameManager.instance.currentStage == GameManager.Stage.stage3 && GameManager.instance.chosenMolecule.GetConstruction() == false)
        {
            text.text = "Ok, now you are in the molecule creation process. " +
                        "Press the buttons above to generate the desired atom and attach it to the correct slot,following the given formula in the top of the screen.";
            text.enabled = true;
        }
        else if (GameManager.instance.currentStage == GameManager.Stage.stage3 && GameManager.instance.chosenMolecule.GetConstruction() == true)
        {
            text.text = "Good job, the molecule has been created. To exit from microscope view press the 'R' button.";
            text.enabled = true;
        }
        else if (GameManager.instance.currentStage == GameManager.Stage.stage4)
        {
            text.text = "Ok, now in your hands is the molecule you created, search for the boxes and place it in the box that corresponds to the molecule's homogenous series.";
            text.enabled = true;
            gameGuideCompletion = true;   
        }
    }

    public void PlayerInput()
    {
        if (GameManager.instance.currentStage == GameManager.Stage.stage0)
        {
            if (Input.GetKey(KeyCode.Tab) && buttonStage0 == false)
            {
                buttonStage0 = true;
            }
        }
    }

    public void DisplayWrongMsg ( string givenText )
    {
        wrongText.text = givenText;
        wrongText.enabled = true;
        text.enabled = false;
        scientistCanvas.enabled = true;
    }
}
