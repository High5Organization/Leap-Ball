using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairText : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }
}