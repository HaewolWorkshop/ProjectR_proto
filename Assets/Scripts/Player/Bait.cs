using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;

    private bool isUsed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(isUsed)
        {
            return;
        }

        var enemys= Physics.OverlapSphere(transform.position, range, enemyMask);
        var pos = transform.position;

        foreach(var enemy in enemys)
        {
            enemy.GetComponent<Zombie>()?.Attention(pos, 1);
        }
        isUsed = true;
    }
}
