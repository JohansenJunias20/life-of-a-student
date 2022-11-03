using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasQuizSpawner : MonoBehaviour
{
    public GameObject GO_CanvasQuiz;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("script canvas quiz called");
        GameInstance.onQuizStart += QuizStart;
        GameInstance.onResetGame += onReset;
        //GameInstance.onLastQuizAnswered += onLastQuizAnswered;
    }

    private void onReset()
    {
        //GO_CanvasQuiz.GetComponent<CanvasQuiz>().currentIndex = -1;
        GameInstance.indexQuiz = -1;
        GO_CanvasQuiz.SetActive(false);
        //because inside canvasquiz.cs cannot assign itself due inactive gameobject
    }
    
    private void QuizStart()
    {
        Debug.Log("POPPING CANVAS QUIZ...");
        GO_CanvasQuiz.SetActive(true);
        GO_CanvasQuiz.GetComponent<CanvasQuiz>().nextQuiz();
        GO_CanvasQuiz.GetComponent<CanvasQuiz>().StartTimer();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
