using UnityEngine;
using System.Collections;

public enum AlertGrowType
{
	ALERT_GROW_NONE,
	ALERT_GROW_RED,
	ALERT_GROW_BLUE,
	ALERT_GROW_GRAY,
	ALERT_GROW_MAX
}

public class AlertGrow : MonoBehaviour {

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
	
	private static AlertGrow _instance = null;
	public static AlertGrow GetInstance()
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
		if( GameParameters.HP < 4 )
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
