using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textScore;
    public Slider healthBar;
    public Slider ShadowBar;
    public GameObject ShadowFill;
    public GameObject fillHealthBar;
    public float health = 100f;
    public GameObject GO_healthText;
    public GameObject GO_healthTextShadow;
    public Sprite shadowHealthRed, shadowHealthGreen, healthRed;
    public GameObject Quiz;
    public GameObject canvasPause;
    private int score = 0;
    private Sprite OriFillHealth;
    // Start is called before the first frame update
    void Start()
    {
        OriFillHealth = fillHealthBar.GetComponent<Image>().sprite;
        GameInstance.onFeedbackAnswerDone += FeedbackAnswerDone;
        GameInstance.onQuizAnswer += QuizAnswer;
        GameInstance.onQuizStart += onQuizStart;
        GameInstance.ReduceHealth += onReduceHealth;
        GameInstance.onLastQuizAnswered += stopQuizSpawn;
        GameInstance.onFinishHit += onGameFinish;
        GameInstance.onResetGame += onReset;
        GameInstance.onStart += onStart;
        GameInstance.onResume += () =>
        {
            disablePause = false;
            pauseScore = false;
            underPause = false;
            //stopQuiz = false;
        };
        GameInstance.backToMainMenu += () =>
        {
            onReset();
            stopQuiz = true;
            pauseScore = true;
            //stop
        };

        //var text = this.gameObject.transform.Find("TextScore");
        //var text = this.gameObject.transform.Find("TextScore").GetComponent<TMPro.TextMeshPro>();
        textScore.text = $"{score}m";
        this.on75m += UICanvas_on75m;
        //Debug.Log(text);
    }

    private void FeedbackAnswerDone()
    {
        pauseScore = false;
        disablePause = false;
    }

    private void onStart()
    {
        onReset();
        Debug.Log("ON START CALLED!");
    }
    private void onReset()
    {
        GameInstance.score = 0;
        disablePause = false;
        pauseScore = false;
        stopQuiz = false;
        health = 100f;
        finalHealth = 100f;
        startHealth = 100f;
        shadowHealth = 100f;
        finalShadowHealth = 100f;
        elapsedTime = 0;
        elapsedTimeScore = 0;
        elapsedTimeShadow = 0;
        stopQuiz = false;
        pauseScore = false;
        underPause = false;
        score = 0;
        Debug.Log("reset!");

        QuizCount = 0;
    }

    private void onGameFinish()
    {
        disablePause = true;
        pauseScore = true;
        stopQuiz = false;
        //throw new NotImplementedException();
    }

    bool stopQuiz = false;
    private bool underPause = false;

    private void stopQuizSpawn()
    {
        stopQuiz = true;
    }
    float shadowHealth = 100f;
    float finalShadowHealth = 100f;
    float startShadowHelath = 100f;
    float finalHealth = 100f;
    public void onPause()
    {
        if (disablePause) return;
        onShowPauseCanvas?.Invoke();
        GameInstance.onPause?.Invoke();
        canvasPause.SetActive(true);
        canvasPause.GetComponent<Animator>().Play("CanvasPause");
        stopQuiz = true;
        underPause = true;
        pauseScore = true;
        disablePause = true;
    }

    public UnityEvent onShowPauseCanvas;
    private void onReduceHealth(float obj)
    {
        //Debug.Log("reducing health...");
        ShadowFill.GetComponent<Image>().sprite = shadowHealthRed;
        elapsedTime = 0;
        startShadowHelath = shadowHealth;
        startHealth = health;
        finalHealth -= obj;
        //health -= obj;
        finalHealth = Mathf.Clamp(finalHealth, 0f, 100f);
        //Debug.Log(health);
        if (finalHealth <= 0)
        {
            GameInstance.onGameOver?.Invoke();
            this.gameObject.transform.Find("GameOver").gameObject.SetActive(true);
            this.gameObject.transform.Find("GameOver").gameObject.GetComponent<Animator>().Play("GameOver");
            onGameOver?.Invoke();
            pauseScore = true;
        }
        //Debug.Log(finalHealth);
        StartCoroutine(blinkingHP(ColorType.red));
        StartCoroutine(reduceShadowHealth());
        GameInstance.score = Convert.ToInt32(finalHealth);

    }
    public UnityEvent onGameOver;
    IEnumerator reduceShadowHealth()
    {
        yield return new WaitForSeconds(0.6f);
        elapsedTimeShadow = 0;
        //Debug.Log("shadow reducing...");
        finalShadowHealth = finalHealth;
        startShadowHelath = shadowHealth;
        //Debug.Log(finalHealth);
    }
    public enum ColorType
    {
        green,
        red
    }
    IEnumerator blinkingHP(ColorType color)
    {
        int i = 0;
        while (i != 5)
        {
            yield return new WaitForSeconds(0.2f);
            if (color == ColorType.green)
            {
                fillHealthBar.GetComponent<Image>().color = new Color(200 / 255f, 255 / 255f, 198 / 255f, 1f);

            }
            else
            {
                fillHealthBar.GetComponent<Image>().sprite = healthRed;
                //fillHealthBar.GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0.2f);
            }
            yield return new WaitForSeconds(0.2f);
            fillHealthBar.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            fillHealthBar.GetComponent<Image>().sprite = OriFillHealth;
            i++;
        }
    }

    //IEnumerator
    private void QuizAnswer(CanvasQuiz.AnswerType obj)
    {
        disablePause = false;
        if (obj == CanvasQuiz.AnswerType.Correct)
        {
            //health += 10f;
            startHealth = health;
            finalHealth += 10f;
            finalHealth = Math.Clamp(finalHealth, 0f, 100f);
            elapsedTime = 0;
            finalShadowHealth = finalHealth;
            startShadowHelath = health;
            elapsedTimeShadow = 0;
            ShadowFill.GetComponent<Image>().sprite = shadowHealthGreen;
            StartCoroutine(blinkingHP(ColorType.green));
            //health = Mathf.Clamp(health, 0f, 100f);
            return;
        }
        if (obj == CanvasQuiz.AnswerType.Wrong)
        {
            this.onReduceHealth(25f);
            return;
        }
        if (obj == CanvasQuiz.AnswerType.Timeout)
        {
            this.onReduceHealth(25f);
            return;
        }
    }
    private bool disablePause = false;
    private bool pauseScore = true;
    private void onQuizStart()
    {
        pauseScore = true;
        disablePause = true;
    }
    IEnumerator spawnQuiz()
    {
        //respawn an Quiz
        while (underPause)
        {
            yield return 0;
        }
        GameInstance.onQuizSpawn?.Invoke();
        Debug.Log("start moving the quiz...");
    }
    private void UICanvas_on75m()
    {
        //if()
        if (!stopQuiz)
            StartCoroutine(spawnQuiz());

    }
    private int QuizCount = 0;
    private float elapsedTimeShadow = 0f;
    private float elapsedTime = 0f;
    private float elapsedTimeScore = 0f;
    private float startHealth = 100f;
    // Update is called once per frame
    void Update()
    {
        GO_healthText.GetComponent<TextMeshProUGUI>().text = $"{Convert.ToInt32(health)}";
        GO_healthTextShadow.GetComponent<TextMeshProUGUI>().text = $"{Convert.ToInt32(health)}";
        healthBar.value = health / 100f;
        ShadowBar.value = shadowHealth / 100f;
        //Debug.Log("called");
        elapsedTime += Time.deltaTime;
        elapsedTimeShadow += Time.deltaTime;
        if (!pauseScore)
        {
            elapsedTimeScore += Time.deltaTime * GameInstance.speedScale;
        }
        var finalScore = elapsedTimeScore * 5;
        textScore.text = $"{Convert.ToInt32(finalScore)}";
        //Debug.Log("score updated");
        var temp = finalScore / 75;
        if (QuizCount != Convert.ToInt32(MathF.Floor(temp)))
        {
            on75m?.Invoke();
            QuizCount = Convert.ToInt32(MathF.Floor(temp));
        }
        if (finalHealth != health)
        {

            var alpha = elapsedTime / 0.5f;
            alpha = Math.Clamp(alpha, 0f, 1f);
            health = Vector2.Lerp(new Vector2(0f, startHealth), new Vector2(0, finalHealth), alpha).y;

        }
        if (finalShadowHealth != shadowHealth)
        {
            var alphaShadow = elapsedTimeShadow / 0.4f;
            alphaShadow = Math.Clamp(alphaShadow, 0f, 1f);
            shadowHealth = Vector2.Lerp(new Vector2(0f, startShadowHelath), new Vector2(0, finalShadowHealth), alphaShadow).y;
            //Debug.Log(alphaShadow);
        }
    }
    public event Action on75m;


}
