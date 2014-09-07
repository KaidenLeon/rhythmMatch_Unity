using UnityEngine;
using System.Collections;
using System;
public class E_ItemManager : MonoBehaviour {

	[SerializeField]
	private Sprite t2RotateColor = null;
	[SerializeField]
	private Sprite t2RotateGray = null;

	private bool soundPlayOnce = true;
	private float elapsedTime = 0.0f;
	private SpriteRenderer sr = null;


    private static E_ItemManager _instance = null;
    public static E_ItemManager GetInstance()
	{
		return _instance;
	}
	
	void Start()
	{
		if (_instance == null)
		{
			_instance = this;
			sr = gameObject.GetComponent<SpriteRenderer>(); 
		}		
		else
		{
			Destroy (gameObject);
		}
	}


	void Update()
	{

		if( E_GameParameters.itemGauge > 30 )
		{
			if( soundPlayOnce )
			{
				E_SoundManager.GetInstance().PlayT1GaugeFull();
				soundPlayOnce = false;
			}
			elapsedTime += (Time.deltaTime * 6.0f);
			sr.sprite = t2RotateColor;
			Vector3 r = new Vector3(0.0f,0.0f, (float)Math.Sin(  elapsedTime ));
			transform.Rotate (r);

		}
		else
		{
			elapsedTime = 0;
			sr.sprite = t2RotateGray;
		}
	}

	public void UseItem()
	{
		if( E_GameParameters.itemGauge > 30 )
		{
			E_GameParameters.itemGauge = 0;
			transform.rotation = Quaternion.Euler(Vector3.zero);

			//test
			E_SoundManager.GetInstance().PlayItemAttack();
			soundPlayOnce = true;
		}
	}

	public void Rotate(bool isON)
	{
		E_GameParameters.isNoteRotate = isON;
	}

}
