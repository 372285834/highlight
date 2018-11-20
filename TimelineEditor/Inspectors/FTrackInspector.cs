using UnityEngine;
using UnityEditor;

using System;

using GP;
using GPEditor;

namespace GPEditor
{
	[CustomEditor(typeof(FTrack), true)]
	public class FTrackInspector : Editor {

		private SerializedProperty _events = null;

		private bool _allTracksSameType = true;

		private bool _showEvents = true;
		public bool ShowEvents { get { return _showEvents; } set { _showEvents = value; } }

		public virtual void OnEnable()
		{
			if( target == null )
				return;

			FTrack track = (FTrack)target;

			Type trackType = track.GetType();

			for( int i = 0; i != targets.Length; ++i )
			{
				if( trackType != targets[i].GetType() )
				{
					_allTracksSameType = false;
					break;
				}
			}

			if( _allTracksSameType )
				_events = serializedObject.FindProperty("_events");
			else
				_showEvents = false;
		}

		public override void OnInspectorGUI()
		{
			if( _allTracksSameType )
				base.OnInspectorGUI();

			EditorGUI.BeginChangeCheck();
			string newName = EditorGUILayout.TextField( "Name",  target.name );
			if( EditorGUI.EndChangeCheck() )
			{
				target.name = newName;
				EditorUtility.SetDirty( target );
			}

			if( _showEvents && _events != null )
			{
				serializedObject.Update();
				EditorGUILayout.PropertyField( _events, true );
				serializedObject.ApplyModifiedProperties();
			}
		}
	}
}
