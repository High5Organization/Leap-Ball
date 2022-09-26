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
    [SerializeField] ParticleSystem partical;
    [SerializeField] StairsSpawner stairsSpawner;
    [SerializeField] List<Color> _colorList;
    [SerializeField] GameObject _comboText;
    [SerializeField] GameObject _comboParent;
    [SerializeField] TextMeshProUGUI _bounceCountText;
    [SerializeField] TextMeshProUGUI _stairsCountText;

    public bool Isjumpable;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _anim.GetComponent<Animator>();
        JumpPower = _jumpPower;

        _bounceCountText.text = "Bounces : " + GameManager.Instance.BounceCount;
        _stairsCountText.text = "Stairs : " + GameManager.Instance.StairsCount;

        comboCounter = 0;
        _comboText.SetActive(false);
    }
    private void Update()
    {
        if (GameManager.Instance.GameState == GameStates.InGameStart)
        {

            if (Isjumpable == true)
            {
                _rb.velocity = Vector3.up * JumpPower;

                GameManager.Instance.BounceCount++;
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
            _anim.SetBool("Shape", true);

            //Get Collided Stairs Index From StairList
            GameManager.Instance.StairsCount = FindObjectOfType<StairsSpawner>().StairsList.IndexOf(other.gameObject) + 1;
            _stairsCountText.text = "Stairs : " + GameManager.Instance.StairsCount;

            partical.transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
            partical.transform.SetParent(other.gameObject.transform);
            partical.Play();

            if (other.gameObject == stairsSpawner.StairsList[stairsSpawner.StairsList.Count - 1].gameObject)
            {
                GameManager.Instance.IntializeGameWin();
            }


            if (transform.GetComponent<MeshRenderer>().material.color == other.transform.GetComponent<MeshRenderer>().material.color)
            {
                comboCounter++;
                // if (comboCounter > 8)
                // {
                //     comboCounter = 8;
                // }
                if (comboCounter % 2 == 0 & comboCounter > 0)
                {
                    JumpPower += _jumpPowerIncrease;
                    StartCoroutine(SetComboText());
                    // if (JumpPower > 30)
                    // {
                    //     JumpPower = 30;
                    // }
                }
            }
            else
            {
                JumpPower = _jumpPower;
                comboCounter = 0;
            }
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
            _anim.SetBool("Shape", false);

            //Get Next 4 Stairs Color Randomly For Player
            int index = FindObjectOfType<StairsSpawner>().StairsList.IndexOf(other.gameObject);
            int index2 = FindObjectOfType<StairsSpawner>().StairsList.IndexOf(other.gameObject) + 4;
            int rndm = Random.Range(index, index2);
            transform.GetComponent<MeshRenderer>().material.color = stairsSpawner.StairsList[rndm].GetComponent<MeshRenderer>().material.color;

            print(JumpPower);
        }
    }
    IEnumerator SetComboText()
    {
        _comboText.SetActive(true);
        _comboParent.transform.position = new Vector3(_comboParent.transform.position.x, transform.position.y + 2, _comboParent.transform.position.z);
        yield return new WaitForSeconds(1);
        _comboText.SetActive(false);
    }
}
