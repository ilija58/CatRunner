using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsHandler : MonoBehaviour
{

    private void Update()
    {
        SetItemsCountText(GetItemsCount());
    }

    public int GetItemsCount()
    {
        Debug.Log(GameObject.Find("GameManager").GetComponent<GameBehaviour>().Items);
        return GameObject.Find("GameManager").GetComponent<GameBehaviour>().Items;
    }

    public void SetItemsCountText(int items)
    {
        TextMeshProUGUI itemsCountText = GameObject.Find("Items counter").GetComponent<TextMeshProUGUI>();
        Debug.Log(GameObject.Find("Items counter").GetComponent<TextMeshProUGUI>());
        itemsCountText.text = "COINS: " + items.ToString();
    }
}
