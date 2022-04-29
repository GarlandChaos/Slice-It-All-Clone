using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SliceableModel
{
    sphere,
    card,
    capsicum
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Sliceable : MonoBehaviour
{
    Rigidbody rb = null;
    bool sliced = false;
    [SerializeField]
    SliceableModel model = SliceableModel.sphere;
    public SliceableModel _Model { get => model; }
    Renderer rend;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Knife" && !sliced)
        {
            BeSliced(0.0f, 1);
            Sliceable sliceable = ObjectPoolManager.instance.GetObject(model).GetComponent<Sliceable>();
            if (sliceable)
            {
                sliceable.transform.position = transform.position;
                sliceable.transform.rotation = transform.rotation;
                sliceable.gameObject.SetActive(true);
                sliceable.BeSliced(0.0f, 0);
            }
        }
    }

    public void BeSliced(float edgeSliceValue, int sliceSideValue)
    {
        sliced = true;
        rend.material.SetFloat("_Edge", edgeSliceValue);
        rend.material.SetInt("_LeftSlice", sliceSideValue);
        rb.isKinematic = false;
        if(sliceSideValue == 1)
        {
            rb.AddForce(-transform.right * 500f);
        }
        else
        {
            rb.AddForce(transform.right * 500f);
        }
        GameManager.instance._Money += 1;
        StartCoroutine(DeactivateCoroutine());
    }

    IEnumerator DeactivateCoroutine()
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        float timer = 0f;
        float timeUntilDeactivate = 2f;

        do
        {
            timer += Time.deltaTime / timeUntilDeactivate;

            yield return wait;
        }
        while (timer < 1f);

        gameObject.SetActive(false);
    }
}
