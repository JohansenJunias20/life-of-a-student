using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public enum ObstacleType
    {
        Game,
        Movie
    };
    private enum PositionSpawn
    {
        Left,
        Center,
        Right
    };
    public GameObject game, movie;
    public GameObject RespawnObstacle_Left, RespawnObstacle_Right, RespawnObstacle_Center;
    public float interval = 2f;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.onQuizStart += GameInstance_onQuizStart;
        GameInstance.onQuizSpawn += Gameinstance_onQuizSpawn;
        GameInstance.onQuizAnswer += QuizAnswer;
        GameInstance.SpawnFinish += onSpawnFinishLine;
        GameInstance.onGameOver += onGameOver;
        GameInstance.onFinishHit += onGameFinish;
        GameInstance.onResetGame += onReset;
        StartCoroutine(Spawner());
    }

    private void onReset()
    {
        this.stopSpawn = false;
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

    private void QuizAnswer(CanvasQuiz.AnswerType obj)
    {
        stopSpawn = false;
    }

    private void Gameinstance_onQuizSpawn()
    {
        stopSpawn = true;
    }

    private bool stopSpawn = false;
    private void GameInstance_onQuizStart()
    {
        stopSpawn = true;
    }
    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (stopSpawn) continue;
            var obstacleType = (ObstacleType)Random.Range(0, 2);
            var position = (PositionSpawn)Random.Range(0, 3);
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
                Instantiate(game, tr.position, tr.rotation);
                break;
            case ObstacleType.Movie:
                Instantiate(movie, tr.position, tr.rotation);
                break;
            default:
                break;
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
