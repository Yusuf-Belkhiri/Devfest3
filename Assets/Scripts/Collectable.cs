using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Item _item; 
    
    private void Start()
    {
        Collect();
    }

    private void Collect()
    {
        InventoryManager.Instance.AddItem(_item);
        Destroy(gameObject);
    }
}