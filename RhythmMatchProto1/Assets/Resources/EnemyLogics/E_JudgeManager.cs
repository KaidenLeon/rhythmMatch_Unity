using UnityEngine;
using System.Collections;


public class E_JudgeManager : MonoBehaviour {

	private float perfectRange = 0.1f;
	private float goodRange = 0.2f;
	private float coolRange = 0.3f;
	private float badRange = 0.4f;

    private static E_JudgeManager _instance = null;
    public static E_JudgeManager GetInstance()
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

	public void JudgeNote( JudgeType judgeType, int combo, int HP, int itemGauge )
	{

		if( E_GameParameters.HP < 1 )
		{
			return;
		}

		switch(judgeType)
		{
		case JudgeType.JUDGE_TYPE_PERFECT:
			E_NoteManager.GetInstance().NextShowingNote();
			E_NoteManager.GetInstance().RunNoteEffect();
			E_JudgeRingEffect.GetInstance().ShowJudgeRing(JudgeType.JUDGE_TYPE_PERFECT);
			E_JudgeText.GetInstance().ShowJudgeText(JudgeType.JUDGE_TYPE_PERFECT);
			break;
		case JudgeType.JUDGE_TYPE_GOOD:
			E_NoteManager.GetInstance().NextShowingNote();
			E_NoteManager.GetInstance().RunNoteEffect();
			E_JudgeRingEffect.GetInstance().ShowJudgeRing(JudgeType.JUDGE_TYPE_GOOD);
			E_JudgeText.GetInstance().ShowJudgeText(JudgeType.JUDGE_TYPE_GOOD);
			break;
		case JudgeType.JUDGE_TYPE_COOL:
			E_NoteManager.GetInstance().NextShowingNote();
			E_NoteManager.GetInstance().RunNoteEffect();
			E_JudgeRingEffect.GetInstance().ShowJudgeRing(JudgeType.JUDGE_TYPE_COOL);
			E_JudgeText.GetInstance().ShowJudgeText(JudgeType.JUDGE_TYPE_COOL);
			break;
		case JudgeType.JUDGE_TYPE_BAD:
			E_NoteManager.GetInstance().NextShowingNote();
			E_JudgeText.GetInstance().ShowJudgeText(JudgeType.JUDGE_TYPE_BAD);
			break;
		case JudgeType.JUDGE_TYPE_MISS:
			E_NoteManager.GetInstance().NextShowingNote();
			E_JudgeText.GetInstance().ShowJudgeText(JudgeType.JUDGE_TYPE_MISS);
			break;
		}

		E_GameParameters.combo = combo;
		E_GameParameters.HP = HP;
		E_GameParameters.itemGauge = itemGauge;

	}

}
