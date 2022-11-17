using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resume()
    {
        GameInstance.onResume?.Invoke();
        this.gameObject.SetActive(false);   

    }
    public void reset()
    {
        GameInstance.onResetGame?.Invoke();
        this.gameObject.SetActive(false);
        GameInstance.indexQuiz = -1;
        GameInstance.speedScale = 1;
    }
    public void mainMenu()
    {
        //GameInstance.onResetGame?.Invoke();
        GameInstance.backToMainMenu?.Invoke();
        this.gameObject.SetActive(false);
    }
}
