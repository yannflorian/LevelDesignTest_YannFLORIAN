using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismTrigger : MonoBehaviour
{
    public GameObject mechanismPlatform;
    public GameObject floor;
    public GameObject Plank1;
    public GameObject Climb1;
    public GameObject Climb2;
    public ParticleSystem explosionParticles;
    public ParticleSystem hitParticles;

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
        if (other.gameObject == mechanismPlatform && floor.activeSelf)
        {
            Debug.Log("PlatformTouched");
            floor.SetActive(false);
            MeshRenderer mr1 = Plank1.GetComponent<MeshRenderer>();
            MeshCollider mc1 = Plank1.GetComponent<MeshCollider>();
            mr1.enabled = true;
            mc1.enabled = true;
            explosionParticles.Play();
            hitParticles.Stop();
            Climb1.SetActive(true);
            Climb2.SetActive(true);
        }
    }
}
