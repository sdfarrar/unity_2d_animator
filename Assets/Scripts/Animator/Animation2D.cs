using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SF.Animator{

	[CreateAssetMenu(menuName="2D Animation")]
	public class Animation2D : ScriptableObject {
		public Frame[] Frames;
		public float SampleRate = 0.1f;
		public bool Loop;
		public bool Mirror;

		public float GetDuration(int i){
			return Frames[i].Duration * SampleRate;
		}
	}

}