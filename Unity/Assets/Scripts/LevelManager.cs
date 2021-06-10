using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{


    [SerializeField] private CheckpointController _checkpointController = null;
    [SerializeField] private PlayerController _playerController = null;

    private static LevelManager _instance = null;


    private void Awake()
    {
        _instance = this;
    }


    public static LevelManager ReturnInstance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType<LevelManager>();
            if (_instance == null) return null;
        }
        return _instance;
    }

    public PlayerController GetCurrentPlayer()
    {
        return _playerController;
    }
    public CheckpointController GetCurrentCheckpointController()
    {
        return _checkpointController;
    }


}
