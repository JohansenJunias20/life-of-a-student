using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfekMinus : MonoBehaviour
{
    public Vector3 offSetPos;
    private Vector3 OriginPos;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        OriginPos = transform.localPosition;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        //PlayAnim();
    }
    private float elapsedTime = 1f;
    private void PlayAnim()
    {
        var sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = Color.white;
        transform.localPosition = OriginPos;
        elapsedTime = 0f;

        //throw new NotImplementedException();
    }
    IEnumerator Dispose()
    {
        yield return new WaitForSeconds(1f);

        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        Destroy(gameObject);
    }
    private bool disposing = false;
    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < duration)
        {
            var blend = Mathf.SmoothStep(0, 1, elapsedTime / duration);
            transform.localPosition = Vector3.Lerp(OriginPos, OriginPos + offSetPos, blend);
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, blend);
            elapsedTime += Time.deltaTime;
        }
        else
        {
            if (!disposing)
            {
                disposing = true;
                StartCoroutine(Dispose());
            }
        }
    }
}
