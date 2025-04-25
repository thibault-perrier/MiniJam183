using System;
using System.Linq;
using Orders;
using Orders.Base;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(OrderScriptableObject))]
    public class OrderScriptableObjectEditor : UnityEditor.Editor
    {
        private Type[] powerTypes;
        private string[] powerTypeNames;
        private int selectedIndex;

        private void OnEnable()
        {
            powerTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(_assembly => _assembly.GetTypes())
                .Where(_type => _type.IsSubclassOf(typeof(Order)) && !_type.IsAbstract)
                .ToArray();

            powerTypeNames = powerTypes.Select(_type => _type.Name).ToArray();
        }

        public override void OnInspectorGUI()
        {
            OrderScriptableObject _orderScriptableObject = (OrderScriptableObject)target;

            if (_orderScriptableObject.order == null)
            {
                selectedIndex = EditorGUILayout.Popup("Order Type", selectedIndex, powerTypeNames);

                if (GUILayout.Button("Create Order"))
                {
                    _orderScriptableObject.order = (Order)Activator.CreateInstance(powerTypes[selectedIndex]);
                }
            }
            else
            {
                EditorGUILayout.LabelField("Order Type", _orderScriptableObject.order.GetType().Name);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("order"), true);
                serializedObject.ApplyModifiedProperties();
            
                if (GUILayout.Button("Remove Order"))
                {
                    _orderScriptableObject.order = null;
                }
            }

            EditorUtility.SetDirty(target);
        }
    }
}