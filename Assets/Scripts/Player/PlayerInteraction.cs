using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform handPosition;
    private PlayerInventory inventory;
    private CarZone currentCarZone;

    private void Start()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                IPickable pickable = hit.collider.GetComponent<IPickable>();
                if (pickable != null)
                {
                    PickableCarItem pickupItem = hit.collider.GetComponent<PickableCarItem>();
                    if (pickupItem != null)
                    {
                        if (inventory.CurrentItem == null)
                        {
                            PickUpItem(pickupItem);
                        }
                        else
                        {
                            Debug.LogWarning("�� �� ������ ����� ���� �������, ��� ��� ��� ������� ������.");
                        }
                    }
                }
            }
        }
    }

    private void PickUpItem(PickableCarItem pickableItem)
    {
        if (!pickableItem.IsInCar)
        {
            GameObject newItem = Instantiate(pickableItem.ItemPrefab, handPosition.position, handPosition.rotation, handPosition);
            inventory.StoreItem(newItem);
            pickableItem.OnPickUp();
            Debug.Log($"�� ������� {pickableItem.ItemPrefab.name}.");
        }
        else
        {
            Debug.LogWarning($"�� �� ������ ������� {pickableItem.ItemPrefab.name}, ��� ��� �� ��� � ������.");
        }
    }


    public void PlaceInCar()
    {
        if (currentCarZone != null && inventory.CurrentItem != null)
        {
            Transform spawnPoint = currentCarZone.GetNextSpawnPoint();
            if (spawnPoint != null)
            {

                Instantiate(inventory.CurrentItem, spawnPoint.position, spawnPoint.rotation);
                Debug.Log($"{inventory.CurrentItem.name} ������� � ������.");
                Destroy(inventory.CurrentItem);
                inventory.RemoveItem();
            }
            else
            {
                Debug.LogWarning("����� ������ �� ����������� � ���� ������.");
            }
        }
    }

    public void SetCurrentCarZone(CarZone carZone)
    {
        currentCarZone = carZone;
    }

    public void ClearCurrentCarZone()
    {
        currentCarZone = null;
    }
}
