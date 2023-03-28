using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCompletion : MonoBehaviour
{
    public static event Action LevelEnded;
    [SerializeField] private float rotSpeed;
    
    private void Update()
    {
        transform.Rotate(Vector3.one * (rotSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Level Ended");
            LevelEnded?.Invoke();
        }
    }
}
