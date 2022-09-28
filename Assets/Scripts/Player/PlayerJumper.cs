using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayerJumper : MonoBehaviour
{
    float JumpPower;
    [Range(0, 8)]
    public int comboCounter;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _jumpPower;
    [SerializeField] int _jumpPowerIncrease;
    [SerializeField] Animator _anim;
    [SerializeField] Animator _comboAnim;
    [SerializeField] ParticleSystem partical;
    [SerializeField] StairsSpawner stairsSpawner;
    [SerializeField] List<Color> _colorList;
    [SerializeField] GameObject _comboText;
    [SerializeField] GameObject _comboHitCount;
    [SerializeField] GameObject _comboParent;
    #region TMPRO
    [Header("TMPRO")]
    [SerializeField] TextMeshProUGUI _bounceCountText;
    [SerializeField] TextMeshProUGUI _stairsCountText;
    [SerializeField] TextMeshProUGUI _comboHitText;
    [SerializeField] GameObject GreatText;
    [SerializeField] GameObject AwesomeText;
    [SerializeField] GameObject PefectText;
    [SerializeField] List<GameObject> ComboWordsList;
    #endregion

    public bool Isjumpable;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        transform.localScale = Vector3.one * 1.2f;
    }
    private void Start()
    {
        _anim.GetComponent<Animator>();
        JumpPower = _jumpPower;

        _bounceCountText.text = "Bounces : " + GameManager.Instance.BounceCount;
        _stairsCountText.text = "Stairs : " + GameManager.Instance.StairsCount;
        _comboHitCount.SetActive(false);
        comboCounter = 0;
        _comboText.SetActive(false);

        for (int i = 0; i < ComboWordsList.Count; i++)
        {
            ComboWordsList[i].SetActive(false);
        }
    }
    private void Update()
    {
        //Bounce Count Text Operations
        if (GameManager.Instance.GameState == GameStates.InGameStart)
        {

            if (Isjumpable == true)
            {
                _rb.velocity = Vector3.up * JumpPower; //Jump

                GameManager.Instance.BounceCount++; //Bounce Count Increase
                
                BounceTextAnim();

                _bounceCountText.text = "Bounces : " + GameManager.Instance.BounceCount;
                Isjumpable = false;
            }

            _comboText.GetComponent<TextMesh>().text = "+" + "" + comboCounter;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
            Isjumpable = true;

            //Get Collided Stairs Index From StairList
            GameManager.Instance.StairsCount = FindObjectOfType<StairsSpawner>().StairsList.IndexOf(other.gameObject) + 1;
            
            StairTextAnim();

            _stairsCountText.text = "Stairs : " + GameManager.Instance.StairsCount;

            if (other.gameObject == stairsSpawner.StairsList[stairsSpawner.StairsList.Count - 1].gameObject) // Find the Last member of StairList
            {
                GameManager.Instance.IntializeGameWin();
                Handheld.Vibrate();
            }
            // If the Player Collides with the  Stairs of the Correct Color
            if (transform.GetComponent<MeshRenderer>().material.color == other.transform.GetComponent<MeshRenderer>().material.color)
            {
                Handheld.Vibrate();
                comboCounter++;


                if (comboCounter > 5)
                {
                    StartCoroutine(Destruct());
                }

                if (comboCounter % 2 == 0 & comboCounter > 0)
                {
                    StartCoroutine(ShowComboHitText());
                    StartCoroutine(GetComboWords());
                }
                JumpPower += _jumpPowerIncrease;
                StartCoroutine(SetComboText());
            }
            else
            {
                JumpPower = _jumpPower;
                comboCounter = 0;
                HideComboHitText();
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
            //Get Next 4 Stairs Color Randomly For Player
            int index = FindObjectOfType<StairsSpawner>().StairsList.IndexOf(other.gameObject) + 1;
            int index2 = FindObjectOfType<StairsSpawner>().StairsList.IndexOf(other.gameObject) + 4;
            int rndm = Mathf.Abs(Random.Range(index, index2));
            if (rndm == 0)
                return;
            else
                transform.GetComponent<MeshRenderer>().material.color = stairsSpawner.StairsList[rndm].GetComponent<MeshRenderer>().material.color;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("GoUp"))
        // {
        //     _rb.velocity = Vector3.up * JumpPower;
        // }
        if (other.CompareTag("Destruction"))
        {
            if (GameManager.Instance.IsDestructable)
            {
                Destroy(other.GetComponentInParent<OneWayBoxCollider>().gameObject);
            }
        }
    }
    IEnumerator SetComboText()
    {
        _comboText.SetActive(true);
        _comboParent.transform.position = new Vector3(_comboParent.transform.position.x, transform.position.y + 2, _comboParent.transform.position.z);
        yield return new WaitForSeconds(1);
        _comboText.SetActive(false);
    }
    IEnumerator ShowComboHitText()
    {
        if (comboCounter > 1)
        {
            _comboHitCount.SetActive(true);
            _comboHitText.text = "+" + "" + comboCounter;
            _comboAnim.SetTrigger("Combo");
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator GetComboWords()
    {
        int rndm = Random.Range(0, 2);
        ComboWordsList[rndm].SetActive(true);
        yield return new WaitForSeconds(1f);
        ComboWordsList[rndm].SetActive(false);
    }
    IEnumerator Destruct()
    {
        print("bi daha");
        GameManager.Instance.IsDestructable = true;

        yield return new WaitForSeconds(2f);

        GameManager.Instance.IsDestructable = false;
    }
    void HideComboHitText()
    {
        _comboHitCount.SetActive(false);
    }
    void BounceTextAnim()
    {
        GameManager.Instance.BounceText.transform.DOScale(Vector3.one * 0.7f, 0.2f).OnComplete(() => //Bounce Text Anim
          {
              GameManager.Instance.BounceText.transform.DOScale(Vector3.one * 0.55f, 0.2f);
          });
    }
    void StairTextAnim()
    {
        GameManager.Instance.StairText.transform.DOScale(Vector3.one * 0.7f, 0.2f).OnComplete(() => // Stair Text Anim
           {
               GameManager.Instance.StairText.transform.DOScale(Vector3.one * 0.55f, 0.2f);
           });
    }
}
