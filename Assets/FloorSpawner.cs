using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    public GameObject floor;
    public GameObject startPos;
    public float degreeDirectionMovement = 30f;
    public float movementSpeed = 2f;
    public Vector2 sizeFloor;
    private Vector2 nextFloorPlace = new Vector2();
    private bool StopMovement = false;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.onQuizStart += StopTheMovement;
        GameInstance.onQuizAnswer += StartTheMovement;
        List<GameObject> temp = new List<GameObject>();
        //masalahnya adalah saat dimiringin kemudian menggunakan trigonometri (sudah menghasilkan angka yang benar)
        //tetapi masih numpuk
        //tetapi bila diluruskan (degree 0) dan ditambah 4f y nya itu bisa pas.
        //saat kondisi miring saja itu numpuk
        //padahal secara logika bila dimiringkan itu ukurannya tetap 4f(tidak berubah)
        sizeFloor.y = floor.transform.Find("Odd").gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        //sizeFloor.x = floor.transform.Find("Odd").gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        var position = startPos.transform.position;
        var go = Instantiate(floor, startPos.transform.position, startPos.transform.rotation);
        go.GetComponent<FloorMovement>().startPos = startPos.transform.position;
        go.GetComponent<FloorMovement>().degreeDirection = degreeDirectionMovement;
        go.GetComponent<FloorMovement>().movementSpeed = movementSpeed;
        temp.Add(go);

        nextFloorPlace = new Vector2();

        nextFloorPlace.y = (4f / Mathf.Sin(Mathf.Deg2Rad * 70f)) * Mathf.Sin(Mathf.Deg2Rad * 25f);
        nextFloorPlace.x = (4f / Mathf.Sin(Mathf.Deg2Rad * 70f)) * Mathf.Cos(Mathf.Deg2Rad * 25f);
        Debug.Log((4f / Mathf.Sin(Mathf.Deg2Rad * 65f)));

        position -= new Vector3(nextFloorPlace.x, nextFloorPlace.y);
        go = Instantiate(floor, position, startPos.transform.rotation);
        go.GetComponent<FloorMovement>().startPos = startPos.transform.position;
        go.GetComponent<FloorMovement>().degreeDirection = degreeDirectionMovement;
        go.GetComponent<FloorMovement>().movementSpeed = movementSpeed;
        temp.Add(go);

        position -= new Vector3(nextFloorPlace.x, nextFloorPlace.y);
        go = Instantiate(floor, position, startPos.transform.rotation);
        go.GetComponent<FloorMovement>().startPos = startPos.transform.position;
        go.GetComponent<FloorMovement>().degreeDirection = degreeDirectionMovement;
        go.GetComponent<FloorMovement>().movementSpeed = movementSpeed;
        temp.Add(go);

        position -= new Vector3(nextFloorPlace.x, nextFloorPlace.y);
        go = Instantiate(floor, position, startPos.transform.rotation);
        go.GetComponent<FloorMovement>().startPos = startPos.transform.position;
        go.GetComponent<FloorMovement>().degreeDirection = degreeDirectionMovement;
        go.GetComponent<FloorMovement>().movementSpeed = movementSpeed;
        temp.Add(go);

        position -= new Vector3(nextFloorPlace.x, nextFloorPlace.y);
        go = Instantiate(floor, position, startPos.transform.rotation);
        go.GetComponent<FloorMovement>().startPos = startPos.transform.position;
        go.GetComponent<FloorMovement>().degreeDirection = degreeDirectionMovement;
        go.GetComponent<FloorMovement>().movementSpeed = movementSpeed;
        temp.Add(go);

        position -= new Vector3(nextFloorPlace.x, nextFloorPlace.y);
        go = Instantiate(floor, position, startPos.transform.rotation);
        go.GetComponent<FloorMovement>().startPos = startPos.transform.position;
        go.GetComponent<FloorMovement>().degreeDirection = degreeDirectionMovement;
        go.GetComponent<FloorMovement>().movementSpeed = movementSpeed;
        temp.Add(go);

        position -= new Vector3(nextFloorPlace.x, nextFloorPlace.y);
        go = Instantiate(floor, position, startPos.transform.rotation);
        go.GetComponent<FloorMovement>().startPos = startPos.transform.position;
        go.GetComponent<FloorMovement>().degreeDirection = degreeDirectionMovement;
        go.GetComponent<FloorMovement>().movementSpeed = movementSpeed;
        temp.Add(go);

        floors = new List<GameObject>() { };
        while (temp.Count != 0)
        {
            floors.Add(temp[temp.Count - 1]);
            temp.RemoveAt(temp.Count - 1);
        }
        back = floors[floors.Count - 1];
    }

    private void StartTheMovement(CanvasQuiz.AnswerType obj)
    {
        //throw new NotImplementedException();
        StopMovement = false;
    }

    private void StopTheMovement()
    {
        StopMovement = true;
        //throw new NotImplementedException();
    }

    List<GameObject> floors = new List<GameObject>();
    private GameObject back;
    public float degreeDirection;
    // Update is called once per frame
    void Update()
    {
        Vector3 nextPos = new Vector3();
        nextPos.x = Mathf.Cos(Mathf.Deg2Rad * degreeDirection) * (movementSpeed * Time.deltaTime);
        nextPos.y = Mathf.Sin(Mathf.Deg2Rad * degreeDirection) * (movementSpeed * Time.deltaTime);


        var front = floors[0];
        Debug.Log(front.transform.position.y);
        Debug.Log(floors[1].transform.position.y);
        if (front.transform.position.y < -7f)
        {
            Debug.Log("offside floor");
            //front.transform.position = startPos.transform.position;
            floors.RemoveAt(0);
            front.transform.position = back.transform.position + new Vector3(nextFloorPlace.x, nextFloorPlace.y);
            floors.Add(front);
            back = front;

            //create game object 
        }
        if (!StopMovement)
            for (int i = 0; i < floors.Count; i++)
            {
                floors[i].transform.position -= nextPos;
            }

    }
}
