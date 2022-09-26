using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObstacleSpawner;

public class ObstacleMovement : MonoBehaviour
{
    public int degreeDirection = 25;
    float timeElapsed = 0;
    public float duration = 3.5f;
    public int length = 30;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public ObstacleType type;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {

        this.startPosition = transform.position;
        this.endPosition = new Vector3();
        endPosition.x = this.startPosition.x - Mathf.Cos(Mathf.Deg2Rad * degreeDirection) * length;
        endPosition.y = this.startPosition.y - Mathf.Sin(Mathf.Deg2Rad * degreeDirection) * length;
        endPosition.z = 0;
        GameInstance.onQuizDone += GameInstance_onQuizDone;
        GameInstance.onQuizStart += GameInstance_onQuizStart;
        GameInstance.onQuizSpawn += GameInstance_onQuizSpawn; 
        GameInstance.onQuizAnswer += QuizAnswer;
        GameInstance.onGameOver += onGameOver;
        GameInstance.onFinishHit += onGameFinish;
        GameInstance.onResetGame += onReset;

    }

    private void onReset()
    {
        //stopMovement = false;
        stopMovement = false;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private void onGameFinish()
    {
        //throw new NotImplementedException();
        stopMovement = true;
    }

    private void onGameOver()
    {
        stopMovement = true;
    }

    private void QuizAnswer(CanvasQuiz.AnswerType obj)
    {
        stopMovement = false;
    }

    private void GameInstance_onQuizStart()
    {
        stopMovement = true;
    }
    private void GameInstance_onQuizSpawn()
    {
        //stopMovement = true;
    }

    private bool stopMovement = false;
    private void GameInstance_onQuizDone()
    {
        stopMovement = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (stopMovement) return;
        if (timeElapsed < duration)
        {
            var valueToLerp = Vector3.Lerp(this.startPosition, this.endPosition, timeElapsed / duration);
            transform.position = valueToLerp;
            timeElapsed += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
