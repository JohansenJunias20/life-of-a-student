using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishMovement : MonoBehaviour
{
    public GameObject toga;
    public int degreeDirection = 25;
    float timeElapsed = 0;
    private float duration;
    private int length = 30;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool underPause = false;
    // Start is called before the first frame update
    void Start()
    {
        duration = length / GameInstance.speed;
        this.startPosition = transform.position;
        GameInstance.onGameOver += onGameOver;
        GameInstance.SpawnFinish += spawn;
        GameInstance.onResetGame += onReset;
        GameInstance.backToMainMenu += () =>
        {
            underPause = false;
            onReset();
        };
        GameInstance.onPause += () =>
        {
            underPause = true;
        };
        GameInstance.onResume += () =>
        {
            underPause = false;
        };
        GameInstance.onFinishHit += () =>
        {
            toga.SetActive(false);
        };
        this.InitPositions();
    }

    private void onReset()
    {
        this.InitPositions();
        toga.SetActive(true);
        this.stopMoving = true;
        underPause = false;
    }

    private void spawn()
    {
        StartMove();
    }

    private void onGameOver()
    {
        InitPositions();
        GetComponent<EdgeCollider2D>().enabled = true;
        timeElapsed = 0;
    }

    void InitPositions()
    {
        //this.startPosition = new Vector3(29.84876f, -3.60037f, 3.270748f);

        this.endPosition = new Vector3();
        this.transform.position = startPosition;
        endPosition.x = this.startPosition.x - Mathf.Cos(Mathf.Deg2Rad * degreeDirection) * length;
        endPosition.y = this.startPosition.y - Mathf.Sin(Mathf.Deg2Rad * degreeDirection) * length;
        endPosition.z = 0;
        this.timeElapsed = 0;
    }

    public UnityEvent onFinishHit;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.ToLower() == "player")
        {
            onFinishHit?.Invoke();
            GameInstance.onFinishHit?.Invoke();
            Debug.Log("character hit the finish line!!");
            this.stopMoving = true;
            return;
        }


        //CanvasQuiz.SetActive(true);
    }
    public void StartMove()
    {
        //this.Start();
        InitPositions();
        GetComponent<EdgeCollider2D>().enabled = true;
        timeElapsed = 0;
        stopMoving = false;
        Debug.Log("startmove called!");
    }
    private bool stopMoving = true;
    // Update is called once per frame
    void Update()
    {
        if (underPause) return;
        if (stopMoving) return;
        if (timeElapsed < duration)
        {
            var valueToLerp = Vector3.Lerp(this.startPosition, this.endPosition, timeElapsed / duration);
            transform.position = valueToLerp;
            timeElapsed += Time.deltaTime * GameInstance.speedScale;
        }
        else
        {
            stopMoving = true;
            Debug.Log("stop moving set to true in fixed update");
            //Destroy(gameObject);
        }
    }
}
