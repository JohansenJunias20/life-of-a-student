using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    public bool isWin()
    {
        return win;
    }
    private bool win = false;
    public UnityEvent onPlayMarkerSFX, onPlayWinSound, onPlayLoseSound;
    public void playSound()
    {
        if (title.text == "CONGRATS !")
            onPlayWinSound?.Invoke();
        else
            onPlayLoseSound?.Invoke();

    }
    public void playMarkerSFX()
    {
        onPlayMarkerSFX?.Invoke();
    }
    public void countScore()
    {
        if (GameInstance.score >= 86)
        {
            win = true;
            gradeText.text = "A";
            gradeText.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            CircleGrade.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            title.text = "CONGRATS !";
        }
        else if (GameInstance.score >= 76)
        {
            win = true;
            gradeText.text = "B+";
            gradeText.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            CircleGrade.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            title.text = "CONGRATS !";
        }
        else if (GameInstance.score >= 69)
        {
            win = true;
            gradeText.text = "B";
            gradeText.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            CircleGrade.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            title.text = "CONGRATS !";
        }
        else if (GameInstance.score >= 61)
        {
            win = true;
            gradeText.text = "C+";
            gradeText.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            CircleGrade.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            title.text = "CONGRATS !";
        }
        else if (GameInstance.score >= 56)
        {
            win = true;
            gradeText.text = "C";
            gradeText.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            CircleGrade.color = new Color(28f / 255f, 116f / 255f, 187f / 255f, 1f);
            title.text = "CONGRATS !";
        }
        else if (GameInstance.score >= 41)
        {
            win = false;
            gradeText.text = "D";
            gradeText.color = new Color(214f / 255f, 54f / 255f, 88f / 255f, 1f);
            CircleGrade.color = new Color(214f / 255f, 54f / 255f, 88f / 255f, 1f);
            title.text = "FAILED !";
        }
        else
        {
            win = false;
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
