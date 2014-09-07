using UnityEngine;
using System.Collections;

public class E_AlertGrow : MonoBehaviour {

	[SerializeField]
	private Sprite redGrow = null;
	[SerializeField]
	private Sprite blueGrow = null;
	[SerializeField]
	private Sprite grayGrow = null;

	private float BlikingSpeed = 1.0f;
	private static Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
	private float alpha = 0.0f;
	private bool IsGrowing = true;
	private bool IsAlert = false;

	private SpriteRenderer sr = null;
	
	private static E_AlertGrow _instance = null;
	public static E_AlertGrow GetInstance()
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
		
		sr = gameObject.GetComponent<SpriteRenderer>(); 
		Initialize();
	}
	
	void Initialize()
	{
		sr.color = defaultColor;
		IsGrowing = true;
		IsAlert = false;
		alpha = 0.0f;

		if( !redGrow || !blueGrow || !grayGrow )
		{
			Debug.LogError("alert grow sprite miss");
		}
	}
	
	void Update ()
	{

		// high priority mode
		if( E_GameParameters.HP < 4 )
		{
			BlikingSpeed = 2.0f;
			sr.sprite = redGrow;
			IsAlert = true;
		}

		if( !IsAlert )
		{
			return;
		}

		if( IsGrowing )
		{
			alpha += (BlikingSpeed * Time.deltaTime);
			if( alpha > 1.0f )
			{
				IsGrowing = false;
			}
		}
		else
		{
			alpha -= (BlikingSpeed * Time.deltaTime);
			if( alpha < 0.0f )
			{
				IsGrowing = true;
			}
		}
		
		sr.color = new Color(1.0f, 1.0f, 1.0f, alpha);
		
		
	}
}
