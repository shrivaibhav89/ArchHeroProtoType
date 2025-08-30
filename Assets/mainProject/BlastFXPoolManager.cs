using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastFXPoolManager : MonoBehaviour
{
    public static BlastFXPoolManager Instance;
    private Dictionary<GameObject, Queue<BlastFx>> poolDictionary = new();
    private int initialPoolSize = 10;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Pre-instantiate blast FX objects
    }

    public BlastFx GetBlastFX(GameObject prefabRef)
    {
        if (!poolDictionary.ContainsKey(prefabRef))
        {
            poolDictionary[prefabRef] = new Queue<BlastFx>();
        }

        if (poolDictionary[prefabRef].Count > 0)
        {
            BlastFx fx = poolDictionary[prefabRef].Dequeue();
            //fx.SetActive(true);
            return fx;
        }

        // If no inactive object is found, instantiate a new one
        for (int i = 0; i < initialPoolSize; i++)
        {
            BlastFx newFX = Instantiate(prefabRef).GetComponent<BlastFx>();
            newFX.prefabRef = prefabRef;
            newFX.gameObject.SetActive(false);
            poolDictionary[prefabRef].Enqueue(newFX);
        }
        return poolDictionary[prefabRef].Dequeue();
    }

    public void ReturnBlastFX(GameObject fx)
    {
        fx.SetActive(false);
        BlastFx blastFxComponent = fx.GetComponent<BlastFx>();
        if (blastFxComponent == null)
        {
            Debug.LogWarning("Returned object does not have a BlastFx component.");
            Destroy(fx);
            return;
        }

        if (blastFxComponent.prefabRef == null)
        {
            Debug.LogWarning("BlastFx prefab reference is null. Cannot return to pool.");
            Destroy(fx);
            return;
        }

        if (!poolDictionary.ContainsKey(blastFxComponent.prefabRef))
        {
            poolDictionary[blastFxComponent.prefabRef] = new Queue<BlastFx>();
        }

        poolDictionary[blastFxComponent.prefabRef].Enqueue(blastFxComponent);
    }
}
