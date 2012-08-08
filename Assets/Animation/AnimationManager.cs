using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {
	
	[System.Serializable]
	public class TextureAnimation
	{
		public Texture2D currentTexture;
		public string name;
		public int FPS;
		public int columnCount;
		public int rowCount;
	};
	
	public TextureAnimation[] animations;
	public TextureAnimation  currentAnimation;
	
	public Animation2D animationController;
	
		// Use this for initialization
	void Start () {
		currentAnimation=animations[0];
		renderer.material.mainTexture=currentAnimation.currentTexture;
		animationController.framesPerSecond=currentAnimation.FPS;
		animationController.columnCount=currentAnimation.columnCount;
		animationController.rowCount=currentAnimation.rowCount;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("1"))
		{
			PlayAnimation("WalkFront");
		}
		else if (Input.GetKeyDown("2"))
		{
			PlayAnimation("WalkBack");
		}
	}
	
	public void PlayAnimation(string name)
	{
		currentAnimation=FindAnimation(name);
		renderer.material.mainTexture=currentAnimation.currentTexture;
		animationController.framesPerSecond=currentAnimation.FPS;
		animationController.columnCount=currentAnimation.columnCount;
		animationController.rowCount=currentAnimation.rowCount;		
	}
	
	public void PlayAnimation(int index)
	{
		if (index<animations.Length-1)
		{
			currentAnimation=animations[index];
			renderer.material.mainTexture=currentAnimation.currentTexture;
			animationController.framesPerSecond=currentAnimation.FPS;
			animationController.columnCount=currentAnimation.columnCount;
			animationController.rowCount=currentAnimation.rowCount;	
		}
	}
	
	public TextureAnimation FindAnimation(string name)
	{
		for(int i=0;i<animations.Length;i++)
		{
			if (animations[i].name==name)
			{
				return animations[i];
			}
		}
		
		return currentAnimation;
	}
}
