using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMovement : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }
    public Vector2 startPos;
    public float movementSpeed;
    public float degreeDirection;
    // Update is called once per frame
    void Update()
    {
        //if (this.gameObject.transform.position.y < -7f)
        //{
        //    this.gameObject.transform.position = startPos;
        //}

        //Vector3 nextPos = new Vector3();
        //nextPos.x = Mathf.Cos(Mathf.Deg2Rad * degreeDirection) * (movementSpeed * Time.deltaTime);
        //nextPos.y = Mathf.Sin(Mathf.Deg2Rad * degreeDirection) * (movementSpeed * Time.deltaTime);
        //this.gameObject.transform.position -= nextPos;
    }
}
