using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SF.Animator {
	public class Animator2D : MonoBehaviour {

		public bool IsPlaying;
		public Animation2D Idle;
		public Animation2D Move;
		public Animation2D Attack;
		public new SpriteRenderer renderer;

		Animation2D currentAnimation;
		int frameIndex;
		float timer;

		private void Start() {
			PlayAnimation(AnimationType.idle);
		}

		private void Update() {
			if(!IsPlaying){ return; }

			timer += Time.deltaTime;
			if(timer > currentAnimation.GetDuration(frameIndex)){
				timer = 0;
				++frameIndex;

				if(frameIndex > currentAnimation.Frames.Length - 1){
					if(currentAnimation.Loop){
						frameIndex = 0;
					}else{
						--frameIndex;
						IsPlaying = false;
					}
				}
				renderer.sprite = currentAnimation.Frames[frameIndex].Sprite;
			}

		}

		public void PlayAnimation(AnimationType type){
			frameIndex = 0;
			timer = 0;
			IsPlaying = true;
			currentAnimation = GetAnimationByType(type);
			renderer.flipY = currentAnimation.Mirror;
		}

		Animation2D GetAnimationByType(AnimationType type) {
			switch(type){
				case AnimationType.idle: return Idle;
				case AnimationType.move: return Move;
				case AnimationType.attack: return Attack;
				default: return null;
			}
		}

	}

	public enum AnimationType {
		idle,move,attack
	}
}