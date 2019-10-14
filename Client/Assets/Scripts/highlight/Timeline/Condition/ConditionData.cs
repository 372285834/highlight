﻿
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace highlight.tl
{
    public enum LogicType
    {
        And = 0,
        Or,
        False,
        //左与,
        //左或,
        //左非,
        //与右,
        //或右,
        //非右,
    }
    public class ConditionStyle : ComponentStyle
    {
        public LogicType logicType;
#if UNITY_EDITOR
        public override void OnInspectorGUI()
        {
            this.logicType = (LogicType)EditorGUILayout.EnumPopup("比较类型：", this.logicType);
            base.OnInspectorGUI();
        }
#endif
    }
    public enum ConditionType
    {
        失败后停止,
        失败后继续,
        永久执行,
    }
    [Time("条件/条件_类型", typeof(ConditionBaseData))]
    public class ConditionBaseStyle : ComponentStyle
    {
        public ConditionType conditionType;
        //public bool isObs;
#if UNITY_EDITOR
        public override void OnInspectorGUI()
        {
            this.conditionType = (ConditionType)EditorGUILayout.EnumPopup("条件_类型：", this.conditionType);
          //  this.isObs = EditorGUILayout.Toggle("注册事件：", this.isObs);
            base.OnInspectorGUI();
        }
#endif
    }
    public class ConditionBaseData : ComponentData<ConditionBaseStyle>
    {
        public ConditionType conditionType { get { return mStyle.conditionType; } }
       // public bool isObs { get { return mStyle.isObs; } }

        public TriggerStatus GetStatus(bool b)
        {
            //  Debug.Log(conditionType.ToString());
            TriggerStatus result = b ? TriggerStatus.Success : TriggerStatus.Failure;
            switch (conditionType)
            {
                case ConditionType.失败后停止:
                    result = b ? TriggerStatus.Success : TriggerStatus.Failure;
                    break;
                case ConditionType.失败后继续:
                    result = b ? TriggerStatus.Success : TriggerStatus.Running;
                    break;
                case ConditionType.永久执行:
                    result = TriggerStatus.Success;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}