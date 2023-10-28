using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region FIELDS

    public static InventoryManager Instance { get; private set; } // Singleton
    
    private Item[] _items;
    // UI
    private Transform[] _itemSlots;
    [SerializeField] private Transform _itemSlotsContainer;
    [SerializeField] private Text _craftOutputText;
    
    [SerializeField] private Item.ItemNameSpriteMaps[] itemNameSpriteMaps;

    #endregion


    #region METHODs

    public Sprite GetSprite(Item.ItemName name) 
    {
        Sprite targetSprite = null;
        targetSprite = Array.Find(itemNameSpriteMaps, maps => maps.name == name).sprite;
        return targetSprite;
    }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        _itemSlots = new Transform[_itemSlotsContainer.childCount];
        for (int i = 0; i < _itemSlotsContainer.childCount; i++)
        {
            _itemSlots[i] = _itemSlotsContainer.GetChild(i);
        }
        _items = new Item[_itemSlots.Length];
    }

    public void AddItem(Item newItem)
    {
        // Check if this item is already existing in the inventory and its size < 40
        try
        {
            // Item
            Item item = Array.Find(_items, item => item.Name == newItem.Name && item.Amount + newItem.Amount <= 40);
            item.Amount += newItem.Amount;
            // Slot
            int itemIndex = Array.IndexOf(_items, item);
            UpdateUI(itemIndex);
        }
        // if the item is new to the inventory
        catch (Exception)
        {
            int i = 0;
            while (_items[i] != null && i < _items.Length && _items[i].Name != Item.ItemName.Null) i++;   // find an empty case in the inventory
            
            _items[i] = newItem;    // Item
            UpdateUI(i);            // Slot
        }
    }


    // if item index == -1: all item slots,   else: only specified slot
    private void UpdateUI(int itemIndex = -1)
    {
        // Specific item slot
        if (itemIndex >= 0)
        {
            _itemSlots[itemIndex].GetComponentsInChildren<Image>()[^1].sprite = GetSprite(_items[itemIndex].Name);
            _itemSlots[itemIndex].GetComponentInChildren<Text>().text = _items[itemIndex].Amount.ToString();
            return;    
        }
        // All item slots
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            try
            {
                _itemSlots[i].GetComponentsInChildren<Image>()[^1].sprite = GetSprite(_items[i].Name);;
                _itemSlots[i].GetComponentInChildren<Text>().text = _items[i].Amount.ToString();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    
    public void TryToCraftItem(Item craftedItem, Item[] itemsRecipe, int quantity)
    {
        var newItems = ObjectCopier.Clone(_items);
        foreach (var requiredItem in itemsRecipe)
        {
            var item = Array.Find(newItems, item => item.Name == requiredItem.Name);
            
            // Fail
            if (item == null || item.Amount < requiredItem.Amount * quantity)
            {
                _craftOutputText.text = $"NOT ENOUGH {item.Name}, quantity:{item.Amount}, (required:{requiredItem.Amount * quantity})";
                return;
            }
            // Partial Success
            item.Amount -= requiredItem.Amount * quantity;
            
            UpdateUI(Array.IndexOf(newItems, item));
        }

         // Full Success: Craft
        _items = newItems;
        AddItem(craftedItem);
        _craftOutputText.text = $"Crafted: {craftedItem.Name} x {quantity}";
        
        UpdateUI();
    }
    
    
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
