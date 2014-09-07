using UnityEngine;
using System.Collections;

public class E_InputManager : MonoBehaviour {


    private static E_InputManager _instance = null;
    public static E_InputManager GetInstance()
	{
		return _instance;
	}
	
	void Start()
	{
		if (_instance == null)
		{
			_instance = this;
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
				E_SoundManager.GetInstance().PlayBGM();
				E_GameParameters.HP = 10;
				checkOnce = false;
			}

		}


	}


}
