using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    [SerializeField] GameObject BreakedStair;
    [SerializeField] Material BreakedStairMaterial;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Shape", false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (GameManager.Instance.GameState == GameStates.InGameStart && other.transform.CompareTag("Player"))
            anim.SetBool("Shape", true);
    }
    private void OnCollisionExit(Collision other)
    {
        if (GameManager.Instance.GameState == GameStates.InGameWin || other.transform.CompareTag("Player"))
            anim.SetBool("Shape", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        // GameObject Stairs;
        if (other.transform.CompareTag("Player"))
        {
            if (GameManager.Instance.IsDestructable)
            {
                    GameManager.Instance.IntializeDestruct();

                var Stairs = Instantiate(BreakedStair, transform.position, transform.rotation);
                var rbs = Stairs.GetComponentsInChildren<Rigidbody>();

                foreach (var rb in rbs)
                {
                    rb.AddExplosionForce(50, Stairs.transform.position, 2);
                }

                BreakedStairMaterial.color = transform.GetComponent<MeshRenderer>().material.color;
                BreakedStair.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 5, ForceMode.Impulse);

                Destroy(Stairs, 3f);
            }
        }
    }
}
