using UnityEngine;

public class Spawn : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject Spawnpoint;
    public GameObject pushSquareParent;

    public LightControl playerLight;
    public Cutscene cutscene;
    public PlayerMovement playerMovement;
    public float lightResetScale = 5f;

    private bool cutsceneCalled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.health <= 0 && !cutsceneCalled)
        {
            // playerHealth.health = playerHealth.originalHealth;
            // playerHealth.healthCounterText.text = playerHealth.health.ToString();
            // pushSquareParent.transform.parent = null;
            // transform.position = Spawnpoint.transform.position;

            // // Reset light radius (scale)
            // if (playerLight != null)
            // {
            //     playerLight.ResetLight();
            // }
            cutscene.StartCutscene("GameOverScreen");
            playerMovement.DisableMovement();
            cutsceneCalled = true;
        }
    }
}
