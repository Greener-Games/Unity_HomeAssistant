using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public class CustomDateTimeViewer : Attribute
{
    public string format;

    public CustomDateTimeViewer(string format)
    {
        this.format = format;
    }
}


public sealed class CustomRangeAttributeDrawer : OdinAttributeDrawer<CustomDateTimeViewer, DateTime>
{
    protected override void DrawPropertyLayout(IPropertyValueEntry<DateTime> entry, CustomDateTimeViewer attribute, GUIContent label)
    {
        var dateTime = this.ValueEntry.SmartValue;

        var rect = EditorGUILayout.GetControlRect();

        if( label != null )
        {
            rect = EditorGUI.PrefixLabel( rect, label );
        }

        EditorGUI.LabelField( rect, dateTime.ToString(attribute.format) );
    }
}