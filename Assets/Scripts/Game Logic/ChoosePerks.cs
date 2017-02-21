using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChoosePerks : MonoBehaviour {
	public bool isAgent;
	private bool m_canChoose;

	private int m_currentPerk = 0;
	public int maxPerks;

	// Use this for initialization
	void Start () 
	{
		Invoke ("PerkChosen", 2f);	
	}

	void Update()
	{
		if (m_canChoose) 
		{
			//change S to down arrow
			if (Input.GetKeyDown (KeyCode.S)) 
			{
				m_currentPerk += 1;
				if (m_currentPerk >= maxPerks) 
				{
					m_currentPerk = 0;
				}
			}
			//change W to down arrow
			if (Input.GetKeyDown (KeyCode.W)) 
			{
				m_currentPerk -= 1;
				if (m_currentPerk < 0) 
				{
					m_currentPerk = maxPerks;
				}
			}
			//change Space to A Button
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				AgentPerks (m_currentPerk);
			}
		}
	}

    //flips a coin to see if players can choose
	void PerkChosen()
	{
		//flip coin
		int flipCoin = Random.Range(0,2);

		if (flipCoin == 0)
			m_canChoose = false;
		else
			m_canChoose = true;
	}

	public void AIPerks(int perkNo)
	{

	}

	public void AgentPerks(int perkNo)
	{

	}


}
