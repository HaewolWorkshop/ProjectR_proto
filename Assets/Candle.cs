using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] private float moveRange = 1;
    [SerializeField] private float moveSpeed = 1;
    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;

        moveRange = Random.Range(0.05f, 0.3f);
        moveSpeed = Random.Range(0.7f, 1);
    }

    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * moveSpeed) * moveRange;
    }
}
