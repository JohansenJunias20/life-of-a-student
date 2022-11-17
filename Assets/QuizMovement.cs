using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizMovement : MonoBehaviour
{
    private int degreeDirection = 30;
    float timeElapsed = 0;
    private float duration;
    private int length = 30;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public GameObject CanvasQuiz;
    // Start is called before the first frame update
    void Start()
    {
        duration = length / GameInstance.speed;
        GameInstance.onQuizSpawn += onQuizSpawn;
        GameInstance.onFeedbackAnswerDone += FeedBackDone;
        //GameInstance.onQuizAnswer += QuizAnswer;
        GameInstance.onGameOver += onGameOver;
        GameInstance.onResetGame += onReset;
        GameInstance.onResume += () =>
        {
            underPause = false;
        };
        GameInstance.backToMainMenu += () =>
        {
            onReset();
            stopMoving = true;
            underPause = false;
        };
        GameInstance.onPause += () =>
        {
            underPause = true;
        };

        this.startPosition = transform.position;
        this.endPosition = new Vector3();
        this.transform.position = startPosition;
        endPosition.x = this.startPosition.x - Mathf.Cos(Mathf.Deg2Rad * degreeDirection) * length;
        endPosition.y = this.startPosition.y - Mathf.Sin(Mathf.Deg2Rad * degreeDirection) * length;
        endPosition.z = 0;
    }

    private void onReset()
    {
        underPause = false;
        this.InitPositions();
        //stopMoving = true;
    }

    private void onGameOver()
    {
        stopMoving = true;
        underPause = false;
    }

    void InitPositions()
    {
        //this.startPosition = new Vector3(6.22f, -0.24f, 0);
        this.endPosition = new Vector3();
        this.transform.position = startPosition;
        endPosition.x = this.startPosition.x - Mathf.Cos(Mathf.Deg2Rad * degreeDirection) * length;
        endPosition.y = this.startPosition.y - Mathf.Sin(Mathf.Deg2Rad * degreeDirection) * length;
        endPosition.z = 0;
        this.timeElapsed = 0;
        //this.gameObject.GetComponent<SpriteRenderer>()
    }
    private void FeedBackDone()
    {
        GetComponent<EdgeCollider2D>().enabled = false;
        stopMoving = false;
    }

    private void onQuizSpawn()
    {
        Debug.Log("Quiz spawning...");
        //StartMove();
        StartCoroutine(delaySpawn());
    }
    IEnumerator delaySpawn()
    {
        yield return new WaitForSeconds(1f);
        StartMove();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.ToLower() == "player")
        {
            GameInstance.onQuizStart.Invoke();
            this.stopMoving = true;
            return;
        }


        //CanvasQuiz.SetActive(true);
    }
    bool underPause = false;
    public void StartMove()
    {
        if (underPause) return;
        //this.Start();
        InitPositions();
        GetComponent<EdgeCollider2D>().enabled = true;
        stopMoving = false;
    }
    private bool stopMoving = true;
    // Update is called once per frame
    void Update()
    {
        if (stopMoving) return;
        if (underPause) return;
        if (timeElapsed < duration)
        {
            var valueToLerp = Vector3.Lerp(this.startPosition, this.endPosition, timeElapsed / duration);
            transform.position = valueToLerp;
            timeElapsed += Time.deltaTime * GameInstance.speedScale;
        }
        else
        {
            stopMoving = true;
            //Destroy(gameObject);
        }
    }
}
