using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishCanvas : MonoBehaviour
{
    public TMPro.TextMeshProUGUI gradeText;
    public TMPro.TextMeshProUGUI kelulusan;
    // Start is called before the first frame update
    void Start()
    {
        GameInstance.onResetGame += onReset;
    }

    private void onReset()
    {
        this.gameObject.SetActive(false);
    }

    private void countScore()
    {
        if (GameInstance.score >= 86)
        {
            gradeText.text = "A";
            kelulusan.text = "LULUS";
        }
        else if(GameInstance.score >= 76)
        {
            gradeText.text = "B+";
            kelulusan.text = "LULUS";
        }
        else if( GameInstance.score >= 69)
        {
            gradeText.text = "B";
            kelulusan.text = "LULUS";
        }
        else if (GameInstance.score >= 61)
        {
            gradeText.text = "C+";
            kelulusan.text = "LULUS";
        }
        else if (GameInstance.score >= 56)
        {
            gradeText.text = "C";
            kelulusan.text = "LULUS";
        }
        else if(GameInstance.score >= 41)
        {
            gradeText.text = "D";
            kelulusan.text = "TIDAK LULUS";
        }
        else
        {
            gradeText.text = "E";
            kelulusan.text = "TIDAK LULUS";
        }
    }

    // Update is called once per frame
    void Update()
    {
        countScore();
    }
}
