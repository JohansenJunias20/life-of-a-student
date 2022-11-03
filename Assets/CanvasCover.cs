using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCover : MonoBehaviour
{
    public GameObject go_HTP;
    public GameObject go_CanvasCover;
 
    public void onPlayClicked()
    {
        //PlayerPrefs->DeleteAll();
        GameInstance.onStart?.Invoke();
        Debug.Log("start invoke..");
        go_CanvasCover.SetActive(false);
    }
    public void onHTPClicked()
    {
        //GameInstance.onHTPOpen?.Invoke();
        go_HTP.SetActive(true);
        //this.gameObject.SetActive(false);
        go_CanvasCover.SetActive(false);
    }
}
