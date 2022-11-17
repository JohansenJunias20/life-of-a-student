using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpawner : MonoBehaviour
{
    public GameObject GO_MainMenu;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.backToMainMenu += () =>
         {
             GO_MainMenu.SetActive(true);
         };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
