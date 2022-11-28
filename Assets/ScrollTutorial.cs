using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollTutorial : MonoBehaviour
{
    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;
    int posisi = 0;
    public TMPro.TextMeshProUGUI tmpro;
    public Sprite CircleActive, CircleInactive;
    public Image[] circle;
    public GameObject buttonNext, buttonPrev;
    public GameObject OKButton;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.onHTPOpen += () => {
            posisi = 0;
            scroll_pos = 0;
        };
    }
    public void next()
    {
        if (posisi < pos.Length - 1)
        {
            posisi += 1;
            scroll_pos = pos[posisi];
        }

    }
    public void prev()
    {
        if (posisi > 0)
        {
            posisi -= 1;
            scroll_pos = pos[posisi];
        }


    }
    // Update is called once per frame
    void Update()
    {
        if (posisi == 0)
        {
            buttonPrev.SetActive(false);
            buttonNext.SetActive(true);
            circle[0].sprite = CircleActive;
            circle[1].sprite = CircleInactive;
            circle[2].sprite = CircleInactive;
            OKButton.active = false;
        }
        else if (posisi == 1)
        {
            buttonNext.SetActive(true);
            buttonPrev.SetActive(true);
            circle[0].sprite = CircleInactive;
            circle[1].sprite = CircleActive;
            circle[2].sprite = CircleInactive;
            OKButton.active = false;
        }
        else
        {
            buttonPrev.SetActive(true);
            buttonNext.SetActive(false);
            circle[0].sprite = CircleInactive;
            circle[1].sprite = CircleInactive;
            circle[2].sprite = CircleActive;
            OKButton.active = true;
        }

        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            //Debug.Log("scroll pos seted");
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.15f);
                    posisi = i;
                }
            }
        }
        if (posisi == 0)
        {
            tmpro.text = "HOTKEYS";
        }
        else
        {
            tmpro.text = "HOW TO PLAY";
        }
    }

}
