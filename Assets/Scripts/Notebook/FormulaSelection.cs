using UnityEngine;
using UnityEngine.UI;

public class FormulaSelection : MonoBehaviour
{

    //stage 1
    [SerializeField] private Canvas canvasStage1;
    [SerializeField] private GameObject stage1Button;

    //stage 2
    [SerializeField] private Canvas canvasStage2;
    [SerializeField] private Text moleculeNameText;

    private bool correctFormula = false;
    public bool formulaSubmited = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentStage == GameManager.Stage.stage1)
        {
            Stage1();
        }
        if (GameManager.instance.currentStage == GameManager.Stage.stage2)
        {
            Stage2();
        }
    }

    private void Stage2()
    {
        canvasStage1.enabled = false;
        canvasStage2.enabled = true;

        //display molecule name
        moleculeNameText.text = GameManager.instance.chosenMolecule.GetName().ToString();
        moleculeNameText.enabled = true;
    }

    public void CheckFormula(string formula)
    {
        if (GameManager.instance.chosenMolecule.checkFormula(formula) == true)
        {
            correctFormula = true;
        }
        else
        {
            correctFormula = false;
        }
    }

    public void SubmitFormula()
    {
        if (correctFormula == true)
        {
            formulaSubmited = true;
        }
        else
        {
            // TODO : Add guide msg telling that the formula submitted is false. 
        }
    }


    private void Stage1()
    {
        canvasStage1.enabled = true;
        canvasStage2.enabled = false;
    }

    public void Stage1Button()
    {
        stage1Button.SetActive(true);
    }

}
