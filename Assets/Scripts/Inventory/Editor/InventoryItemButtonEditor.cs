using UnityEditor;
using ButtonEditor = UnityEditor.UI.ButtonEditor;
using Image = UnityEngine.UI.Image;

namespace Inventory.Editor
{
    [CustomEditor(typeof(InventoryItemButton))]
    public class InventoryItemButtonEditor : ButtonEditor
    {
        private InventoryItemButton _target;

        public override void OnInspectorGUI()
        {
            _target = (InventoryItemButton) target;
            serializedObject.Update();
            EditorGUILayout.Space();
            _target.iconImage = (Image) EditorGUILayout.ObjectField("Icon Image", _target.iconImage, typeof(Image), true);
            _target.bgImage = (Image) EditorGUILayout.ObjectField("Bg Image", _target.bgImage, typeof(Image), true);
            EditorGUILayout.Space();
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }
    }
}