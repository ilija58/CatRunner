using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    GameBehaviour gameBehaviour;

    private void Start()
    {
        gameBehaviour = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "cat")
        {
            gameBehaviour.Lives--;
        }
    }
}
