using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{

    [SerializeField] private GameObject myPlayer;
    public GameObject destination;

    // Start is called before the first frame update
    public void TeleportPlayer()
    {
        myPlayer.transform.position = destination.transform.position;
    }
}
