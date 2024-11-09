using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;



public class JSONLoader : MonoBehaviour
{
    public PlayerData playerData;

    // UI elements
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI inventory;
    //public GameObject inventoryItemPrefab;

    void Start()
    {
        StartCoroutine(LoadJSON());
    }

    IEnumerator LoadJSON()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://api.jsonbin.io/v3/b/6686a992e41b4d34e40d06fa");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            playerData = JsonConvert.DeserializeObject<PlayerData>(request.downloadHandler.text);
            DisplayData();
        }
        else
        {
            Debug.LogError("Failed to load JSON: " + request.error);
        }
    }

    void DisplayData()
    {

        
        playerNameText.text = "Player: " + playerData.record.playerName;
        levelText.text = "Level: " + playerData.record.level;
        healthText.text = "Health: " + playerData.record.health;
        positionText.text = $"Position:(X: {playerData.record.position.x}, Y: {playerData.record.position.y}, Z: {playerData.record.position.z})";

        inventory.text = "Inventory:";
        foreach (var item in playerData.record.inventory)
        {
            inventory.text += $"\n- {item.itemName} (Qty: {item.quantity}, Weight: {item.weight})";
        }
    }
}