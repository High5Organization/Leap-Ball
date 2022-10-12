using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerJumper : MonoBehaviour
{
    [Range(0, 8)]
    [SerializeField] Rigidbody _rb;
    [SerializeField] Animator _anim;
    [SerializeField] Animator _comboAnim;
    [SerializeField] Animator _combo2Anim;
    [SerializeField] ParticleSystem partical;
    [SerializeField] StairsSpawner stairsSpawner;
    [SerializeField] List<Color> _colorList;
    [SerializeField] GameObject _comboText;
    [SerializeField] GameObject _comboHitCount;
    [SerializeField] GameObject _comboHit2Count;
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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        transform.localScale = Vector3.one * 1.2f;
        GameManager.Instance._jumpPower = GameManager.Instance.JumpPower;
    }
    private void Start()
    {
        GameManager.Instance.IsDestructable = false;

        _anim.GetComponent<Animator>();

        _bounceCountText.text = "Bounces : " + GameManager.Instance.BounceCount;
        _stairsCountText.text = "Stairs : " + GameManager.Instance.StairsCount;

        _comboHitCount.SetActive(false);
        GameManager.Instance.ComboCounter = 0;
        _comboText.SetActive(false);

        for (int i = 0; i < ComboWordsList.Count; i++)
        {
            ComboWordsList[i].SetActive(false);
        }
    }
    private void Update()
    {
        if (transform.position.y < -5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stairs") && GameManager.Instance.GameState == GameStates.InGameStart)
        {
            GameManager.Instance.IntializeJump();

            GameManager.Instance.BounceCount++; //Bounce Count Increase
            _bounceCountText.text = "Bounces : " + GameManager.Instance.BounceCount;

            //Get Collided Stairs Index From StairList
            GameManager.Instance.StairsCount = FindObjectOfType<StairsSpawner>().StairsList.IndexOf(other.gameObject) + 1;

            _stairsCountText.text = "Stairs : " + GameManager.Instance.StairsCount;

            if (other.gameObject == stairsSpawner.StairsList[stairsSpawner.StairsList.Count - 1].gameObject) // Find the Last member of StairList
            {
                for (int i = 0; i < ComboWordsList.Count; i++)
                {
                    ComboWordsList[i].SetActive(false);
                }
                GameManager.Instance.IntializeGameWin();
                Handheld.Vibrate();
            }
            // If the Player Collides with the  Stairs of the Correct Color
            if (transform.GetComponent<MeshRenderer>().material.color == other.transform.GetComponent<MeshRenderer>().material.color)
            {
                Handheld.Vibrate();
                GameManager.Instance.ComboCounter++;
                _comboText.GetComponent<TextMesh>().text = "+" + "" + GameManager.Instance.ComboCounter;

                GameManager.Instance._jumpPower += GameManager.Instance.JumpPowerIncrease;

                if (GameManager.Instance.ComboCounter > 5)
                {
                    StartCoroutine(GetDestruct());
                }
                if (GameManager.Instance.ComboCounter == 6)
                {
                    GameManager.Instance.IntializeThirtBoost();
                }
                else if (GameManager.Instance.ComboCounter == 4)
                {
                    GameManager.Instance.IntializeSecondBoost();
                }
                StartCoroutine(SetComboText());
                if (GameManager.Instance.ComboCounter % 2 == 0 && GameManager.Instance.ComboCounter > 0)
                {
                    StartCoroutine(ShowComboHitText());
                    StartCoroutine(GetComboWords());
                }
            }
            else if (transform.GetComponent<MeshRenderer>().material.color != other.transform.GetComponent<MeshRenderer>().material.color)
            {
                GameManager.Instance._jumpPower = GameManager.Instance.JumpPower;
                GameManager.Instance.ComboCounter = 0;
                HideComboHitText();
            }
            _rb.velocity = Vector3.up * GameManager.Instance._jumpPower; //Jump
            if (GameManager.Instance.JumpSound.GetComponent<AudioSource>().pitch > 1.5f)
            {
                GameManager.Instance.JumpSound.GetComponent<AudioSource>().pitch = 1.5f;
            }
            else
            {
                GameManager.Instance.JumpSound.GetComponent<AudioSource>().pitch += 0.01f * GameManager.Instance.ComboCounter;
            }

        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
            //Get Next 4 Stairs Color Randomly For Player
            int index = stairsSpawner.StairsList.IndexOf(other.gameObject) + 1;
            int index2 = index + 4;
            print(index + "ve" + index2);
            int rndm = Random.Range(index, index2);
            int abs = Mathf.Abs(rndm);
            if (stairsSpawner.StairsList[abs] != null)
            {
                transform.GetComponent<MeshRenderer>().material.color = stairsSpawner.StairsList[abs].GetComponent<MeshRenderer>().material.color;
            }
            else if (other.transform == stairsSpawner.StairsList[stairsSpawner.StairsList.Count])
            {
                transform.transform.GetComponent<MeshRenderer>().material.color = other.transform.GetComponent<MeshRenderer>().material.color;
            }
            else
            {
                transform.transform.GetComponent<MeshRenderer>().material.color = other.transform.GetComponent<MeshRenderer>().material.color;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destruction"))
        {
            if (GameManager.Instance.IsDestructable)
            {
                Destroy(other.GetComponentInParent<Destruction>().gameObject);
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
        if (GameManager.Instance.ComboCounter > 1)
        {
            _comboHitCount.SetActive(true);
            _comboHitText.text = "+" + "" + GameManager.Instance.ComboCounter;
            _comboAnim.SetTrigger("Combo");
        }
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator GetComboWords()
    {
        int rndm = Random.Range(0, 2);
        ComboWordsList[rndm].SetActive(true);
        yield return new WaitForSeconds(3f);
        ComboWordsList[rndm].SetActive(false);
    }
    IEnumerator GetDestruct()
    {
        GameManager.Instance.IsDestructable = true;
        yield return new WaitUntil(() => GameManager.Instance.ComboCounter < 5);
        GameManager.Instance.IsDestructable = false;
    }
    void HideComboHitText()
    {
        _comboHitCount.SetActive(false);
        _comboHit2Count.SetActive(false);
    }
}
