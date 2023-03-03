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
    }

    public void SetPlayer(Player temp)
    {
        player = temp;
    }
}
