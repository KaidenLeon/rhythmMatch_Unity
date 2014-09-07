using UnityEngine;
using System.Collections;

public class JudgeText : MonoBehaviour {
	
	
	[SerializeField]
	private Sprite perfectText = null;
	[SerializeField]
	private Sprite goodText = null;
	[SerializeField]
	private Sprite coolText = null;
	[SerializeField]
	private Sprite badText = null;
	[SerializeField]
	private Sprite missText = null;
	
	
	private float alpha = 0.0f;
	private SpriteRenderer sr = null;
	
	
	
	
	private static JudgeText _instance = null;
	public static JudgeText GetInstance()
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
		
		if(!perfectText || !goodText || !coolText || !badText || !missText)
		{
			Debug.LogError("judge text sprite miss");
		}
	}
	
	public void ShowJudgeText(JudgeType judgeType)
	{
		alpha = 1.0f;
		
		switch(judgeType)
		{
		case JudgeType.JUDGE_TYPE_PERFECT:
			sr.sprite = perfectText;
			break;
		case JudgeType.JUDGE_TYPE_GOOD:
			sr.sprite = goodText;
			break;
		case JudgeType.JUDGE_TYPE_COOL:
			sr.sprite = coolText;
			break;
		case JudgeType.JUDGE_TYPE_BAD:
			sr.sprite = badText;
			break;
		case JudgeType.JUDGE_TYPE_MISS:
			sr.sprite = missText;
			break;
		default:
			Debug.LogError("no judgeType");
			alpha = 0.0f;
			break;
		}

		// send Network packet
	}
	
	void Update ()
	{
		if( GameParameters.HP < 1 )
		{
			alpha = 0.0f;
			sr.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			return;
		}

		if(alpha < 0.0f)
		{
			return;
		}
		
		sr.color = new Color(1.0f, 1.0f, 1.0f, alpha);
		alpha -= Time.deltaTime;
	}
}
