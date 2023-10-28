using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "NewCraftRecipe", menuName = "Craft Recipe")]
public class CraftButton : MonoBehaviour
{
    [field:SerializeField] public Item craftedItem { get; set; }
    [field:SerializeField] public Item[] ItemsRecipe { get; set; }
    [SerializeField] private int _quantity;
    
    private Button _craftButton;


    private void Start()
    {
        _craftButton = GetComponent<Button>();

        craftedItem.Amount *= _quantity;
        _craftButton.onClick.AddListener(() => InventoryManager.Instance.TryToCraftItem(craftedItem, ItemsRecipe, _quantity));
    }
}
