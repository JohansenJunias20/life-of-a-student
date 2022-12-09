using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishSpawner : MonoBehaviour
{
    public GameObject FinishCanvas;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.onLastQuizAnswered += onLastQuizAnswered;
        GameInstance.onFinishCanvasShow += showFinishCanvas;
        GameInstance.backToMainMenu += delegate ()
        {
            if (cour != null)
            {
                StopCoroutine(cour);
            }
            onReset();
            underPause = false;
        };
        GameInstance.onGameOver += onGameOver;
        GameInstance.onResetGame += onReset;
        GameInstance.onPause += () =>
        {
            underPause = true;
        };
        GameInstance.onResume += () =>
        {
            underPause = false;
        };
        GameInstance.onStart += () =>
        {
            underPause = false;
        };
    }
    private bool gameOver = false;
    private void onGameOver()
    {
        gameOver = true;
        if (cour != null)
        {
            StopCoroutine(cour);
            Debug.Log("couroutine Stopped!");
        }
        //throw new NotImplementedException();
    }

    private void onReset()
    {
        underPause = false;
        gameOver = false;
        FinishCanvas.SetActive(false);
        if (cour != null)
        {
            StopCoroutine(cour);
            Debug.Log("couroutine Stopped!");
        }

    }

    private void showFinishCanvas()
    {
        onShowCanvas?.Invoke();
        FinishCanvas.SetActive(true);
        if (FinishCanvas.GetComponent<FinishCanvas>().isWin())
        {
            onWin?.Invoke();
        }
        else
        {
            onLose?.Invoke();
        }
        //throw new NotImplementedException();
    }

    public UnityEvent onShowCanvas, onLose, onWin;
    public float distance = 75;
    private void onLastQuizAnswered()
    {

        cour = StartCoroutine(spawnFinish());
    }
    Coroutine cour = null;
    private bool underPause = false;
    IEnumerator spawnFinish()
    {
        Debug.Log("SPAWNING FINISH... IN 15SECS");
        //WAITFORSECONDS walaupun dipause tetap jalan. tidak bisa pakai while loop yg di line 95
        int duration = 15;
        int i = 0;
        Debug.Log("under pause:");
        Debug.Log(underPause);
        while (i < duration)
        {
            //Debug.Log(underPause);
            if (underPause) { yield return 0; }
            else
            {
                yield return new WaitForSeconds(1);
                //Debug.Log("tick SPAWN FINISH");
                i++;
            }
        }
        Debug.Log("SPAWNING FINISH");

        if (gameOver) yield return 0;

        while (underPause)
        {
            yield return null;
        }
        GameInstance.SpawnFinish?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
