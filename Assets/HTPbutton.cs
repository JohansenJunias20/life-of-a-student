using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTPbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Canvas CanvasHTP;
    public GameObject GO_CanvasCover;
    public void onClick()
    {
        CanvasHTP.gameObject.SetActive(false);
        GO_CanvasCover.SetActive(true);
        //GameInstance.onStart?.Invoke();
    }
}
