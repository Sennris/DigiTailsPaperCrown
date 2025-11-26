using UnityEngine;

public class StopParticles : MonoBehaviour
{
    private ParticleSystem ps;

    public void DisableParticles(GameObject particleEmitter)
    {
        ps = particleEmitter.GetComponent<ParticleSystem>();
        ps.Stop();
    }
}
