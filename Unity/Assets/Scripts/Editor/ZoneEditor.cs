using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PQTriggerSystem3D
{
    [CustomEditor(typeof(Zone))]
    public class ZoneEditor : Editor
    {
        private bool CheckMasks(int layerMask, int selectedLayer)
        {
            for (int i = 0; i < 32; i++)
            {
                if (layerMask == (layerMask | (1 << i)))
                {
                    if (Physics.GetIgnoreLayerCollision(i, selectedLayer) == false)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override void OnInspectorGUI()
        {
            var layerMask = serializedObject.FindProperty("_constraints").FindPropertyRelative("LayerMask").intValue;
            var selectedLayer = (serializedObject.targetObject as Zone).gameObject.layer;

            if (CheckMasks(layerMask, selectedLayer) == false)
            {
                EditorGUILayout.HelpBox("Layer of this GameObject does not collide with Zone's LayerMask", MessageType.Error);
            }

            base.OnInspectorGUI();
        }
    }
}