using UnityEngine;
using System.Collections;

public class ScoreTimer : MonoBehaviour 
{
	public float tScore = 0;
	public float score = 0;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Network.isServer && Application.loadedLevelName == "RichLeadNetworkTest")
			score =  (int)Mathf.Floor(Time.timeSinceLevelLoad + tScore);
	}
	
	public void GetTreasure(float tValue)
	{
		tScore += tValue;
	}
	
	public void GetArray(VoteScreenGUI.Vote[] votes)
	{
		
		GameSettings.leadScore = score * 5;
		
		Debug.Log(GameSettings.leadScore);
		for(int i = 0; i < votes.Length; i++)
		{
			switch(votes[i])
			{					
				case VoteScreenGUI.Vote.positive:
				if(i == 0)
					GameSettings.support1Score = score * 10;
				if(i == 1)
					GameSettings.support2Score = score * 10;
				if(i == 2)
					GameSettings.support3Score = score * 10;
				break;
				
				case VoteScreenGUI.Vote.neutral:
				if(i == 0)
					GameSettings.support1Score = score;
				if(i == 1)
					GameSettings.support2Score = score;
				if(i == 2)
					GameSettings.support3Score = score;
				break;
				
				case VoteScreenGUI.Vote.negative:
				if(i == 0)
					GameSettings.support1Score = score * -1;
				if(i == 1)
					GameSettings.support2Score = score * -1;
				if(i == 2)
					GameSettings.support3Score = score * -1;
				break;
			}
		}
	}
}
