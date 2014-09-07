using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	private bool leftDown = false;
	private bool rightDown = false;
	private bool downDown = false;

	private static InputManager _instance = null;
	public static InputManager GetInstance()
	{
		return _instance;
	}
	
	void Start()
	{
		if (_instance == null)
		{
			_instance = this;
			GameParameters.myID = Random.Range(1, 1000000);
		}		
		else
		{
			Destroy (gameObject);
		}

	}

	// test code
	private bool checkOnce = true;

	void Update ()
	{
		// test code
		if(checkOnce)
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				SoundManager.GetInstance().PlayBGM();
				GameParameters.HP = 10;
				checkOnce = false;
			}

		}

		if( !leftDown )
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				leftDown = true;
				JudgeManager.GetInstance().JudgeLeft();
				//Debug.Log(SoundManager.GetInstance().GetPlayTime());
				SoundManager.GetInstance().PlayLeftNote();
			}
		}
		else
		{
			if (Input.GetKeyUp(KeyCode.LeftArrow))
			{
				leftDown = false;
			}
		}

			
		
		if( !rightDown )
		{
			if (Input.GetKey(KeyCode.RightArrow))
			{
				rightDown = true;
				JudgeManager.GetInstance().JudgeRight();
				SoundManager.GetInstance().PlayRightNote();
			}
		}
		else
		{
			if (Input.GetKeyUp(KeyCode.RightArrow))
			{
				rightDown = false;
			}
		}




		if( !downDown )
		{
			if (Input.GetKey(KeyCode.DownArrow))
			{
				downDown = true;
				ItemManager.GetInstance().UseItem();
			}
		}
		else
		{
			if (Input.GetKeyUp(KeyCode.DownArrow))
			{
				downDown = false;
			}
		}

	}


}
