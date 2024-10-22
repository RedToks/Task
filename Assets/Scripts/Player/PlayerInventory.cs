using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private GameObject currentItem;

    public GameObject CurrentItem
    {
        get { return currentItem; }
    }

    public void StoreItem(GameObject item)
    {
        currentItem = item;
    }

    public void RemoveItem()
    {
        currentItem = null;
    }
}
