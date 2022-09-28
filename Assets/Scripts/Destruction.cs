using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    [SerializeField] GameObject BreakedStair;
    [SerializeField] Material BreakedStairMaterial;
    private void OnTriggerEnter(Collider other)
    {
        // GameObject Stairs;
        if (GameManager.Instance.IsDestructable && other.transform.CompareTag("Player"))
        {
            var Stairs = Instantiate(BreakedStair, transform.position, transform.rotation);
            var rbs = Stairs.GetComponentsInChildren<Rigidbody>();

            foreach (var rb in rbs)
            {
                rb.AddExplosionForce(50, Stairs.transform.position, 2);
            }

            BreakedStairMaterial.color = transform.GetComponent<MeshRenderer>().material.color;
            BreakedStair.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 5, ForceMode.Impulse);

            // BreakedStair.transform.GetComponent<MeshRenderer>().material.color = transform.GetComponent<MeshRenderer>().material.color;
            StartCoroutine(DestroyBreakedStairDelay());
            Destroy(gameObject);
        }
        IEnumerator DestroyBreakedStairDelay()
        {
            yield return new WaitForSeconds(1);
            // Destroy(Stairs);
        }
    }
}
