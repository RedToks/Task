using UnityEngine;

public class CarZone : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    private int currentSpawnIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerInteraction.SetCurrentCarZone(this);
                Debug.Log("Игрок вошел в зону пикапа.");
                playerInteraction.PlaceInCar();
            }
            else
            {
                Debug.LogWarning("Инвентарь игрока не найден.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            playerInteraction.ClearCurrentCarZone();
            Debug.Log("Игрок покинул зону пикапа.");
        }
    }

    public Transform GetNextSpawnPoint()
    {
        Transform spawnPoint = spawnPoints[currentSpawnIndex];
        currentSpawnIndex = (currentSpawnIndex + 1) % spawnPoints.Length;
        return spawnPoint;
    }
}
