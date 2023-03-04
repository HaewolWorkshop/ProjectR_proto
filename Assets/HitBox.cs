using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Zombie>().Damage(5f);
        }
        if (other.CompareTag("Breakable"))
        {
            other.GetComponent<Breakable>().Break(transform.position);
        }
    }

    public void SetPlayer(Player temp)
    {
        player = temp;
    }
}
