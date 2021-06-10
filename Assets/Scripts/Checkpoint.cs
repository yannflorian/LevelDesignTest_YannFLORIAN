using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour,IComparer<Checkpoint>
{
    public Transform RespawnPostitionTransform = null;
    [SerializeField] private int _checkpointId = 0;
    [SerializeField] private List<CheckpointState> _objectsToRestore = new List<CheckpointState>();



    private void OnTriggerEnter(Collider other)
    {
        var playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            LevelManager.ReturnInstance().GetCurrentCheckpointController().SetCurrentCheckpoint(this);
        }
    }


    public void Respawn()
    {
        this.RespawnPlayer();
        _objectsToRestore.ForEach((x) => x.Restore());
    }

    private void RespawnPlayer()
    {
        if (RespawnPostitionTransform == null)
            return;

        var player = LevelManager.ReturnInstance().GetCurrentPlayer();

        if (player == null)
            return;

        player.OnCheckpointRespawn(this);
    }

    public int GetCheckpointId()
    {
        return _checkpointId;
    }

    public int Compare(Checkpoint x, Checkpoint y)
    {
        return x.GetCheckpointId().CompareTo(y.GetCheckpointId());
    }
}
