
using UnityEngine;
using System.Collections;
using UnityEditor;
//可编辑多个物体，附着在类前。一般和CustomEditor一同使用
[CanEditMultipleObjects]
//自定义Transform的显示文件  
[CustomEditor(typeof(Transform))]
public class TransformInspector : Editor
{
    //定义序列化类   mPos mScale 
    SerializedProperty mPos;
    SerializedProperty mScale;
    public override void OnInspectorGUI()
    {
        //定义一个宽15f的标签框 
        EditorGUIUtility.labelWidth = 15f;
        //对mPos初始化，得到其中的序列化类 m_LocalPosition
        mPos = serializedObject.FindProperty("m_LocalPosition");
        mScale = serializedObject.FindProperty("m_LocalScale");
        //运行封装好的3个方法
        DrawPosition();
        DrawRotation();
        DrawScale();
        //保存变化后的值
        serializedObject.ApplyModifiedProperties();
    }

    //绘制Position方法
    void DrawPosition()
    {
        //开始一个水平控制组
        //GUILayout.BeginVertical（）；开始一个垂直控制组
        GUILayout.BeginHorizontal();
        {
            //定义一个按钮P    
            bool reset = GUILayout.Button("P", GUILayout.Width(20f));
            //原来的Position属性框 这里可以不要
            //EditorGUILayout.LabelField("Position", GUILayout.Width(50f));
            //通过PropertyField在编辑器中显示    
            EditorGUILayout.PropertyField(mPos.FindPropertyRelative("x"));
            EditorGUILayout.PropertyField(mPos.FindPropertyRelative("y"));
            EditorGUILayout.PropertyField(mPos.FindPropertyRelative("z"));
            //如果按下P 
            if (reset) mPos.vector3Value = Vector3.zero;
        }
        //结束这个水平控制组
        GUILayout.EndHorizontal();
    }




    //绘制缩放的方法
    void DrawScale()
    {
        //开始一个水平控制组
        GUILayout.BeginHorizontal();
        {
            bool reset = GUILayout.Button("R", GUILayout.Width(20f));
            //EditorGUILayout.LabelField("Scale", GUILayout.Width(50f));
            EditorGUILayout.PropertyField(mScale.FindPropertyRelative("x"));
            EditorGUILayout.PropertyField(mScale.FindPropertyRelative("y"));
            EditorGUILayout.PropertyField(mScale.FindPropertyRelative("z"));
            if (reset) mScale.vector3Value = Vector3.one;
        }
        //结束这个水平控制组
        GUILayout.EndHorizontal();
    }
    //绘制旋转的方法
    void DrawRotation()
    {
        //开始一个水平控制组
        GUILayout.BeginHorizontal();
        {
            bool reset = GUILayout.Button("S", GUILayout.Width(20f));
            //EditorGUILayout.LabelField("Rotation", GUILayout.Width(50f));
            Vector3 ls = (serializedObject.targetObject as Transform).localEulerAngles;
            FloatField("X", ref ls.x);
            FloatField("Y", ref ls.y);
            FloatField("Z", ref ls.z);
            if (reset)
                (serializedObject.targetObject as Transform).localEulerAngles = Vector3.zero;
            else
                (serializedObject.targetObject as Transform).localEulerAngles = ls;
        }
        //结束控制这个水平组
        GUILayout.EndHorizontal();
    }



    void FloatField(string name, ref float f)
    {
        f = EditorGUILayout.FloatField(name, f);
    }
}