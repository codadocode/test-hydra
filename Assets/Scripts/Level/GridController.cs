using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class GridController : MonoBehaviour
{
    public static GridController gridController;
    [Header("Block Prefab")]
    [SerializeField]
    private GameObject _blockPrefab;
    [Header("Grid Config")]
    [SerializeField]
    private Transform _mapTransform;
    [SerializeField]
    private int _wSize = 0;
    [SerializeField]
    private int _hSize = 0;
    private GameObject[,] _groundArray;
    private Dictionary<Vector3, GameObject> _obstacles;
    [Header("Snake Config")]
    [SerializeField]
    private int _xSpawn = 0;
    [SerializeField]
    private int _ySpawn = 0;
    private int _hSpawn = 1;
    [SerializeField]
    private GameObject _snakePrefab;
    [SerializeField]
    [Header("Food Config")]
    private GameObject _foodPrefab;
    private SnakeController _snake;
    [SerializeField]
    [Header("UI Config")]
    private GameObject _losePanel;
    [Header("Obstacles Config")] 
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private int _obstaclesQuant;
    

    private void Awake()
    {
        gridController = this;
        this._groundArray = new GameObject[this._wSize, this._hSize];
        this._obstacles = new Dictionary<Vector3, GameObject>();
        GenerateGrid();
        GenerateObstacles();
        SpawnSnake();
        SpawnFood();
    }

    private void GenerateObstacles()
    {
        for (int i = 0; i < this._obstaclesQuant; i++)
        {
            int posX = Random.Range(1, (GetWSize() - 1));
            int posY = Random.Range(0, (GetHSize() - 1));
            GameObject obstacleWall = Instantiate(this._obstaclePrefab, new Vector3(posX, 1, posY), new Quaternion(0, 0, 0, 0));
            while (this._obstacles.ContainsKey(obstacleWall.transform.position))
            {
                posX = Random.Range(1, (GetWSize() - 1));
                posY = Random.Range(0, (GetHSize() - 1));
                obstacleWall.transform.position = new Vector3(posX, 1, posY);
            }
            this._obstacles.Add(obstacleWall.transform.position, obstacleWall);
        }
    }

    public bool HasObstacleInPosition(Vector3 positionToCheck)
    {
        return this._obstacles.ContainsKey(positionToCheck);
    }
    
    public GameObject[,] GetGroundArray()
    {
        return this._groundArray;
    }

    public int GetWSize()
    {
        return this._wSize;
    }

    public int GetHSize()
    {
        return this._hSize;
    }

    public GameObject GetBlockByIndex(int x, int y)
    {
        return this._groundArray[x, y];
    }

    private void GenerateGrid()
    {
        Vector3 startSpawnPosition = Vector3.zero;
        for (int i = 0; i < this._wSize; i++)
        {
            for (int j = 0; j < this._hSize; j++)
            {
                startSpawnPosition.x = i;
                startSpawnPosition.z = j;
                GameObject actualBlock = Instantiate(this._blockPrefab, startSpawnPosition, new Quaternion(0,0,0,0));
                actualBlock.transform.SetParent(this._mapTransform);
                AddBlockInsideArray(actualBlock, i, j);
            }
        }
    }

    private void AddBlockInsideArray(GameObject block, int xPos, int yPos)
    {
        this._groundArray[xPos, yPos] = block;
    }

    private void SpawnSnake()
    {
        Vector3 spawnPosition = new Vector3(this._xSpawn, this._hSpawn, this._ySpawn);
        this._snake = Instantiate(this._snakePrefab, spawnPosition, new Quaternion(0,0,0,0)).GetComponent<SnakeController>();
    }

    private void SpawnFood()
    {
        int Xpos = Random.Range(0, GetWSize());
        int Ypos = Random.Range(0, GetHSize());
        GameObject food = Instantiate(this._foodPrefab, new Vector3(Xpos, 1, Ypos), new Quaternion(0,0,0,0));
        while (HasObstacleInPosition(food.transform.position))
        {
            Xpos = Random.Range(0, GetWSize());
            Ypos = Random.Range(0, GetHSize());
            food.transform.position = new Vector3(Xpos, 1, Ypos);
        }
    }
    
    public SnakeController GetSnakeController()
    {
        return this._snake;
    }

    public void SetLoseUIActive()
    {
        this._losePanel.SetActive(true);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
