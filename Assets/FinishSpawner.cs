using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSpawner : MonoBehaviour
{
    public GameObject FinishCanvas;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.onLastQuizAnswered += onLastQuizAnswered;
        GameInstance.onFinishHit += showFinishCanvas;
        GameInstance.onGameOver += onGameOver;
        GameInstance.onResetGame += onReset;
    }
    private bool gameOver = false;
    private void onGameOver()
    {
        gameOver = true;
        //throw new NotImplementedException();
    }

    private void onReset()
    {
        gameOver = false;
        FinishCanvas.SetActive(false);

    }

    private void showFinishCanvas()
    {
        FinishCanvas.SetActive(true);
        //throw new NotImplementedException();
    }

    public float distance = 75;
    private void onLastQuizAnswered()
    {

        StartCoroutine(spawnFinish());
    }
    IEnumerator spawnFinish()
    {
        Debug.Log("SPAWNING FINISH... IN 15SECS");
        yield return new WaitForSeconds(15);
        Debug.Log("SPAWNING FINISH");
        if (!gameOver)
            GameInstance.SpawnFinish?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
