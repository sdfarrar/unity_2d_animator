using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SF.Animator{
	[CustomEditor(typeof(Animation2D), true)]
	[CanEditMultipleObjects]
	public class Animation2DEditor : Editor {
		// Currently this is an empty stub so a custom Sprite preview drawer will work without the flicker

		private SerializedObject obj;

		void OnEnable()	{
			obj = new SerializedObject(target);
		}

		public override void OnInspectorGUI() {
			DrawDefaultInspector();
			EditorGUILayout.Space();
			DropAreaGUI();
		}

		// Allows sprites to be dropped in and added
		public void DropAreaGUI() {
			Event evt = Event.current;
			Rect drop_area = GUILayoutUtility.GetRect (0.0f, 50.0f, GUILayout.ExpandWidth (true));
			GUI.Box (drop_area, "Drop Sprites Here");
		
			switch (evt.type) {
			case EventType.DragUpdated:
			case EventType.DragPerform:
				if (!drop_area.Contains (evt.mousePosition))
					return;
				
				DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
			
				if (evt.type == EventType.DragPerform) {
					DragAndDrop.AcceptDrag ();
				
					Animation2D anim = (Animation2D)target;
					List<Frame> frames = new List<Frame>(anim.Frames);
					bool dirty = false;
					foreach (Object dragged_object in DragAndDrop.objectReferences) {
						Sprite sprite = (Sprite) dragged_object;
						Frame frame = new Frame();
						frame.Sprite = sprite;
						frame.Duration = (anim.Frames.Length>0) ? anim.Frames[0].Duration : 1;
						frames.Add(frame);
						dirty = true;
					}

					if(dirty){
						EditorUtility.SetDirty(target);
						anim.Frames = frames.ToArray();
					}
					
				}
				break;
			}

		}

	}
}