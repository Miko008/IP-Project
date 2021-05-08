using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MiniGameManager2 : MonoBehaviour
{
    public Equation equation;
    private int result;

    [SerializeField]
    private Text equationText;

    [SerializeField]
    private float timeDelay = 1f;

    [SerializeField]
    private Text answer1Text;
    [SerializeField]
    private Text answer2Text;
    [SerializeField]
    private Text answer3Text;
    [SerializeField]
    private Text answer4Text;

    void Start()
    {
        equation.component1 = Random.Range(1, 101);
        equation.component2 = Random.Range(1, 101);
        equation.operation = ChooseOperation();

        result = 0;

        if (equation.operation == 'x')
        {
            result = equation.component1 * equation.component2;
        }
        else if (equation.operation == '+')
        {
            result = equation.component1 + equation.component2;
        }
        else
        {
            result = equation.component1 - equation.component2;
        }

        equationText.text = string.Format("{0} {1} {2} = ?", equation.component1, equation.operation, equation.component2);
        ChooseResultLocation();
    }

    char ChooseOperation()
    {
        //char operations = new char[3];
        //operations[0] = 'x';
        //operations[1] = '+';
        //operations[2] = '-';

        var operations = new[]{
            'x',
            '+',
            '-'
        };

        int temp = Random.Range(0, 3);

        return operations[temp];
    }

    void ChooseResultLocation() 
    {
        int temp = Random.Range(1, 5);
        if (temp == 1)
        {
            answer1Text.text = result.ToString();
            answer2Text.text = Random.Range(1, 201).ToString();
            answer3Text.text = Random.Range(1, 201).ToString();
            answer4Text.text = Random.Range(1, 201).ToString();
        }
        else if (temp == 2)
        {
            answer1Text.text = Random.Range(1, 201).ToString();
            answer2Text.text = result.ToString();
            answer3Text.text = Random.Range(1, 201).ToString();
            answer4Text.text = Random.Range(1, 201).ToString();
        }
        else if (temp == 3)
        {
            answer1Text.text = Random.Range(1, 201).ToString();
            answer2Text.text = Random.Range(1, 201).ToString();
            answer3Text.text = result.ToString();
            answer4Text.text = Random.Range(1, 201).ToString();
        }
        else
        {
            answer1Text.text = Random.Range(1, 201).ToString();
            answer2Text.text = Random.Range(1, 201).ToString();
            answer3Text.text = Random.Range(1, 201).ToString();
            answer4Text.text = result.ToString();
        }
    }

    IEnumerator TransitionToNextEquation()
    {
        yield return new WaitForSeconds(timeDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectedAnswer1()
    {
        //animator.SetTrigger("True");
        //if (answer1Text == result.ToString)
        //{
        //    Debug.Log("correct!");
        //}
        StartCoroutine(TransitionToNextEquation());
    }

    public void UserSelectedAnswer2()
    {
        //animator.SetTrigger("True");
        
        StartCoroutine(TransitionToNextEquation());
    }

    public void UserSelectedAnswer3()
    {
        //animator.SetTrigger("True");
        
        StartCoroutine(TransitionToNextEquation());
    }

    public void UserSelectedAnswer4()
    {
        //animator.SetTrigger("True");
        
        StartCoroutine(TransitionToNextEquation());
    }
}
