﻿using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(PreviewSpriteAttribute))]
    public class PreviewSpriteDrawer : PropertyDrawer
    {
        const float imageHeight = 100;
 
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference &&
                (property.objectReferenceValue as Sprite) != null)
            {
                return EditorGUI.GetPropertyHeight(property, label, true) + imageHeight + 10;
            }
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
 
        static string GetPath(SerializedProperty property)
        {
            string path = property.propertyPath;
            int index = path.LastIndexOf(".");
            return path.Substring(0, index + 1);
        }
 
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Draw the normal property field
            EditorGUI.PropertyField(position, property, label, true);

            // Only draw sprite in repaint events
            if(!Event.current.type.Equals(EventType.Repaint)){ return; }
            if (property.propertyType != SerializedPropertyType.ObjectReference){ return; }

            Sprite sprite = property.objectReferenceValue as Sprite;
            if (sprite == null){ return; }

            position.y += EditorGUI.GetPropertyHeight(property, label, true) + 5;
            position.height = imageHeight;

            DrawTexturePreview(position, sprite);
        }
 
        private void DrawTexturePreview(Rect position, Sprite sprite)
        {
            Vector2 fullSize = new Vector2(sprite.texture.width, sprite.texture.height);
            Vector2 size = new Vector2(sprite.textureRect.width, sprite.textureRect.height);
 
            Rect coords = sprite.textureRect;
            coords.x /= fullSize.x;
            coords.width /= fullSize.x;
            coords.y /= fullSize.y;
            coords.height /= fullSize.y;
 
            Vector2 ratio;
            ratio.x = position.width / size.x;
            ratio.y = position.height / size.y;
            float minRatio = Mathf.Min(ratio.x, ratio.y);
 
            Vector2 center = position.center;
            position.width = size.x * minRatio;
            position.height = size.y * minRatio;
            position.center = center;
 
            GUI.DrawTextureWithTexCoords(position, sprite.texture, coords, true);
        }

    }
}