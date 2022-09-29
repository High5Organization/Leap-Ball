using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StairAnim : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Stairs"))
        {
            BounceTextAnim();
            StairTextAnim();
        }
    }
    void StairTextAnim()
    {
        GameManager.Instance.StairText.transform.DOScale(Vector3.one * 0.7f, 0.2f).OnComplete(() => // Stair Text Anim
           {
               GameManager.Instance.StairText.transform.DOScale(Vector3.one * 0.55f, 0.2f);
           });
    }
    void BounceTextAnim()
    {
        GameManager.Instance.BounceText.transform.DOScale(Vector3.one * 0.7f, 0.2f).OnComplete(() => //Bounce Text Anim
          {
              GameManager.Instance.BounceText.transform.DOScale(Vector3.one * 0.55f, 0.2f);
          });
    }
}
