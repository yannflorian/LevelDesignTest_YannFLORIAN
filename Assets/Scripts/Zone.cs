using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

using Object = UnityEngine.Object;


    public class Zone : MonoBehaviour
    {
        [System.Serializable]
        public class Constraints
        {
            public GameObject SpecyficGameObject = null;
            public string Tag = "All";
            public LayerMask LayerMask;
        }

        [System.Serializable]
        public class Events
        {
            [System.Serializable] public class TriggerEvent : UnityEvent<GameObject> { };
            [System.Serializable] public class TriggerMessageEvent : UnityEvent<GameObject, string> { };

            public TriggerEvent OnZoneEnter;
            public TriggerEvent OnZoneStay;
            public TriggerEvent OnZoneExit;
            public TriggerMessageEvent OnObjectRejected;
        }

        [SerializeField] protected bool _ignoreEventsOnAwake = false;

        [SerializeField] protected Constraints _constraints;
        [SerializeField] protected Events _events;

        protected Collider _collider = null;
        protected bool _ignoreEvents = false;
        protected List<GameObject> _interactingWith = new List<GameObject>();
        protected Vector3 _lastContactNormal = Vector3.zero;


        protected virtual void Awake()
        {
            if (_ignoreEventsOnAwake)
            {
                _ignoreEvents = true;
            }
        }

        private void EnterTrigger(GameObject enteringObject)
        {
            if (this.enabled == false)
            {
                _events.OnObjectRejected.Invoke(enteringObject, "Zone Component is disabled");
                return;
            }

            if (_constraints.SpecyficGameObject)
            {
                if (enteringObject != _constraints.SpecyficGameObject)
                {
                    _events.OnObjectRejected.Invoke(enteringObject, "Not a specified object");
                    return;
                }
            }
            else
            {
                if (_constraints.Tag.Equals("All") == false && enteringObject.CompareTag(_constraints.Tag) == false)
                {
                    _events.OnObjectRejected.Invoke(enteringObject, "Wrong Tag");
                    return;
                }

                if (_constraints.LayerMask != (_constraints.LayerMask | 1 << enteringObject.layer))
                {
                    _events.OnObjectRejected.Invoke(enteringObject, "Wrong Layer");
                    return;
                }
            }


            _interactingWith.Add(enteringObject);

            if (_ignoreEvents)
            {
                _ignoreEvents = false;
            }
            else
            {
                _events.OnZoneEnter?.Invoke(enteringObject);
            }
        }

        public void ExitTrigger(GameObject exitingObject)
        {
            if (_interactingWith.Contains(exitingObject) == false) { return; }


        _interactingWith.Remove(exitingObject);
            _events.OnZoneExit?.Invoke(exitingObject);
        }

        private void ExitZoneWithAll()
        {
            while (_interactingWith.Count > 0)
            {
                ExitTrigger(_interactingWith[0]);
            }
        }

        virtual protected void Update()
        {
           

            foreach (var go in _interactingWith.Where(go =>  go == null  || go.activeSelf == false  ).ToList())
            {
                ExitTrigger(go);
            }

            for (int i = 0; i < _interactingWith.Count; i++)
            {
                var interactedObject = _interactingWith[i];

                if (interactedObject == null ||  interactedObject.activeSelf == false)
                {
                    ExitTrigger(interactedObject);
                    i--;
                    continue;
                }

                _events.OnZoneStay?.Invoke(interactedObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            EnterTrigger(other.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (_interactingWith.Contains(other.gameObject) == false)
            {
                EnterTrigger(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            ExitTrigger(other.gameObject);
        }

      
        private void OnDisable()
        {
            ExitZoneWithAll();
        }

       
    }
