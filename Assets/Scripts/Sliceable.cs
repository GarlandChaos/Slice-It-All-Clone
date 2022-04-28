using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Sliceable : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb = null;
    bool sliced = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("My name is " + name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Knife" && !sliced)
        {
            Debug.Log(name + " collided with " + other.name);
            sliced = true;
            //slice algorithm
            rb.isKinematic = false;
            rb.AddForce(transform.right * 500f);
            GameManager.instance._Money += 1;
        }
        
    }
}
