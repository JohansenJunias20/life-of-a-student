using UnityEngine;
using System.Collections;
using static CanvasQuiz;

public class TimerShake : MonoBehaviour
{
    private void Start()
    {
        GameInstance.ReduceHealth += (health) =>
        {
            StartCoroutine(Slow(0.5f));
            StartCoroutine(Shake(0.5f, 0.15f));

        };
        GameInstance.onQuizAnswer += (quizAnswer) =>
        {
            if (quizAnswer == AnswerType.Correct) return;
            StartCoroutine(Slow(0.5f));
            StartCoroutine(Shake(0.5f, 0.2f));

        };
    }
    bool currentSlow = false;
    private float originalSpeedScale = 0;
    public IEnumerator Slow(float duration)
    {
        if (currentSlow)
        {
            GameInstance.speedScale = 0.6f * originalSpeedScale;
            yield return new WaitForSeconds(duration);
            GameInstance.speedScale = originalSpeedScale;
            currentSlow = false;
            yield break;
        };
        currentSlow = true;
        //var originalSpeed = GameInstance.speed;
        originalSpeedScale = GameInstance.speedScale;
        GameInstance.speedScale = 0.6f * originalSpeedScale;
        yield return new WaitForSeconds(duration);
        GameInstance.speedScale = originalSpeedScale;
        currentSlow = false;

    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
    }

}