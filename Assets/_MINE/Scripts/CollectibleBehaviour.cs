using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    public bool isCollected = false;
    GameBehaviour gameBehaviour;

    private void Start()
    {
        gameBehaviour = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "cat")
        {
            gameBehaviour.Items++;
            Debug.Log("Item collected");
            isCollected = true;
            Destroy(this.transform.parent.gameObject);
        }
    }

 
}
