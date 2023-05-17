using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float leftDistance;
    public float rightDistance;
    public float lerpSpeed;
    public float waitTime;
    private bool goingRight = true;
    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        StartCoroutine(PatrolFunction());
    }

    IEnumerator PatrolFunction()
    {
        while (true)
        {
            if (goingRight)
            {
                Vector3 target;
                target = transform.right.normalized * rightDistance;
                target = startPos + target;
                while (Vector3.Distance(transform.position, target) > 0.1f)
                {


                    transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * lerpSpeed);
                    yield return null;
                }
                goingRight = false;
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                Vector3 target;
                target = transform.right.normalized * leftDistance;
                target = startPos - target;
                while (Vector3.Distance(transform.position, target) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * lerpSpeed);
                    yield return null;
                }
                goingRight = true;
                yield return new WaitForSeconds(waitTime);
            }

        }
    }
}