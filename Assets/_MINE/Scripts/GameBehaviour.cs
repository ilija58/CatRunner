using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
enum showScreen { Lose = -1 , Win = 1, None = 0}
public class GameBehaviour : MonoBehaviour
{
    showScreen screen = showScreen.None;
    float spawnX, spawnY, spawnZ;
    const int spawnItemsCount = 10;
    public GameObject coinClone;
    public GameObject obstacleClone;
    public int maxItems = 10;
    private GameObject startButton;
    [SerializeField] private Transform roadTransform;
    public float distanceBetween = 50f;
    private string labelText = "Go through the obstacles and collect all" + spawnItemsCount + "the items!";
    private Transform playerTransform;

    private int _itemsCollected = 0;
    private int _lives = 1;
    private bool _isPlay = false;

    public bool isPlayed 
    {
        get { return _isPlay; }
        set { _isPlay = value;

            if (_isPlay)
            {
                ChangeGameTimeState(_isPlay);
            }
            else
            {
                ChangeGameTimeState(_isPlay);
            }
        }
    }
    public int Items
    {
        get { return _itemsCollected; }
        set { _itemsCollected = value;
            if(_itemsCollected >= maxItems)
            {
                labelText = "Congratulations! You collect all items!";
                screen = showScreen.Win;
                Time.timeScale = 0f;
            }
        }
    }

    public int Lives
    {
        get { return _lives; }
        set 
        { 
            _lives = value;
            Debug.Log(_lives);
            if(_lives <= 0)
            {
                Time.timeScale = 0f;
               
                screen = showScreen.Lose;
            }
        }
    }
    

    private void OnGUI()
    {
        
        switch (screen)
        {
            case showScreen.Win:
                if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 400, 500), "You collect items!!!"))
                    {
                        Time.timeScale = 1.0f;
                        SceneManager.LoadScene(0);
                    }
                break;
            case showScreen.Lose:
                if(GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 400, 500), "You lose!!!"))
                {
                    Time.timeScale = 1.0f;
                    SceneManager.LoadScene(0);
                }
                break;
        } 
            
    }

    private void Start()
    {
        
        isPlayed = false;
        roadTransform = GameObject.Find("Road").GetComponent<Transform>();
        playerTransform = GameObject.Find("cat").GetComponent<Transform>();
        GenerateSpawnPool(distanceBetween, coinClone);
        GenerateSpawnPool(distanceBetween + 20f, obstacleClone);
    }

    List<Vector3> GenerateSpawnPool(float distance,GameObject _gameObject)
    {
        List<Vector3> pos = new List<Vector3>(spawnItemsCount);
        for (int i = 0; i < spawnItemsCount; i++)
        {
            spawnX = Mathf.RoundToInt(Random.Range(-1, 2)) * PlayerBehaviour.lineSize;
            spawnY = roadTransform.position.y + 1f;
            if(i == 0)
            {
                spawnZ = playerTransform.transform.position.z + distance;
            }
            else
            {
                spawnZ = playerTransform.transform.position.z + distance * i;
            }
            Vector3 position = new Vector3(spawnX, spawnY, spawnZ);
            pos.Insert(i, position);
        }
        //foreach (Vector3 item in pos)
        //{
        //    Debug.Log(item);
        //}
        SpawnItem(pos, _gameObject);
        return pos;
    }

    public static bool ChangeGameTimeState(bool state)
    {
        if (state)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        return state;
    }


    void SpawnItem(List<Vector3> posList, GameObject _gameObject)
    {
        foreach(Vector3 item in posList)
        {
            Instantiate(_gameObject, item, _gameObject.GetComponent<Transform>().rotation);
        }
          
    }

}
