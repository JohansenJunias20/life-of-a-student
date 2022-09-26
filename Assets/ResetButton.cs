using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public GameObject gameOver = null;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void onClick()
    {
        if(gameOver!= null)
        {
            gameOver.SetActive(false);
        }
        GameInstance.onResetGame?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
