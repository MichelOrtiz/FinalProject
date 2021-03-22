using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] float timeUntilDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", timeUntilDestroyed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
