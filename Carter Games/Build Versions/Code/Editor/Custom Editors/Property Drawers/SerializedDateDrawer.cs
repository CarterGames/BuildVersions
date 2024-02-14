/*
 * Copyright (c) 2024 Carter Games
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEditor;
using UnityEngine;

namespace CarterGames.Assets.BuildVersions.Editor
{
    /// <summary>
    /// Handles the custom property drawer for the serialized date.
    /// </summary>
    [CustomPropertyDrawer(typeof(SerializedDate))]
    public class SerializedDateDrawer : PropertyDrawer
    {
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Fields
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        private float widthSize;
        private const float OffsetSize = 2f;
        
        /* —————————————————————————————————————————————————————————————————————————————————————————————————————————————
        |   Unity Methods
        ————————————————————————————————————————————————————————————————————————————————————————————————————————————— */
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            
            var labelPos = new Rect(position.x, position.y, position.width, position.height);
            
            EditorGUI.BeginChangeCheck();

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            position = EditorGUI.PrefixLabel(labelPos, label);
            widthSize = position.width / 3f;

            // Rects
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
            var dayLabel = new Rect(position.x, position.y, widthSize - OffsetSize, EditorGUIUtility.singleLineHeight);
            var monthLabel = new Rect(position.x + widthSize * 1 + OffsetSize, position.y, widthSize - OffsetSize * 1, EditorGUIUtility.singleLineHeight);
            var yearLabel = new Rect(position.x + widthSize * 2 + OffsetSize * 2, position.y, widthSize - OffsetSize * 2, EditorGUIUtility.singleLineHeight);
            
            var pos1 = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + OffsetSize, widthSize - OffsetSize, EditorGUIUtility.singleLineHeight);
            var pos2 = new Rect(position.x + widthSize * 1 + OffsetSize, position.y + EditorGUIUtility.singleLineHeight + OffsetSize, widthSize - OffsetSize * 1, EditorGUIUtility.singleLineHeight);
            var pos3 = new Rect(position.x + widthSize * 2 + OffsetSize * 2, position.y + EditorGUIUtility.singleLineHeight + OffsetSize, widthSize - OffsetSize * 2, EditorGUIUtility.singleLineHeight);
            
            
            // Properties
            /* ────────────────────────────────────────────────────────────────────────────────────────────────────────── */
            EditorGUI.LabelField(dayLabel, EditorMetaData.SerializableDate.Day);
            EditorGUI.LabelField(monthLabel, EditorMetaData.SerializableDate.Month);
            EditorGUI.LabelField(yearLabel, EditorMetaData.SerializableDate.Year);
            
            EditorGUI.PropertyField(pos1, property.Fpr("day"), GUIContent.none);
            EditorGUI.PropertyField(pos2, property.Fpr("month"), GUIContent.none);
            EditorGUI.PropertyField(pos3, property.Fpr("year"), GUIContent.none);
            
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }        
            
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
        
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) * 2 + OffsetSize;
        }
    }
}
