using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCoverSpawner : MonoBehaviour
{
    public GameObject CanvasCover;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.onResetGame += delegate ()
        {
            CanvasCover.SetActive(true);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
