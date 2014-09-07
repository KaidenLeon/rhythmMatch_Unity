using UnityEngine;
using System.Collections;

public class E_JudgeRingEffect : MonoBehaviour {




	[SerializeField]
	private Sprite perfectRing = null;
	[SerializeField]
	private Sprite goodRing = null;
	[SerializeField]
	private Sprite coolRing = null;




	private float alpha = 0.0f;
	private SpriteRenderer sr = null;




	private static E_JudgeRingEffect _instance = null;
	public static E_JudgeRingEffect GetInstance()
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

		if(!perfectRing || !goodRing || !coolRing)
		{
			Debug.LogError("ring sprite miss");
		}
	}

	public void ShowJudgeRing(JudgeType judgeType)
	{
		alpha = 1.0f;

		switch(judgeType)
		{
		case JudgeType.JUDGE_TYPE_PERFECT:
			sr.sprite = perfectRing;
			break;
		case JudgeType.JUDGE_TYPE_GOOD:
			sr.sprite = goodRing;
			break;
		case JudgeType.JUDGE_TYPE_COOL:
			sr.sprite = coolRing;
			break;
		default:
			Debug.LogError("no logType");
			alpha = 0.0f;
			break;
		}
	}

	void Update ()
	{
		if(alpha < 0.0f)
		{
			return;
		}

		sr.color = new Color(1.0f, 1.0f, 1.0f, alpha);

		Vector3 r = new Vector3(0.0f,0.0f,1500.0f * Time.deltaTime);
		transform.Rotate (r);
		alpha -= Time.deltaTime;
	}
}
