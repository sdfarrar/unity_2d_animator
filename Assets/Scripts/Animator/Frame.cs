using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SF.Animator{

	[System.Serializable]
	public class Frame {
		[PreviewSprite]
		public Sprite Sprite;
		public float Duration = 1;
	}

}