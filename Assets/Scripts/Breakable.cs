using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody[] frags;
    [SerializeField] private float explosion;
    [SerializeField] private float range;

    public void Break(Vector3 hitPoint)
    {
        foreach(var frag in frags)
        {
            frag.gameObject.SetActive(true);
            frag.AddExplosionForce(explosion, hitPoint, range);
        }

        target.SetActive(false);

        GetComponent<Collider>().enabled = false;
    }

}
