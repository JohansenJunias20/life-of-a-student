using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CanvasQuiz;
using static ObstacleSpawner;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject posLeft, posCenter, posRight, toga;
    private string currentPosition = "center";
    public Animator animator;
    void Start()
    {
        GameInstance.onResetGame += onReset;
        GameInstance.onPause += () =>
        {
            stopMovement = true;
            animator.SetBool("Running", false);
        };
        GameInstance.onResume += () =>
        {
            stopMovement = false;
            animator.SetBool("Running", true);
        };
        GameInstance.onFinishHit += delegate ()
        {
            stopMovement = true;
            toga.SetActive(true);
            toga.GetComponent<Animator>().Play("TogaAnimasi");
            animator.SetBool("Running", false);
            StartCoroutine(delayFinishFeedback());
        };
        GameInstance.onResetGame += delegate ()
        {
            animator.SetBool("Running", true);
            stopMovement = false;
        };
        GameInstance.onFeedbackAnswerDone += delegate ()
        {
            animator.SetBool("Running", true);
            stopMovement = false;
        };
        GameInstance.onQuizStart += delegate ()
        {
            stopMovement = true;
            animator.SetBool("Running", false);
        };
        GameInstance.onGameOver += delegate ()
        {
            animator.SetBool("Running", false);
            stopMovement = true;
        };
        GameInstance.onStart += delegate ()
        {
            animator.SetBool("Running", true);
            stopMovement = false;
        };
        this.gameObject.transform.position = posCenter.transform.position;
    }
    IEnumerator delayFinishFeedback()
    {
        yield return new WaitForSeconds(2);
        GameInstance.onFinishCanvasShow?.Invoke();
    }
    private void onReset()
    {
        toga.SetActive(false);
        this.gameObject.transform.position = posCenter.transform.position;
        currentPosition = "center";
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("character hit something");
        var result = other.gameObject.GetComponent<ObstacleMovement>();
        if (result == null) return;
        Destroy(other.gameObject);
        GameInstance.ReduceHealth?.Invoke(result.damage);
        StartCoroutine(Blinking());
    }
    bool blinking = false;
    IEnumerator Blinking()
    {
        //if (blinking) yield break;
        //blinking = true;
        var i = 0;
        //throw new System.NotImplementedException();
        while (true)
        {
            if (i == 3) break;
            this.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.25f);
            this.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.25f);
            Debug.Log("blingking!");
            i++;
        }
        //blinking = false;
    }

    bool stopMovement = true;
    // Update is called once per frame
    void Update()
    {
        //animator.speed = 0;
        animator.speed = GameInstance.speedScale;
        if (stopMovement) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveCharacter("left");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveCharacter("right");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(GameInstance.speed);
            Debug.Log(GameInstance.speedScale);
        }
    }
    float elapsedTime = 0;
    private bool currentTransition = false;

    Vector3 getPosition(string direction)
    {
        switch (direction)
        {
            case "left":
                return posLeft.transform.position;
                break;
            case "right":
                return posRight.transform.position;
                break;
            case "center":
                return posCenter.transform.position;
                break;
            default:
                break;
        }
        return new Vector3();
    }
    string prevDirection = "center";
    IEnumerator Move()
    {
        var duration = 0.25f;
        if (currentTransition)
        {
            Debug.Log("under transition!");
            var startPos = transform.position;
            elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                var alpha = elapsedTime / duration;
                //Debug.Log(this.currentPosition);
                var blend = Mathf.SmoothStep(0, 1, alpha);
                transform.position = Vector3.Lerp(startPos, getPosition(this.currentPosition), blend);
                //Debug.Log(alpha);
                yield return 0;
            }
            currentTransition = false;
            yield break;
        }

        elapsedTime = 0;
        currentTransition = true;
        Debug.Log("not under transition!");

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var alpha = elapsedTime / duration;
            //Debug.Log(this.currentPosition);
            var blend = Mathf.SmoothStep(0, 1, alpha);
            transform.position = Vector3.Lerp(getPosition(this.prevDirection), getPosition(this.currentPosition), blend);
            //Debug.Log(alpha);
            yield return 0;
        }
        currentTransition = false;

    }
    void MoveCharacter(string direction)
    {

        if (direction == "right")
        {
            if (this.currentPosition == "right")
            {
                //sudah paling kanan
                return;
            }
        }
        if (direction == "left")
        {
            if (this.currentPosition == "left")
            {
                //sudah paling kiri
                return;
            }
        }
        var nextPos = getNextPosition(direction);
        if (nextPos == null) throw new System.Exception("nextPos NULL");
        if (nextPos != this.currentPosition)
        {
            //elapsedTime = 0;
        }
        this.prevDirection = this.currentPosition;
        this.currentPosition = nextPos;
        if (currentTransition)
        {
            if (prev != null)
                StopCoroutine(prev);
        }
        prev = StartCoroutine(Move());
        return;
        this.currentPosition = nextPos;
        switch (this.currentPosition)
        {
            case "left":
                this.gameObject.transform.position = posLeft.transform.position;
                break;
            case "right":
                this.gameObject.transform.position = posRight.transform.position;
                break;
            case "center":
                this.gameObject.transform.position = posCenter.transform.position;
                break;
            default:
                break;
        }
    }
    Coroutine prev = null;
    string getNextPosition(string direction)
    {
        if (direction == "left" && currentPosition == "center")
        {
            return "left";
        }

        if (direction == "right" && currentPosition == "center")
        {
            return "right";
        }

        if (direction == "right" && currentPosition == "left")
        {
            return "center";
        }

        if (direction == "left" && currentPosition == "right")
        {
            return "center";
        }
        return null;
    }
}
