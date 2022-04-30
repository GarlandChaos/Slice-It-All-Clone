using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

namespace GameSystem
{
    public class KnifeControlller : MonoBehaviour
    {
        [SerializeField]
        Transform knifeTransform = null;
        [SerializeField]
        float moveDistance = 5f;
        [SerializeField]
        InterpolationSettings movementSettings = null;
        Coroutine moveCoroutine = null;
        float firstRotationAdjustment = -250f;
        float normalRotationAdjustment = 360f;
        [SerializeField]
        Transform rotationReference = null;
        bool underGravity = false;
        float gravity = 9.8f;
        Vector3 defaultRotation = Vector3.zero;
        [SerializeField]
        GameEvent gameOverEvent = null;

        void Update()
        {
            BeAffectedByGravity();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Damager")
            {
                Stop();
                gameOverEvent.Invoke();
            }
            else if (other.tag == "Stopper")
            {
                Stop();
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

        public void BeAffectedByGravity()
        {
            if (underGravity)
            {
                Vector3 newPos = knifeTransform.position;
                newPos.y -= gravity * Time.deltaTime;
                knifeTransform.position = newPos;
            }
        }

        public void Stop()
        {
            underGravity = false;
        }

        public void FirstMoveAndRotation()
        {
            moveCoroutine = StartCoroutine(MoveCoroutine(true));
            underGravity = true;
        }

        IEnumerator RotateCoroutine(float rotationAdjustment, bool firstMove = false)
        {
            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            float timer = 0f;
            Vector3 startRotation = knifeTransform.eulerAngles;
            Vector3 endRotation = firstMove ? rotationReference.eulerAngles : defaultRotation;
            endRotation.x += rotationAdjustment;

            do
            {
                timer += Time.deltaTime / movementSettings.TranslationTime;
                knifeTransform.eulerAngles = Vector3.Lerp(startRotation, endRotation, movementSettings.TranslationCurve.Evaluate(timer));
                yield return wait;
            }
            while (timer < 1f);

            if (firstMove)
            {
                defaultRotation = knifeTransform.eulerAngles;
            }
        }

        IEnumerator MoveCoroutine(bool firstMove = false)
        {
            if (!firstMove)
            {
                StartCoroutine(RotateCoroutine(normalRotationAdjustment));
            }
            else
            {
                StartCoroutine(RotateCoroutine(firstRotationAdjustment, true));
            }

            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            float timer = 0f;
            Vector3 startPosition = knifeTransform.position;
            Vector3 endPosition = startPosition;
            endPosition.z += moveDistance;
            endPosition.y += moveDistance;

            do
            {
                timer += Time.deltaTime / movementSettings.TranslationTime;
                knifeTransform.position = Vector3.Lerp(startPosition, endPosition, movementSettings.TranslationCurve.Evaluate(timer));
                yield return wait;
            }
            while (timer < 1f);

            moveCoroutine = null;
        }
    }
}
