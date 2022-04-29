using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControlller : MonoBehaviour
{
    [SerializeField]
    Transform knifeTransform = null;
    [SerializeField]
    float moveDistance = 5f;
    Coroutine moveCoroutine = null;
    bool underGravity = false;
    float gravity = 9.8f;
    Vector3 defaultRotation = Vector3.zero;
    [SerializeField]
    GameEvent gameOverEvent = null;

    // Update is called once per frame
    void Update()
    {
        if (underGravity)
        {
            knifeTransform.position = new Vector3(knifeTransform.position.x, knifeTransform.position.y - gravity * Time.deltaTime, knifeTransform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damager")
        {
            gameOverEvent.Invoke();
        }
        else if (other.tag == "Stopper")
        {
            underGravity = false;
            if (moveCoroutine != null)
            {
                StopAllCoroutines();
                moveCoroutine = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Stopper")
        {
            underGravity = true;
        }
    }

    public void React()
    {
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MoveCoroutine());
        }
    }

    IEnumerator RotateCoroutine()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        float timer = 0f;
        float animationTime = 1f;
        Vector3 startRotation = knifeTransform.eulerAngles;
        Vector3 endRotation = defaultRotation;
        endRotation.x += 360f;

        do
        {
            timer += Time.deltaTime / animationTime;
            knifeTransform.eulerAngles = Vector3.Lerp(startRotation, endRotation, curve.Evaluate(timer));
            yield return wait;
        }
        while (timer < 1f);
    }

    public void FirstMoveAndRotation()
    {
        moveCoroutine = StartCoroutine(MoveCoroutine(true));
        underGravity = true;
    }

    IEnumerator FirstRotateCoroutine()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        float timer = 0f;
        float animationTime = 1f;
        Vector3 startRotation = knifeTransform.eulerAngles;
        Vector3 endRotation = startRotation;
        endRotation.x -= 250f;

        do
        {
            timer += Time.deltaTime / animationTime;
            knifeTransform.eulerAngles = Vector3.Lerp(startRotation, endRotation, curve.Evaluate(timer));
            yield return wait;
        }
        while (timer < 1f);

        defaultRotation = knifeTransform.eulerAngles;
    }

    IEnumerator MoveCoroutine(bool firstMove = false)
    {
        if (!firstMove)
        {
            StartCoroutine(RotateCoroutine());
        }
        else
        {
            StartCoroutine(FirstRotateCoroutine());
        }

        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        float timer = 0f;
        float animationTime = 1f;
        Vector3 startPosition = knifeTransform.position;
        Vector3 endPosition = startPosition;
        endPosition.z += moveDistance;
        endPosition.y += moveDistance;

        do
        {
            timer += Time.deltaTime / animationTime;
            knifeTransform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(timer));
            yield return wait;
        }
        while (timer < 1f);

        moveCoroutine = null;
    }
}
