using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public enum ObstacleType
    {
        Game,
        Movie,
        Sleep
    };
    private enum PositionSpawn
    {
        Left,
        Center,
        Right
    };
    public GameObject game, movie;
    public GameObject RespawnObstacle_Left, RespawnObstacle_Right, RespawnObstacle_Center;
    private float startInterval = 0.8f;
    private float interval;
    // Start is called before the first frame update
    void Start()
    {
        interval = startInterval;
        GameInstance.onQuizStart += GameInstance_onQuizStart;
        GameInstance.onQuizSpawn += Gameinstance_onQuizSpawn;
        GameInstance.onFeedbackAnswerDone += onFeedbackAnswerDone;
        GameInstance.SpawnFinish += onSpawnFinishLine;
        GameInstance.onGameOver += onGameOver;
        GameInstance.onFinishHit += onGameFinish;
        GameInstance.onResetGame += onReset;
        GameInstance.onStart += onReset;
        GameInstance.backToMainMenu += () =>
        {
            if (cour != null)
                StopCoroutine(cour);
            destroyObstacles();
            stopSpawn = true;
            this.underPause = false;
        };
        GameInstance.onResume += () =>
        {
            stopSpawn = false;
            underPause = false;
        };
        GameInstance.onPause += () =>
        {
            //stopSpawn = true;
            underPause = true;
        };
        GameInstance.onLastQuizAnswered += () =>
        {
            cour = StartCoroutine(delayStop());
        };
        StartCoroutine(Spawner());
    }
    Coroutine cour = null;
    IEnumerator delayStop()
    {
        var duration = 14;
        var i = 0;
        while (i < duration)
        {
            if (underPause) yield return 0;
            else
            {
                yield return new WaitForSeconds(1);
                i++;
            }
        }
        while (underPause)
        {
            yield return null;
        }
        stopSpawn = true;
    }

    private void onReset()
    {
        this.stopSpawn = false;
        this.interval = startInterval;
        this.underPause = false;
        if (cour != null)
            StopCoroutine(cour);
        //StartCoroutine(Spawner());
        destroyObstacles();
    }
    private void destroyObstacles()
    {
        this.obstacles.ForEach(obstacle =>
        {
            if (obstacle.gameObject != null)
                Destroy(obstacle.gameObject);
        });
        this.obstacles.Clear();
    }
    private void onGameFinish()
    {
        stopSpawn = true;
        //throw new System.NotImplementedException();
    }

    private void onSpawnFinishLine()
    {
        stopSpawn = true;
        //throw new System.NotImplementedException();
    }

    private void onGameOver()
    {
        this.stopSpawn = true;
    }

    private void onFeedbackAnswerDone()
    {
        stopSpawn = false;
        interval = startInterval / (GameInstance.speedScale);
    }

    private void Gameinstance_onQuizSpawn()
    {
        stopSpawn = true;
    }

    private bool stopSpawn = true;
    private bool underPause;

    private void GameInstance_onQuizStart()
    {
        stopSpawn = true;
    }
    IEnumerator Spawner()
    {

        while (true)
        {
            //yield return new WaitForSeconds(interval);
            var timeCount = 0f;
            while (timeCount < interval)
            {
                if (!underPause)
                    timeCount += Time.deltaTime;
                yield return 0;
            }
            //Debug.Log("test!");

            if (stopSpawn)
            {
                continue;
            };
            var obstacleType = (ObstacleType)Random.Range(0, 3);
            var position = (PositionSpawn)Random.Range(0, 3);

            if (Random.Range(0, 2) == 0)
            {
                var newposition = position;
                //while (newposition == position)
                //{
                //    newposition = (PositionSpawn)Random.Range(0, 3);

                //}
                newposition += 1;
                newposition = (PositionSpawn)((int)newposition % 3);
                var newobstacleType = (ObstacleType)Random.Range(0, 3);
                SpawnObject(newobstacleType, newposition);
            }
            SpawnObject(obstacleType, position);
        }
    }
    void SpawnObject(ObstacleType obstacle, PositionSpawn position)
    {
        //set default value only
        Transform tr = RespawnObstacle_Center.transform;
        switch (position)
        {
            case PositionSpawn.Left:
                tr = RespawnObstacle_Left.transform;
                break;
            case PositionSpawn.Center:
                tr = RespawnObstacle_Right.transform;
                break;
            case PositionSpawn.Right:
                tr = RespawnObstacle_Center.transform;
                break;
            default:
                break;
        };
        switch (obstacle)
        {

            case ObstacleType.Game:
                var go = Instantiate(game, tr.position, tr.rotation);
                obstacles.Add(go);
                break;
            case ObstacleType.Movie:
                var goM = Instantiate(movie, tr.position, tr.rotation);
                obstacles.Add(goM);
                break;
            case ObstacleType.Sleep:
                var goS = Instantiate(sleep, tr.position, tr.rotation);
                obstacles.Add(goS);
                break;
            default:
                break;
        }

    }
    List<GameObject> obstacles = new List<GameObject>();
    public GameObject sleep;

    // Update is called once per frame
    void Update()
    {

    }
}
