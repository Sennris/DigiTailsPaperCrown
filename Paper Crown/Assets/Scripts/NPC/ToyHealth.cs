using UnityEngine;

public class ToyHealth : MonoBehaviour
{
    public int health;
    public ToySpawn toySpawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 5;
    }

    public void Damage()
    {
        health -= 1;
        if(health <= 0)
        {
            Destroy(gameObject);

            // Respawn a random toy
            toySpawnManager.SpawnRandomToy(toySpawnManager.transform.position);
        
        }
    }
}
