using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsSpawner : MonoBehaviour
{
    #region Stairs
    [SerializeField] float _stairsSpawnAngle;
    Queue<GameObject> _stairsPoolObjects;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _playerTrail;
    [SerializeField] float _stairsHigh;
    [SerializeField] float _stairsDistanceZ;
    [SerializeField] float _stairsSpawnRate;
    [SerializeField] GameObject _stairsPrefab;
    [SerializeField] List<Color> _colorList;
    [SerializeField] PlayerJumper playerJumper;
    public List<GameObject> StairsList;
    #endregion

    private void Awake()
    {
        _stairsPoolObjects = new Queue<GameObject>(); // Creating a Queue for Object Pooling
        StairsList = new List<GameObject>(); // Adding Queue To List
        for (int i = 0; i < GameManager.Instance.PoolSize; i++)
        {
            GameObject stairs = Instantiate(_stairsPrefab);
            stairs.GetComponent<MeshRenderer>().enabled = false;
            stairs.SetActive(false);
            _stairsPoolObjects.Enqueue(stairs);
            StairsList.Add(stairs);
        }
    }
    private void Start()
    {
        _playerTrail.SetActive(false);
        _player.transform.position = new Vector3(StairsList[0].transform.position.x, StairsList[0].GetComponent<MeshRenderer>().bounds.size.y + .5f, _stairsDistanceZ - 5);
        StartCoroutine(SpawnStairs());

        for (int i = 0; i < GameManager.Instance.PoolSize; i++)
        {
            var Stairs = GetPooledObject();
            Stairs.transform.SetParent(transform);
            Stairs.transform.position += new Vector3(0, i * _stairsHigh, _stairsDistanceZ);
            Stairs.transform.RotateAround(transform.position, Vector3.up, -_stairsSpawnAngle * i);
            int rndm = Random.Range(0, _colorList.Count);
            Stairs.GetComponent<MeshRenderer>().material.color = _colorList[rndm];
        }
        _player.transform.GetComponent<MeshRenderer>().material.color = StairsList[0].GetComponent<MeshRenderer>().material.color;
        _player.GetComponent<Rigidbody>().isKinematic = true;
        _playerTrail.transform.position = _player.transform.position;

    }
    private void Update()
    {
        if (playerJumper.comboCounter > 3 && GameManager.Instance.GameState == GameStates.InGameStart)
        {
            _playerTrail.SetActive(true);
            float yAxis = Mathf.Lerp(_playerTrail.transform.position.y, _player.transform.position.y, Time.deltaTime * 10);
            _playerTrail.transform.position = new Vector3(transform.position.x, yAxis, _player.transform.position.z);
        }
        else
        {
            _playerTrail.SetActive(false);
        }
    }
    public GameObject GetPooledObject()
    {
        GameObject stair = _stairsPoolObjects.Dequeue();
        stair.SetActive(true);
        _stairsPoolObjects.Enqueue(stair);
        return stair;
    }
    IEnumerator SpawnStairs()
    {
        for (int i = 0; i < StairsList.Count; i++)
        {
            StairsList[i].GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(_stairsSpawnRate);
        }
    }
}
