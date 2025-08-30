using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastFx : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefabRef; 
    [SerializeField] private List<ParticleSystem> particleSystems;
    void OnEnable()
    {
        StartCoroutine(ReturnAfterDelay(2f));
    }
    public void StopEffect()
    {
        foreach (var ps in particleSystems)
        {
            ps.Stop();
        }
        BlastFXPoolManager.Instance.ReturnBlastFX(gameObject);
    }

    private IEnumerator ReturnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopEffect();
    }
}
