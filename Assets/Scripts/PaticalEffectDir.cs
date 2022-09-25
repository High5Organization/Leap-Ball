using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticalEffectDir : MonoBehaviour
{
    ParticleSystem particle;
    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        var main = particle.main;
        if (Input.GetAxis("Mouse X") > 0)
        {
            main.startSpeed = 5;
        }
        if (Input.GetAxis("Mouse X") < 0)
        {
            main.startSpeed = -5;
        }
    }
}
