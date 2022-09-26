using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textScore;
    public Slider healthBar;
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

        //var text = this.gameObject.transform.Find("TextScore");
        //var text = this.gameObject.transform.Find("TextScore").GetComponent<TMPro.TextMeshPro>();
        textScore.text = $"{score}m";
        this.on75m += UICanvas_on75m;
        //Debug.Log(text);
    }

    private void onReset()
    {
        GameInstance.score = 0;
        pauseScore = false;
        stopQuiz = false;
        health = 100f;
        elapsedTime = 0;
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

    private void onReduceHealth(float obj)
    {
        Debug.Log("reducing health...");
        health -= obj;
        health = Mathf.Clamp(health, 0f, 100f);
        Debug.Log(health);
        if (health <= 0)
        {
            GameInstance.onGameOver?.Invoke();
            this.gameObject.transform.Find("GameOver").gameObject.SetActive(true);
            pauseScore = true;
        }
        GameInstance.score = Convert.ToInt32(health);

    }

    private void QuizAnswer(CanvasQuiz.AnswerType obj)
    {
        pauseScore = false;
        if (obj == CanvasQuiz.AnswerType.Correct)
        {
            health += 10f;
            health = Mathf.Clamp(health, 0f, 100f);
            return;
        }
        if (obj == CanvasQuiz.AnswerType.Wrong)
        {
            this.onReduceHealth(25f);
            return;
        }
    }

    private bool pauseScore = false;
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
    private float elapsedTime = 0f;
    // Update is called once per frame
    void Update()
    {
        healthBar.value = health / 100f;

        if (pauseScore) return;
        elapsedTime += Time.deltaTime;
        var finalScore = elapsedTime * 5;
        textScore.text = $"{Convert.ToInt32(finalScore)}M";
        var temp = finalScore / 75;
        if (QuizCount != Convert.ToInt32(MathF.Floor(temp)))
        {
            on75m?.Invoke();
            QuizCount = Convert.ToInt32(MathF.Floor(temp));
        }

    }

    public void testsaja()
    {
        //action
    }
    public event Action on75m;


}
