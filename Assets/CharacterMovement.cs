using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObstacleSpawner;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject posLeft, posCenter, posRight;
    private string currentPosition = "center";
    void Start()
    {
        GameInstance.onResetGame += onReset;
        this.gameObject.transform.position = posCenter.transform.position;
    }
    private void onReset()
    {
        this.gameObject.transform.position = posCenter.transform.position;
        currentPosition = "center";
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("character hit something");
        var result = other.gameObject.GetComponent<ObstacleMovement>();
        if (result == null) return;
        Destroy(other.gameObject);
        GameInstance.ReduceHealth?.Invoke(result.damage);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCharacter("left");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCharacter("right");
        }
    }

    void MoveCharacter(string direction)
    {
        if (direction == "right")
        {
            if (this.currentPosition == "right")
            {
                //sudah paling kanan
                return;
            }
        }
        if (direction == "left")
        {
            if (this.currentPosition == "left")
            {
                //sudah paling kiri
                return;
            }
        }
        var nextPos = getNextPosition(direction);
        if (nextPos == null) throw new System.Exception("nextPos NULL");
        this.currentPosition = nextPos;
        switch (this.currentPosition)
        {
            case "left":
                this.gameObject.transform.position = posLeft.transform.position;
                break;
            case "right":
                this.gameObject.transform.position = posRight.transform.position;
                break;
            case "center":
                this.gameObject.transform.position = posCenter.transform.position;
                break;
            default:
                break;
        }
    }
    string getNextPosition(string direction)
    {
        if (direction == "left" && currentPosition == "center")
        {
            return "left";
        }

        if (direction == "right" && currentPosition == "center")
        {
            return "right";
        }

        if (direction == "right" && currentPosition == "left")
        {
            return "center";
        }

        if (direction == "left" && currentPosition == "right")
        {
            return "center";
        }
        return null;
    }
}
