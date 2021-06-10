using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{

    private List<Checkpoint> _checkpointList = new List<Checkpoint>();
    private Checkpoint _currentCheckpoint;

    public void RespawnFromLastCheckpoint()
    {
        if(_currentCheckpoint == null)
        {
            this.ReloadCurrentLevel();
        }
        else
        {
            _currentCheckpoint.Respawn();
        }
    }

    public void SetCurrentCheckpoint(Checkpoint checkpoint)
    {
        _currentCheckpoint = checkpoint;
    }

    public void  RegisterCheckpoint(Checkpoint checkpoint)
    {
        if (!_checkpointList.Contains(checkpoint))
        {
            _checkpointList.Add(checkpoint);
            this.SortCheckpointListByCheckpointId();
        }
    }

    private void SortCheckpointListByCheckpointId()
    {
        _checkpointList.Sort();
    }

    private void ReloadCurrentLevel()
    {
        var currentSceneIndex =  SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


}
