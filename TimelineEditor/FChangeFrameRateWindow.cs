﻿using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

using GP;

namespace GPEditor
{
	public class FChangeFrameRateWindow : EditorWindow {

		public static void Show( Vector2 guiPos, GTimelineEditor sequence, UnityAction<GTimelineEditor, int, bool> callback )
		{
			FChangeFrameRateWindow window = CreateInstance<FChangeFrameRateWindow>();

			Rect r = new Rect();
			r.min = EditorGUIUtility.GUIToScreenPoint( guiPos );
			r.width = 0;
			r.height = 0;

			window._sequence = sequence;
			window._frameRate = 25;
//			window.OnChange.AddListener( callback );
			window.OnChange = callback;

			window.ShowAsDropDown( r, new Vector2(200, 100) );
		}

//		private ChangeFrameRateEvent OnChange = new ChangeFrameRateEvent();
		private UnityAction<GTimelineEditor, int, bool> OnChange = null;
		
		private GTimelineEditor _sequence;
		
		private int _frameRate;

		void OnGUI()
		{
			EditorGUIUtility.labelWidth = 70;
			
			_frameRate = EditorGUILayout.IntSlider( "Frame Rate", _frameRate, 1, 120 );
			
			GUILayout.Space( 10 );
			
			EditorGUILayout.LabelField( "New Length", (_sequence.Length*_sequence.InverseFrameRate*_frameRate).ToString());
			
			EditorGUIUtility.labelWidth = 0;
			
			GUILayout.FlexibleSpace();
			
			EditorGUILayout.BeginHorizontal();
			if( GUILayout.Button( "Cancel", GUILayout.Width(80) ) )
				Close();
			
			GUILayout.FlexibleSpace();
			
			if( GUILayout.Button( "Change", GUILayout.Width(80) ) )
			{
				OnChange.Invoke( _sequence, _frameRate, true );
				Close();
			}
			
			EditorGUILayout.EndHorizontal();
		}

//		private class ChangeFrameRateEvent : UnityEvent<GTimeline, int, bool>
//		{
//		}
	}
}
