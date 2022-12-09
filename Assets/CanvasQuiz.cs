using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasQuiz : MonoBehaviour
{
    //public int GameInstance.indexQuiz = -1;
    public GameObject GO_Answer1, GO_Answer2, GO_answer3, GO_QuestionBoard;
    public TextMeshProUGUI TMP_Answer1;
    public TextMeshProUGUI TMP_Answer2;
    public TextMeshProUGUI TMP_Answer3;
    public TextMeshProUGUI TMP_Pertanyaan;
    public TextMeshProUGUI TMP_Timer;
    private void onReset()
    {
        GameInstance.indexQuiz = -1;
        currentDuration = 7;
        timeout = false;
        this.gameObject.SetActive(false);
    }
    public struct IQuiz
    {
        public string pertanyaan { get; set; }
        public string[] jawaban { get; set; }
        public int answer { get; set; }

    };
    public UnityEvent onCorrectAnswer, onWrongAnswer, onTimeout;
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
        }
        ,
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
        }
        //,
        //new IQuiz()
        //{
        //    pertanyaan = "Ciri-ciri dari Segmented Marketing, kecuali ?",
        //    jawaban = new string[3] { "More income", "Adapt offer to best match segment", "Isolate broad segements comprising a market" },
        //    answer = 1
        //}
    };
    public enum AnswerType
    {
        Wrong,
        Correct,
        Timeout
    }
    public Sprite BoxGreen, BoxRed;
    private bool answered = false;
    public void Answer(int index)
    {
        if (answered) return;
        if (timeout) return;
        answered = true;
        if (GameInstance.indexQuiz + 1 == quiz.Count)
        {
            GameInstance.onLastQuizAnswered?.Invoke();
            Debug.Log("FINISHED!");
        }
        if (index + 1 == quiz[GameInstance.indexQuiz].answer)
        {
            //benar
            onCorrectAnswer?.Invoke();
            Debug.Log("BENAR!");
            GameInstance.onQuizAnswer?.Invoke(AnswerType.Correct);
            //switch (index)
            //{
            //    case 0:
            //        GO_Box1.gameObject.SetActive(true);
            //        GO_Box1.GetComponent<Image>().sprite = BoxGreen;
            //        break;
            //    case 1:
            //        GO_Box2.gameObject.SetActive(true);
            //        GO_Box3.GetComponent<Image>().sprite = BoxGreen;
            //        break;
            //    case 2:
            //        GO_Box3.gameObject.SetActive(true);
            //        GO_Box3.GetComponent<Image>().sprite = BoxGreen;
            //        break;
            //    default:
            //        break;
            //}
            StartCoroutine(blinking(AnswerType.Correct, index));

        }
        else
        {
            Debug.Log("SALAH!");
            onWrongAnswer?.Invoke();

            //switch (index)
            //{
            //    case 0:
            //        GO_Box1.gameObject.SetActive(true);
            //        GO_Box1.GetComponent<Image>().sprite = BoxRed;
            //        break;
            //    case 1:
            //        GO_Box2.gameObject.SetActive(true);
            //        GO_Box2.GetComponent<Image>().sprite = BoxRed;
            //        break;
            //    case 2:
            //        GO_Box3.gameObject.SetActive(true);
            //        GO_Box3.GetComponent<Image>().sprite = BoxRed;
            //        break;
            //    default:
            //        break;
            //}
            StartCoroutine(blinking(AnswerType.Wrong, index));
            //salah
            GameInstance.onQuizAnswer?.Invoke(AnswerType.Wrong);
        }
        StartCoroutine(delayFeedback());
        GameInstance.speedScale += 0.3f;
        GameInstance.speedScale = Math.Min(GameInstance.speedScale, 1.8f);
    }
    public GameObject GO_Box1, GO_Box2, GO_Box3;
    IEnumerator delayFeedback()
    {
        yield return new WaitForSeconds(3f);
        GameInstance.onFeedbackAnswerDone?.Invoke();
        canvasTimeout.SetActive(false);
        this.gameObject.SetActive(false);
    }
    IEnumerator blinking(AnswerType answer, int index)
    {
        Color finalColor = answer == AnswerType.Correct ? Color.green : Color.red;
        var times = 0;
        while (true)
        {
            switch (index)
            {
                case 0:
                    GO_Answer1.GetComponent<Image>().color = finalColor;
                    break;
                case 1:
                    GO_Answer2.GetComponent<Image>().color = finalColor;
                    break;
                case 2:
                    GO_answer3.GetComponent<Image>().color = finalColor;
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.2f);
            GO_Answer1.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            GO_Answer2.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            GO_answer3.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);

            if (times == 5)
            {
                break;
            }
            times++;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.onGameOver += onGameOver;
        GameInstance.onResetGame += onReset;
        //oriScaleQuestionBoard = GO_QuestionBoard.transform.localScale;
    }

    private void onGameOver()
    {
        this.gameObject.SetActive(false);
    }
    public GameObject canvasTimeout;
    private bool timeout = false;
    // Update is called once per frame
    void Update()
    {
        if (!startTimer) return;
        if (answered) return;
        currentDuration -= Time.deltaTime;
        if (currentDuration <= 0 && !timeout)
        {
            onTimeout?.Invoke();
            if (GameInstance.indexQuiz + 1 == quiz.Count)
            {
                GameInstance.onLastQuizAnswered?.Invoke();
                Debug.Log("FINISHED!");
            }
            timeout = true;
            canvasTimeout.SetActive(true);
            canvasTimeout.GetComponent<Animator>().Play("CanvasTimeout");
            StartCoroutine(delayFeedback());
            GameInstance.onQuizAnswer?.Invoke(AnswerType.Timeout);
            //this.gameObject.SetActive(false);
        }
        if (currentDuration <= 3)
        {
            //INI DIPANGGIL SAAT ANGKANYA BERUBAH SAJA
            //StartCoroutine(Shake(0.5f, 0.5f));
            if (TMP_Timer.text != Convert.ToInt32(Math.Clamp(currentDuration, 0, 100)).ToString())
            {

                TMP_Timer.color = Color.red;
                StartCoroutine(Shake(0.5f, 5f));
                onShakeTimer?.Invoke();
            }
        }
        else
        {

        }

        TMP_Timer.text = Convert.ToInt32(Math.Clamp(currentDuration, 0, 100)).ToString();
    }
    public UnityEvent onShakeTimer;
    public void nextQuiz()
    {
        currentDuration = durationAnswer;
        answered = false;
        timeout = false;
        GameInstance.indexQuiz++;
        Debug.Log("nextquiz called, CURRENT INDEX:");
        GO_Answer1.GetComponent<Image>().color = Color.white;
        GO_Answer2.GetComponent<Image>().color = Color.white;
        GO_answer3.GetComponent<Image>().color = Color.white;
        GO_Box1.SetActive(false);
        GO_Box2.SetActive(false);
        GO_Box3.SetActive(false);
        Debug.Log(GameInstance.indexQuiz);
        var currentQuiz = quiz[GameInstance.indexQuiz];
        //GO_QuestionBoard.localScale = oriScaleQuestionBoard * 0f;
        TMP_Answer1.text = currentQuiz.jawaban[0];
        TMP_Answer2.text = currentQuiz.jawaban[1];
        TMP_Answer3.text = currentQuiz.jawaban[2];
        TMP_Pertanyaan.text = currentQuiz.pertanyaan;
        TMP_Timer.color = Color.white;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = TMP_Timer.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            TMP_Timer.transform.position = orignalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return 0;
        }
        TMP_Timer.transform.position = orignalPosition;
    }

    private float _currentDuration;
    private float currentDuration
    {
        get { return _currentDuration; }
        set
        {
            //if (shakeTimer && _currentDuration != -1f)
            //{
            //    //StartCoroutine(Shake(0.5f, 0.2f));
            //};
            _currentDuration = value;
        }
    }
    public float durationAnswer = 7f;
    private bool startTimer = false;

    public void StartTimer()
    {
        currentDuration = durationAnswer;
        startTimer = true;
    }
}
