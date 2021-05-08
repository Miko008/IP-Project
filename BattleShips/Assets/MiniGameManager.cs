using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text factText;
    [SerializeField]
    private Text trueAnswerText;
    [SerializeField]
    private Text falseAnswerText;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float timeDelay = 1f;

    void Start()
    {
        if ((unansweredQuestions == null) || (unansweredQuestions.Count == 0))
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
        //Debug.Log(currentQuestion.fact + " is " + currentQuestion.isTrue);
    }

    void SetCurrentQuestion()
    {
        int randomIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomIndex];

        factText.text = currentQuestion.fact;
        //unansweredQuestions.RemoveAt(randomIndex);

        if (currentQuestion.isTrue)
        {
            trueAnswerText.text = "CORRECT!";
            falseAnswerText.text = "WRONG!";
        }
        else
        {
            trueAnswerText.text = "WRONG!";
            falseAnswerText.text = "CORRECT!";
        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(timeDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UserSelectedTrue()
    {
        animator.SetTrigger("True");
        //if (currentQuestion.isTrue)
        //{
        //    Debug.Log("correct answer");
        //}
        //else
        //{
        //    Debug.Log("wrong answer");
        //}

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectedFalse()
    {
        animator.SetTrigger("False");
        //if (!currentQuestion.isTrue)
        //{
        //    Debug.Log("correct answer");
        //}
        //else
        //{
        //    Debug.Log("wrong answer");
        //}

        StartCoroutine(TransitionToNextQuestion());
    }
}
