using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnswer : MonoBehaviour
{
    public GameObject canvasQuiz;
    // Start is called before the first frame update
    public void onClick(int index)
    {
        Debug.Log("answering..");
        canvasQuiz.GetComponent<CanvasQuiz>().Answer(index);
    }
}
