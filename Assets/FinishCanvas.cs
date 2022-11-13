using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishCanvas : MonoBehaviour
{
    public TMPro.TextMeshProUGUI gradeText;
    public Image CircleGrade;
    public TMPro.TextMeshProUGUI title;
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
            gradeText.color = new Color(28f/255f,116f/255f,187f/255f, 1f);
            CircleGrade.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            title.text = "CONGRATS !";
        }
        else if(GameInstance.score >= 76)
        {
            gradeText.text = "B+";
            gradeText.color = new Color(28f/255f,116f/255f,187f/255f, 1f);
            CircleGrade.color = new Color(28f/255f,116f/255f,187f/255f, 1f);
            title.text = "CONGRATS !";
        }
        else if( GameInstance.score >= 69)
        {
            gradeText.text = "B";
            gradeText.color = new Color(28f/255f,116f/255f,187f/255f, 1f);
            CircleGrade.color = new Color(28f/255f,116f/255f,187f/255f, 1f);
            title.text = "CONGRATS !";
        }
        else if (GameInstance.score >= 61)
        {
            gradeText.text = "C+";
            gradeText.color = new Color(28f/255f,116f/255f,187f/255f, 1f);
            CircleGrade.color = new Color(28f/255f,116f/255f,187f/255f, 1f);
            title.text = "CONGRATS !";
        }
        else if (GameInstance.score >= 56)
        {
            gradeText.text = "C";
            gradeText.color = new Color(28f/255f,116f/255f,187f/255f, 1f);
            CircleGrade.color = new Color(28f/255f,116f/255f,187f/255f, 1f);
            title.text = "CONGRATS !";
        }
        else if(GameInstance.score >= 41)
        {
            gradeText.text = "D";
            gradeText.color = new Color(214f / 255f, 54f / 255f, 88f / 255f, 1f);
            CircleGrade.color = new Color(214f / 255f, 54f / 255f, 88f / 255f, 1f);
            title.text = "FAILED !";
        }
        else
        {
            gradeText.text = "E";
            gradeText.color = new Color(214f / 255f, 54f / 255f, 88f / 255f, 1f);
            CircleGrade.color = new Color(214f / 255f, 54f / 255f, 88f / 255f, 1f);
            title.text = "FAILED !";
        }
    }

    // Update is called once per frame
    void Update()
    {
        countScore();
    }
}
