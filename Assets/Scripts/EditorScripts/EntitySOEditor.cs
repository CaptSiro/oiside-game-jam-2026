
using UnityEngine;
using UnityEditor;
using ScriptableObjects;

[CustomEditor(typeof(EntitySO))]
public class EntitySOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw all normal EntitySO fields except attacks
        DrawPropertiesExcluding(serializedObject, "m_Script", "attacks");

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Attacks", EditorStyles.boldLabel);

        SerializedProperty attacks =
            serializedObject.FindProperty("attacks");

        // Draw existing attacks
        for (int i = 0; i < attacks.arraySize; i++)
        {
            SerializedProperty attack =
                attacks.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginVertical("box");

            string typeName = attack.managedReferenceValue != null
                ? attack.managedReferenceValue.GetType().Name
                : "Missing Attack";

            EditorGUILayout.BeginHorizontal();

            attack.isExpanded = EditorGUILayout.Foldout(
                attack.isExpanded,
                $"{i + 1}. {typeName}",
                true
            );

            if (GUILayout.Button("Remove", GUILayout.Width(65)))
            {
                attacks.DeleteArrayElementAtIndex(i);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                break;
            }

            EditorGUILayout.EndHorizontal();

            if (attack.isExpanded &&
                attack.managedReferenceValue != null)
            {
                EditorGUILayout.PropertyField(attack, GUIContent.none, true);
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space(5);

        // Add attack dropdown
        if (GUILayout.Button("Add Attack"))
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(
                new GUIContent("Projectile Attack"),
                false,
                () => AddAttack(typeof(EntitySO.ProjectileAttack))
            );

            menu.AddItem(
                new GUIContent("Charge Attack"),
                false,
                () => AddAttack(typeof(EntitySO.ChargeAttack))
            );

            menu.ShowAsContext();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void AddAttack(System.Type attackType)
    {
        EntitySO entity = (EntitySO)target;

        Undo.RecordObject(entity, "Add Attack");

        SerializedObject so = new SerializedObject(entity);
        so.Update();

        SerializedProperty attacks = so.FindProperty("attacks");

        int index = attacks.arraySize;
        attacks.arraySize++;

        SerializedProperty newAttack =
            attacks.GetArrayElementAtIndex(index);

        if (attackType == typeof(EntitySO.ProjectileAttack))
            newAttack.managedReferenceValue = new EntitySO.ProjectileAttack();

        else if (attackType == typeof(EntitySO.ChargeAttack))
            newAttack.managedReferenceValue = new EntitySO.ChargeAttack();

        so.ApplyModifiedProperties();

        EditorUtility.SetDirty(entity);
    }
}
