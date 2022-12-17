#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using ACPT = UnityEngine.AnimatorControllerParameterType;

namespace Tools
{
    [CustomPropertyDrawer(typeof(AnimatorParameter))]
    public class AnimatorParameterPropertyDrawer : PropertyDrawer
    {
        private static readonly string[] EnumTypeNames = Enum.GetNames(typeof(ACPT));

        private static readonly Dictionary<int, int> ToEnum = new Dictionary<int, int>()
        {
            { 0, 1 },
            { 1, 3 },
            { 2, 4 },
            { 3, 9 },
        };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            Animator animator = ((MonoBehaviour)property.serializedObject.targetObject).GetComponent<Animator>();

            if (animator == null)
            {
                EditorGUILayout.LabelField("No Animator component found!");
                EditorGUI.EndProperty();
                return;
            }

            if (animator.runtimeAnimatorController == null)
            {
                EditorGUILayout.LabelField("The animator has no controller link");
                EditorGUI.EndProperty();
                return;
            }

            AnimatorController editorController = AssetDatabase.LoadAssetAtPath<AnimatorController>(AssetDatabase.GetAssetPath(animator.runtimeAnimatorController));
            SerializedProperty currentType = property.FindPropertyRelative("parameterType");
            Rect propertyTypePopup = position;
            propertyTypePopup.width /= 2;
            currentType.enumValueIndex = EditorGUI.Popup(propertyTypePopup, currentType.enumValueIndex, EnumTypeNames);

            SerializedProperty currentHash = property.FindPropertyRelative("hash");

            AnimatorControllerParameter[] parameters = editorController.parameters;
            List<AnimatorControllerParameter> animatorParameters = parameters.Where(x => x.type == (ACPT)ToEnum[currentType.enumValueIndex]).ToList();

            List<string> animatorParameterNames = new List<string>();
            int currentlySelectedParameterIndex = 0;

            for (int i = 0; i < animatorParameters.Count; i++)
            {
                animatorParameterNames.Add(animatorParameters[i].name);
                if (currentHash.intValue == animatorParameters[i].nameHash)
                {
                    currentlySelectedParameterIndex = i;
                }
            }

            if (animatorParameterNames.Count == 0)
            {
                animatorParameterNames = new List<string>() { "not found" };
            }

            Rect namePopup = position;
            namePopup.width /= 2;
            namePopup.x += namePopup.width;
            int selectedIndex = EditorGUI.Popup(namePopup, currentlySelectedParameterIndex, animatorParameterNames.ToArray());
            if (selectedIndex < animatorParameters.Count)
            {
                currentHash.intValue = animatorParameters[selectedIndex].nameHash;
                property.FindPropertyRelative("name").stringValue = animatorParameterNames[selectedIndex];
            }

            EditorGUI.EndProperty();
        }
    }
}

#endif
