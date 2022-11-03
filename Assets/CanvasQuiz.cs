using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasQuiz : MonoBehaviour
{
    //public int GameInstance.indexQuiz = -1;
    public TextMeshProUGUI TMP_Answer1;
    public TextMeshProUGUI TMP_Answer2;
    public TextMeshProUGUI TMP_Answer3;
    public TextMeshProUGUI TMP_Pertanyaan;
    public TextMeshProUGUI TMP_Timer;
    private void onReset()
    {
        GameInstance.indexQuiz = -1;
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
              new IQuiz()
        {
            pertanyaan = "Sifat-sifat yang dimiliki oleh seorang technopreneur, kecuali ? ",
            jawaban = new string[3] { "smart", "logic", "creativity" },
            answer = 1
        }
              ,
        new IQuiz()
        {
            pertanyaan = "Skill yang dibutuhkan oleh seorang technopreneur, kecuali ? ",
            jawaban = new string[3] { "teamwork", "leadership", "Self-interested" },
            answer = 3
        },
        new IQuiz()
        {
            pertanyaan = "Perusahaan rintisan yang belum lama beroperasi adalah ? ",
            jawaban = new string[3] { "UKM", "Start Up", "UMKM" },
            answer = 2
        },
        new IQuiz()
        {
            pertanyaan = "Seorang yang berpikir 'Outside the box' adalah ?  ",
            jawaban = new string[3] { "Entrepreneur", "Sociopreneur", "Technopreneur" },
            answer = 3
        },
        new IQuiz()
        {
            pertanyaan = "Sebuah badan usaha yang memberikan pendanaan pada sebuah start-up adalah ?",
            jawaban = new string[3] { "Angel Investor", "Bootstrap", "Venture Capital" },
            answer = 3
        },
        new IQuiz()
        {
            pertanyaan = "Target pasar utama atau tujuan utama dari konsumen yang yang dituju merupakan arti dari ?",
            jawaban = new string[3] { "Mass Market", "Target Market", "Niche Market" },
            answer = 2
        },
        new IQuiz()
        {
            pertanyaan = "Industri dengan target pasar dan konsumen yang lebih spesifik merupakan arti dari ? ",
            jawaban = new string[3] { "Mass Market", "Target Market", "Niche Market" },
            answer = 3
        },
        new IQuiz()
        {
            pertanyaan = "Target pasar yang lebih luas tapi masih sesuai dengan segmen yang sudah ditetapkan sebelumnya merupakan arti dari ?  ",
            jawaban = new string[3] { "Mass Market", "Target Market", "Niche Market" },
            answer = 1
        },
        new IQuiz()
        {
            pertanyaan = "Yang tidak termasuk dalam segementasi pasar adalah ?  ",
            jawaban = new string[3] { "Segemented Market", "Mass Market", "Target Market" },
            answer = 3
        },
        new IQuiz()
        {
            pertanyaan = "Ciri-ciri dari Segmented Marketing, kecuali ?",
            jawaban = new string[3] { "More income", "Adapt offer to best match segment", "Isolate broad segements comprising a market" },
            answer = 1
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
        if (GameInstance.indexQuiz + 1 == quiz.Count)
        {
            GameInstance.onLastQuizAnswered?.Invoke();
            Debug.Log("FINISHED!");
        }
        if (index+1 == quiz[GameInstance.indexQuiz].answer)
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
        GameInstance.speedScale += 0.3f;
        GameInstance.speedScale = Math.Min(GameInstance.speedScale, 2.5f);
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
            if (GameInstance.indexQuiz + 1 == quiz.Count)
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
        GameInstance.indexQuiz++;
        Debug.Log("nextquiz called, CURRENT INDEX:");
        Debug.Log(GameInstance.indexQuiz);
        var currentQuiz = quiz[GameInstance.indexQuiz];

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
