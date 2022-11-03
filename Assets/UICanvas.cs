using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textScore;
    public Slider healthBar;
    public Slider ShadowBar;
    public GameObject fillHealthBar;
    public float health = 100f;
    public GameObject Quiz;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.onQuizAnswer += QuizAnswer;
        GameInstance.onQuizStart += onQuizStart;
        GameInstance.ReduceHealth += onReduceHealth;
        GameInstance.onLastQuizAnswered += stopQuizSpawn;
        GameInstance.onFinishHit += onGameFinish;
        GameInstance.onResetGame += onReset;
        GameInstance.onStart += onStart;

        //var text = this.gameObject.transform.Find("TextScore");
        //var text = this.gameObject.transform.Find("TextScore").GetComponent<TMPro.TextMeshPro>();
        textScore.text = $"{score}m";
        this.on75m += UICanvas_on75m;
        //Debug.Log(text);
    }
    private void onStart()
    {
        onReset();
        Debug.Log("ON START CALLED!");
    }
    private void onReset()
    {
        GameInstance.score = 0;
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
        score = 0;
        Debug.Log("reset!");

        QuizCount = 0;
    }

    private void onGameFinish()
    {
        pauseScore = true;
        stopQuiz = true;
        //throw new NotImplementedException();
    }

    bool stopQuiz = false;
    private void stopQuizSpawn()
    {
        stopQuiz = true;
    }
    float shadowHealth = 100f;
    float finalShadowHealth = 100f;
    float startShadowHelath = 100f;
    float finalHealth = 100f;
    private void onReduceHealth(float obj)
    {
        Debug.Log("reducing health...");
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
            pauseScore = true;
        }
        Debug.Log(finalHealth);
        StartCoroutine(blinkingHP(new Color(255f / 255f, 97f / 255f, 97f / 255f)));
        StartCoroutine(reduceShadowHealth());
        GameInstance.score = Convert.ToInt32(finalHealth);

    }
    IEnumerator reduceShadowHealth()
    {
        yield return new WaitForSeconds(0.6f);
        elapsedTimeShadow = 0;
        //Debug.Log("shadow reducing...");
        finalShadowHealth = finalHealth;
        Debug.Log(finalHealth);
    }
    IEnumerator blinkingHP(Color color)
    {
        int i = 0;
        while (i != 5)
        {
            yield return new WaitForSeconds(0.2f);
            fillHealthBar.GetComponent<Image>().color = color;
            yield return new WaitForSeconds(0.2f);
            fillHealthBar.GetComponent<Image>().color = new Color(255f / 255f, 194f / 255f, 47f / 255f);
            i++;
        }
    }

    //IEnumerator
    private void QuizAnswer(CanvasQuiz.AnswerType obj)
    {
        pauseScore = false;
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
            StartCoroutine(blinkingHP(new Color(80f / 255f, 255f / 255f, 74f / 255f)));
            //health = Mathf.Clamp(health, 0f, 100f);
            return;
        }
        if (obj == CanvasQuiz.AnswerType.Wrong)
        {
            this.onReduceHealth(25f);
            return;
        }
    }

    private bool pauseScore = true;
    private void onQuizStart()
    {
        pauseScore = true;
    }
    private void UICanvas_on75m()
    {
        //respawn an Quiz
        if (stopQuiz) return;
        GameInstance.onQuizSpawn?.Invoke();
        Debug.Log("start moving the quiz...");
    }
    private int QuizCount = 0;
    private float elapsedTimeShadow = 0f;
    private float elapsedTime = 0f;
    private float elapsedTimeScore = 0f;
    private float startHealth = 100f;
    // Update is called once per frame
    void Update()
    {
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
        textScore.text = $"{Convert.ToInt32(finalScore)}M";
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
