using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismPlatform : MonoBehaviour
{
    public ParticleSystem hitParticles;
    public GameObject ceiling;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ceiling)
        {
            hitParticles.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ceiling)
        {
            hitParticles.Stop();
        }
    }
}
