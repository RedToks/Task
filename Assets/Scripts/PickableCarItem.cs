using UnityEngine;

public class PickableCarItem : MonoBehaviour, IPickable
{
    public GameObject ItemPrefab;
    public bool IsInCar { get; private set; } = false;

    public void OnPickUp()
    {
        Destroy(gameObject);
    }
}
