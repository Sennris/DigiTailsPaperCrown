using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class ToySpawn : MonoBehaviour
{   
    public List<GameObject> ToyList;
    public float Momentum;
    private Rigidbody2D body;
    private GameObject ToyClone;
    private float shakeTimes = 3;
    private float shakeLength = 0.5f;

    public Animator swordPopdownAnimator;
    public WalkToyMovement toyMovement;

    public void SpawnToys()
    {
        swordPopdownAnimator.SetTrigger("Popdown");
        foreach (var toy in ToyList)
        {
            SpawnSingleToy(toy, transform.position);
        }
    }

     // Spawn a single toy at a given position
    public void SpawnSingleToy(GameObject toyPrefab, Vector3 position)
    {
        GameObject toyClone = Instantiate(toyPrefab, position, Quaternion.identity);
        Rigidbody2D body = toyClone.GetComponent<Rigidbody2D>();
        toyClone.SetActive(true);
        body.AddForce(new Vector2(Momentum, 0), ForceMode2D.Impulse);

        //StartCoroutine(SpawnShake());
        SoundManager.PlaySound("Toy_Bang"); // sound

        // Assign the ToySpawn manager to the toy's health script
        ToyHealth toyHealth = toyClone.GetComponent<ToyHealth>();
        if (toyHealth != null)
        {
            toyHealth.toySpawnManager = this;
        }
    }

    // Spawn a random toy from the list
    public void SpawnRandomToy(Vector3 position)
    {
        if (ToyList.Count == 0) return;

        int randomIndex = Random.Range(0, ToyList.Count);
        GameObject randomToy = ToyList[randomIndex];
        SpawnSingleToy(randomToy, position);
    }
    
    public IEnumerator SpawnShake()
    {
        for (int i = 0; i < shakeTimes; i++)
        {
            SoundManager.PlaySound("Toy_Bang");

            yield return new WaitForSeconds(shakeLength);
        }
    }
    
}