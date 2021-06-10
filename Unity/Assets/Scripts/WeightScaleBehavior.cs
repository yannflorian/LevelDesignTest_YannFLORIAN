using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightScaleBehavior : MonoBehaviour
{
    public GameObject mechanism;
    public GameObject Box1;
    public GameObject Box2;
    public GameObject Box3;
    private bool Box1OnTrigger = false;
    private bool Box2OnTrigger = false;
    private bool Box3OnTrigger = false;
    public float mechanismBlockedAngle;
    private float mechanismAngleMax = 180;
    private bool ceilingDestroyed = false;
    private int weight = 0;
    private float mechanismAngle = 0;
    private float mechanismAngleFinal = 0;
    private bool playerOnScale = false;
    private float playerVerticalVelocityMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mechanismAngle != mechanismAngleFinal)
        {
            if (mechanismAngle < mechanismAngleFinal)
            {
                mechanismAngle += 30 * Time.deltaTime * playerVerticalVelocityMultiplier;
            }
            else
            {
                mechanismAngle -= 50 * Time.deltaTime;
            }
            mechanism.transform.rotation = Quaternion.Euler(0, 0, mechanismAngle);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Vector3 vel = rb.velocity;
            playerVerticalVelocityMultiplier = Mathf.Abs(vel.y) / 2 + 1;
            Debug.Log("Velocity is " + vel.y);
            if (vel.y < -8)
            {
                Debug.Log("Destroy Roof");
                ceilingDestroyed = true;
                
            }
            
             playerOnScale = true;
        }

        else if (other.gameObject == Box1)
        {
            Box1OnTrigger = true;
        }

        else if (other.gameObject == Box2)
        {
            Box2OnTrigger = true;
        }

        else if (other.gameObject == Box3)
        {
            Box3OnTrigger = true;
        }

        updateAngleFinal();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnScale = false;
        }

        else if (other.gameObject == Box1)
        {
            Box1OnTrigger = false;
        }

        else if (other.gameObject == Box2)
        {
            Box2OnTrigger = false;
        }

        else if (other.gameObject == Box3)
        {
            Box3OnTrigger = false;
        }

        updateAngleFinal();

    }

    private void updateAngleFinal()
    {
        if (playerOnScale)
        {
            weight = 3;
        }
        else
        {
            if (Box1OnTrigger)
            {
                weight++;
            }
            if (Box2OnTrigger)
            {
                weight++;
            }
            if (Box3OnTrigger)
            {
                weight++;
            }
        }
        
        mechanismAngleFinal = weight * 60;
        Debug.Log(weight);
        Debug.Log(mechanismAngleFinal);

        if (mechanismAngleFinal > mechanismBlockedAngle && !ceilingDestroyed)
        {
            mechanismAngleFinal = mechanismBlockedAngle;
        }
        else if (mechanismAngleFinal > mechanismBlockedAngle && ceilingDestroyed)
        {
            mechanismAngleFinal = mechanismAngleMax;
        }

        // reset weight value
        weight = 0; 
        Debug.Log(mechanismAngleFinal);
    }
}
