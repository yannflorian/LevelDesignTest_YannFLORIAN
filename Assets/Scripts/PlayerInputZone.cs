using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInputZone : MonoBehaviour
{
  
    [Serializable]
    public class Settings
    {
        public string ButtonName;
        public UnityEvent EventOnInputPress;
    }

    [SerializeField] Settings _settings;

    private bool _isPlayerIn = false;

    private bool hasObjectToPickUp;
    private bool hasPickedUpObject;
    private GameObject pickableObject;

    private void Update()
    {
        if (!_isPlayerIn || string.IsNullOrEmpty(_settings.ButtonName))
            return;

        if (Input.GetButtonDown(_settings.ButtonName)){
            _settings.EventOnInputPress?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if(player != null)
        {
            _isPlayerIn = true;
        }

        if (other.CompareTag("Pickable") == false)
        {
            Debug.Log("PICKABLE");
        }
    }



    private void onTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            _isPlayerIn = false;
        }
    }

  


}
