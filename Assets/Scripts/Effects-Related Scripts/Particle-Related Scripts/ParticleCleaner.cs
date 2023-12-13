using System.Collections;
using UnityEngine;

public class ParticleCleaner : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DeleteParticle());
    }

    IEnumerator DeleteParticle()
    {
        yield return new WaitUntil(() => !this.gameObject.GetComponent<ParticleSystem>().IsAlive());
        Destroy(this.gameObject);
    }
}
