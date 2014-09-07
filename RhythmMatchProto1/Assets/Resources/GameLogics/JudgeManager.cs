using UnityEngine;
using System.Collections;

public enum JudgeType
{
	JUDGE_TYPE_NONE,
	JUDGE_TYPE_PERFECT,
	JUDGE_TYPE_GOOD,
	JUDGE_TYPE_COOL,
	JUDGE_TYPE_BAD,
	JUDGE_TYPE_MISS,
	JUDGE_TYPE_MAX
}

public class JudgeManager : MonoBehaviour {

	private float perfectRange = 0.1f;
	private float goodRange = 0.2f;
	private float coolRange = 0.3f;
	private float badRange = 0.4f;

	private static JudgeManager _instance = null;
	public static JudgeManager GetInstance()
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

	public void JudgeLeft()
	{
		JudgeNote(NoteType.NOTE_TYPE_LEFT);
	}

	public void JudgeRight()
	{
		JudgeNote(NoteType.NOTE_TYPE_RIGHT);
	}

	private void JudgeNote(NoteType noteType)
	{
		if( GameParameters.HP < 1 )
		{
			return;
		}

		NoteMovements noteMovements = NoteManager.GetInstance().SeekShowingNote();
		if( !noteMovements )
		{
			return;
		}

		Note note = noteMovements.GetNote();
		if( note == null )
		{
			return;
		}

		if( noteType != note.GetNoteType() )
		{
			return;
		}

		float nowTime = SoundManager.GetInstance().GetPlayTime();
		float endTime = note.GetEndTime();

		// manage sensitive
		nowTime += GameParameters.judgeSensitive;

		//if ( thisNote->GetPositionY() > start && thisNote->GetPositionY() < end )
		
		if( (endTime - perfectRange) < nowTime && (endTime + perfectRange) > nowTime )
		{
			NoteManager.GetInstance().NextShowingNote();
			NoteManager.GetInstance().RunNoteEffect();
			JudgeRingEffect.GetInstance().ShowJudgeRing(JudgeType.JUDGE_TYPE_PERFECT);
			JudgeText.GetInstance().ShowJudgeText(JudgeType.JUDGE_TYPE_PERFECT);
			NetworkManager.GetInstance().SendJudgeResult(JudgeType.JUDGE_TYPE_PERFECT);

			++GameParameters.itemGauge;
			++GameParameters.itemGauge;
			return;
		}
		else if( (endTime - goodRange) < nowTime && (endTime + goodRange) > nowTime )
		{
			NoteManager.GetInstance().NextShowingNote();
			NoteManager.GetInstance().RunNoteEffect();
			JudgeRingEffect.GetInstance().ShowJudgeRing(JudgeType.JUDGE_TYPE_GOOD);
			JudgeText.GetInstance().ShowJudgeText(JudgeType.JUDGE_TYPE_GOOD);
			NetworkManager.GetInstance().SendJudgeResult(JudgeType.JUDGE_TYPE_GOOD);

			++GameParameters.itemGauge;
			return;
		}
		else if( (endTime - coolRange) < nowTime && (endTime + coolRange) > nowTime )
		{
			NoteManager.GetInstance().NextShowingNote();
			NoteManager.GetInstance().RunNoteEffect();
			JudgeRingEffect.GetInstance().ShowJudgeRing(JudgeType.JUDGE_TYPE_COOL);
			JudgeText.GetInstance().ShowJudgeText(JudgeType.JUDGE_TYPE_COOL);
			NetworkManager.GetInstance().SendJudgeResult(JudgeType.JUDGE_TYPE_COOL);

			return;
		}
		else if( (endTime - badRange) < nowTime && (endTime + badRange) > nowTime )
		{
			NoteManager.GetInstance().NextShowingNote();
			JudgeText.GetInstance().ShowJudgeText(JudgeType.JUDGE_TYPE_BAD);
			NetworkManager.GetInstance().SendJudgeResult(JudgeType.JUDGE_TYPE_BAD);

			--GameParameters.HP;
			return;
		}

		// too far pass

	}

	void Update () {
	
	}
}
