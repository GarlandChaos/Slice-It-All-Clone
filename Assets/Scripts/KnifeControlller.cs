using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class KnifeControlller : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb = null;
    [SerializeField]
    Transform knifeTransform = null;
    [SerializeField]
    float moveDistance = 20f;
    //[SerializeField]
    //float torque = 10f;
    Coroutine moveCoroutine = null;
    bool move = false;
    bool underGravity = true;
    float gravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //move = true;
            if (moveCoroutine == null)
            {
                moveCoroutine = StartCoroutine(MoveCoroutine());
            }
        }

        if (underGravity)
        {
            knifeTransform.position = new Vector3(knifeTransform.position.x, knifeTransform.position.y - gravity * Time.deltaTime, knifeTransform.position.z);
        }


    }

    private void FixedUpdate()
    {
        //if(move)
        //{
        //    move = false;
        //    MoveForward();
        //    MoveUp();
        //    Rotate();
        //}
        //if(rb.velocity.z < 0)
        //{
        //    Vector3 velocity = rb.velocity;
        //    velocity.z = 0f;
        //    rb.velocity = velocity;
        //}
    }

    //void MoveForward()
    //{
    //    rb.AddForce(transform.forward * force);
    //}

    //void MoveUp()
    //{
    //    rb.AddForce(transform.up * force);
    //}

    //void Rotate()
    //{
    //    rb.AddTorque(transform.right * torque, ForceMode.VelocityChange);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        
        //Debug.Log("Collided with " + collision.other.name);
        if(collision.other.tag == "Damager")
        {
            UIManager.instance.OpenLoseScreen();
        }
        else if (collision.other.tag == "Stopper")
        {
            //rb.isKinematic = true;
            //rb.useGravity = false;
            //rb.angularVelocity = Vector3.zero;
            //rb.velocity = Vector3.zero;
            if (moveCoroutine != null)
            {
                StopAllCoroutines();
                moveCoroutine = null;
            }

        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log("Colliding with " + collision.other.name);
    //}

    private void OnCollisionExit(Collision collision)
    {
        if (collision.other.tag == "Stopper")
        {
            //rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Damager")
        {
            UIManager.instance.OpenLoseScreen();
        }
        else if (other.tag == "Stopper")
        {
            //rb.isKinematic = true;
            //rb.useGravity = false;
            //rb.angularVelocity = Vector3.zero;
            //rb.velocity = Vector3.zero;
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
            //rb.isKinematic = true;
            //rb.useGravity = false;
            //rb.angularVelocity = Vector3.zero;
            //rb.velocity = Vector3.zero;
            underGravity = true;
            //if (moveCoroutine != null)
            //{
            //    StopAllCoroutines();
            //    moveCoroutine = null;
            //}

        }
    }

    IEnumerator RotateCoroutine()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        float timer = 0f;
        float animationTime = 1f;
        Vector3 startRotation = knifeTransform.eulerAngles;
        Vector3 endRotation = startRotation;
        endRotation.x += 360f;

        do
        {
            timer += Time.deltaTime / animationTime;
            knifeTransform.eulerAngles = Vector3.Lerp(startRotation, endRotation, curve.Evaluate(timer));
            yield return wait;
        }
        while (timer < 1f);
    }

    IEnumerator MoveCoroutine()
    {

        StartCoroutine(RotateCoroutine());

        //rb.isKinematic = false;

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

        //rb.isKinematic = false;
        moveCoroutine = null;
    }
}
