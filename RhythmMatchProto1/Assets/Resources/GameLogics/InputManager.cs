using UnityEngine;
using System.Collections;
using System;

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
			GameParameters.myID = UnityEngine.Random.Range(1, 1000000);
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



		///====================================================
		TouchEvent();

	}

	private void TouchEvent()
	{
		/*
		if( Input.touchCount > 1 )
		{
			ItemManager.GetInstance().UseItem();
			return;
		}

		Debug.Log ("111:"+checkOnce);
		if(checkOnce)
		{
			Debug.Log ("222:"+Input.touchCount);

			if( Input.touchCount > 4 )
			{
				Debug.Log ("333");
				SoundManager.GetInstance().PlayBGM();
				GameParameters.HP = 10;
				checkOnce = false;
				return;
			}
		}

		*/
		float acc = Input.acceleration.y * Input.acceleration.x * Input.acceleration.z;
		acc = Math.Abs(acc);


		if (SoundManager.GetInstance().GetPlayTime() < 0.2f)
		{
			if (acc > 2.0f)
			{
				SoundManager.GetInstance ().PlayBGM ();
				GameParameters.HP = 10;
				checkOnce = false;
				return;
			}
		}
		else
		{
			if (acc > 1.0f)
			{
				ItemManager.GetInstance().UseItem();
			}

		}


		Touch[] myTouches = Input.touches;
		for(int i = 0; i < Input.touchCount; i++)
		{
			
			Touch touch = myTouches[i];
			float positionY = touch.position.y;

			if(positionY > (Screen.height / 2) )
			{
				if( !leftDown )
				{
					if ( touch.phase == TouchPhase.Began )
					{
						leftDown = true;
						JudgeManager.GetInstance().JudgeLeft();
						SoundManager.GetInstance().PlayLeftNote();
					}
				}
				else
				{
					if ( touch.phase == TouchPhase.Ended )
					{
						leftDown = false;
					}
				}
			}
			else
			{
				if( !rightDown )
				{
					if ( touch.phase == TouchPhase.Began )
					{
						rightDown = true;
						JudgeManager.GetInstance().JudgeRight();
						SoundManager.GetInstance().PlayRightNote();
					}
				}
				else
				{
					if ( touch.phase == TouchPhase.Ended )
					{
						rightDown = false;
					}
				}
			}
			
			


		}
	}

}
