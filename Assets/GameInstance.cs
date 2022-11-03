using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CanvasQuiz;

public class GameInstance : MonoBehaviour
{
    public static Action onStart { get; set; }
    public static Action onHTPOpen { get; set; }
    public static Action onResetGame { get; set; }
    public static int score;
    public static int indexQuiz = -1;
    // ini constant tidak bisa dirubah, untuk merubah speed pakai speedScale
    public static float speed = 8f;
    //value between 0-1 tujuannya untuk merubah speed sewaktu2
    public static float speedScale = 1f;
    //perintah untuk ngespawn Finish, dipanggil di QuizMovement, dilisten di FinishSpawner
    public static Action SpawnFinish { get; set; }
    //saat finish Game Object hit ke character, dipanggil di FinishMovement, dilisten 
    //hampir semua karena semua activity harus di stop saat finish line sudah ke hit character
    public static Action onFinishHit { get; set; }
    public static Action onLastQuizAnswered { get; set; }
    public static Action onGameOver { get; set; }
    public static Action<float> ReduceHealth { get; set; }
    public static Action onQuizStart { get; set; }
    public static Action onBeforeQuizSpawn { get; set; }
    public static Action onQuizSpawn { get; set; }
    public static Action<AnswerType> onQuizAnswer { get; set; }
    public static event Action onQuizDone;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
