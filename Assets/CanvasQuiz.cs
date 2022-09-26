using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasQuiz : MonoBehaviour
{
    public int currentIndex = -1;
    public TextMeshProUGUI TMP_Answer1;
    public TextMeshProUGUI TMP_Answer2;
    public TextMeshProUGUI TMP_Answer3;
    public TextMeshProUGUI TMP_Pertanyaan;
    public TextMeshProUGUI TMP_Timer;
    private void onReset()
    {
        currentIndex = -1;
        currentDuration = -1;
        timeout = false;
        this.gameObject.SetActive(false);
    }
    public struct IQuiz
    {
        public string pertanyaan { get; set; }
        public string[] jawaban { get; set; }
        public int answer { get; set; }

    };
    public List<IQuiz> quiz = new List<IQuiz>()
    {
        new IQuiz() {
            pertanyaan = "yang bukan merupakan perhitungan HPP",
            jawaban = new string[] { "AVERAGE", "FIFO","STACK" } ,
            answer=2}
        ,
        new IQuiz() {
            pertanyaan = "yang bukan merupakan data structure",
            jawaban=new string[] { "ARRAY" , "STACK","LOOP", },
            answer = 2
        },
        new IQuiz()
        {
            pertanyaan ="yang merupakan bahasa pemrograman",
            jawaban= new string[] { "C#", "HTML", "CSS"},
            answer=0
        }
    };
    public enum AnswerType
    {
        Wrong,
        Correct,
        Timeout
    }
    private bool answered = false;
    public void Answer(int index)
    {
        if (timeout) return;
        answered = true;
        if (currentIndex + 1 == quiz.Count)
        {
            GameInstance.onLastQuizAnswered?.Invoke();
            Debug.Log("FINISHED!");
        }
        if (index == quiz[currentIndex].answer)
        {
            //benar
            Debug.Log("BENAR!");
            GameInstance.onQuizAnswer?.Invoke(AnswerType.Correct);
            // listen to event Action onQuizAnswer :
            //1. Hide QuizCanvas -> CanvasQuizSpawner
            //2. resume the score count -> CanvasUI
            //3. resume the spawn Obstacle -> ObstacleSpawner
            //4. resume the obstacles Movement -> ObstacleMovement
            //4. resume the quiz Movement -> QuizMovement

        }
        else
        {
            Debug.Log("SALAH!");
            //salah
            GameInstance.onQuizAnswer?.Invoke(AnswerType.Wrong);
        }
        this.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.onGameOver += onGameOver;
        GameInstance.onResetGame += onReset;
    }

    private void onGameOver()
    {
        this.gameObject.SetActive(false);
    }

    private bool timeout = false;
    // Update is called once per frame
    void Update()
    {
        if (!startTimer) return;
        if (answered) return;
        currentDuration -= Time.deltaTime;
        if (currentDuration <= 0)
        {
            if (currentIndex + 1 == quiz.Count)
            {
                GameInstance.onLastQuizAnswered?.Invoke();
                Debug.Log("FINISHED!");
            }
            timeout = true;
            GameInstance.onQuizAnswer?.Invoke(AnswerType.Timeout);
            this.gameObject.SetActive(false);
        }
        TMP_Timer.text = Convert.ToInt32(currentDuration).ToString();
    }

    public void nextQuiz()
    {
        answered = false;
        timeout = false;
        currentIndex++;
        Debug.Log("nextquiz called, CURRENT INDEX:");
        Debug.Log(currentIndex);
        var currentQuiz = quiz[currentIndex];

        TMP_Answer1.text = currentQuiz.jawaban[0];
        TMP_Answer2.text = currentQuiz.jawaban[1];
        TMP_Answer3.text = currentQuiz.jawaban[2];
        TMP_Pertanyaan.text = currentQuiz.pertanyaan;
    }
    private float currentDuration = -1f;
    public float durationAnswer = 7f;
    private bool startTimer = false;
    public void StartTimer()
    {
        currentDuration = durationAnswer;
        startTimer = true;
    }
}
