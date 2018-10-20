using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SF.Animator{
	[CustomEditor(typeof(Animation2D), true)]
	[CanEditMultipleObjects]
	public class Animation2DEditor : Editor {
		// Currently this is an empty stub so a custom Sprite preview drawer will work without the flicker
	}
}