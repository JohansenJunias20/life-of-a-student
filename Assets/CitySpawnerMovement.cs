using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawnerMovement : MonoBehaviour
{
    public Sprite atas;
    public Sprite bawah;
    private List<GameObject> Latas = new List<GameObject>();
    private List<GameObject> Lbawah = new List<GameObject>();
    private Vector3 pAtas0 = new Vector3(3f, 7.949f, 0);
    private Vector3 pAtas1 = new Vector3(28.5f, 23.370f, 0);
    private Vector3 pBawah0 = new Vector3(11.63f, -4.17f, 0);
    private Vector3 pBawah1 = new Vector3(35.92f, 11.02f, 0);
    private float degreeDirection = 30;
    private float movementSpeed;
    private bool stopMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = GameInstance.speed;
        GameInstance.onQuizStart += StopTheMovement;
        GameInstance.onGameOver += StopTheMovement;
        GameInstance.onFinishHit += StopTheMovement;
        GameInstance.onResetGame += StartTheMovement;
        GameInstance.onStart += StartTheMovement;
        GameInstance.onQuizAnswer += (a) => { StartTheMovement(); };
        GameObject go_atas_0 = new GameObject();
        var spriteRenderer = go_atas_0.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -1;
        go_atas_0.transform.position = pAtas0;
        spriteRenderer.sprite = atas;

        GameObject go_atas_1 = new GameObject();
        var spriteRenderer1 = go_atas_1.AddComponent<SpriteRenderer>();
        spriteRenderer1.sortingOrder = -1;
        go_atas_1.transform.position = pAtas1;
        spriteRenderer1.sprite = atas;

        GameObject go_bawah_0 = new GameObject();
        var spriteRenderer2 = go_bawah_0.AddComponent<SpriteRenderer>();
        spriteRenderer2.sprite = bawah;
        spriteRenderer2.sortingOrder = 1;
        go_bawah_0.transform.position = pBawah0;

        GameObject go_bawah_1 = new GameObject();
        var spriteRenderer3 = go_bawah_1.AddComponent<SpriteRenderer>();
        go_bawah_1.transform.position = pBawah1;
        spriteRenderer3.sortingOrder = 1;
        spriteRenderer3.sprite = bawah;

        Latas.Add(go_atas_0);
        Latas.Add(go_atas_1);
        Lbawah.Add(go_bawah_0);
        Lbawah.Add(go_bawah_1);
    }

    private void StartTheMovement()
    {
        //throw new NotImplementedException();
        stopMovement = false;
    }

    private void StopTheMovement()
    {
        stopMovement = true;
        //throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopMovement) return;
        Vector3 nextPos = new Vector3();
        nextPos.x = Mathf.Cos(Mathf.Deg2Rad * degreeDirection) * (Time.deltaTime * movementSpeed * GameInstance.speedScale);
        nextPos.y = Mathf.Sin(Mathf.Deg2Rad * degreeDirection) * (Time.deltaTime * movementSpeed * GameInstance.speedScale);
        foreach (var item in Latas)
        {
            if (item.transform.position.y < -7f)
            {
                item.transform.position = pAtas1;
            }
            item.transform.position -= nextPos;
        }

        foreach (var item in Lbawah)
        {
            if (item.transform.position.y < -19f)
            {
                item.transform.position = pBawah1;
            }
            item.transform.position -= nextPos;
        }
    }
}
