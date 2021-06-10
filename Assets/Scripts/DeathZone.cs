using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        if(playerController != null)
        {
            LevelManager.ReturnInstance().GetCurrentCheckpointController().RespawnFromLastCheckpoint();
        }
    }
}
