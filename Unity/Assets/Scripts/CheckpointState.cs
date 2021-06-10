using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointState : MonoBehaviour
{


    public UnityEvent OnRestore;
    private Vector3 _localPosition;
    private Quaternion _localRotation;
    private Vector3 _localScale;




    // Start is called before the first frame update
    void Start()
    {
        _localPosition = this.transform.localPosition;
        _localRotation = this.transform.localRotation;
        _localScale = this.transform.localScale;
    }


    public void Restore()
    {
        this.transform.localPosition = _localPosition;
        this.transform.localRotation = _localRotation;
        this.transform.localScale = _localScale;
        this.OnRestore?.Invoke();
    }

}
