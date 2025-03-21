using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private InventoryManager inventoryManager;

    private GameData gameData;
    private PlayerInventoryData PlayerInventoryList;

    private FileDataHandler DataHandler;

    public static DataPersistenceManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        DataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        
        LoadGame();
    }

    public void SaveGame()
    {
        InventoryManager.Instance.SaveInventory(ref PlayerInventoryList);

        DataHandler.SaveInventory(PlayerInventoryList);
    }

    public void LoadGame()
    {
        PlayerInventoryList = DataHandler.LoadInventory();
        //Debug.Log(PlayerInventoryList.items.Count);

        if (PlayerInventoryList != null)
        {
            inventoryManager.LoadInventory(PlayerInventoryList);
        }
        else
        {
            Debug.Log("No data was found.");
            PlayerInventoryList = new PlayerInventoryData();
            PlayerInventoryList.items = new List<InventoryItemsData>();
        }
    }

   
}
