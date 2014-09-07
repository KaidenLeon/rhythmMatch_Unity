using UnityEngine;
using System.Collections;

public class NoteMovements : MonoBehaviour {

	[SerializeField]
	private Sprite leftNoteSprite = null;
	[SerializeField]
	private Sprite rightNoteSprite = null;

	private const float moveLength = 9.85f;
	private float noteSpeed = 0.0f;

	private static Vector3 startPosition = new Vector3(5.6f,-2.155086f, -1.0f);
	private static float rotateSpeed = 100.0f;


	private Note note = null;
	private SpriteRenderer sr = null;

	void Awake()
	{
		transform.localScale = new Vector2(1.038939f, 1.038934f);
		sr = gameObject.GetComponent<SpriteRenderer>(); 
	}

	public void Initialize(Note noteData)
	{
		note = noteData;

		noteSpeed = moveLength / GameParameters.noteTime;
		//Debug.Log("noteSpeed:"+noteSpeed);
		//Debug.Log("startTime:" + SoundManager.GetInstance().GetPlayTime());
		if(noteSpeed < 0.0f)
		{
			Debug.LogError("NoteSpeedCalcError:" + noteSpeed);
		}


		transform.position = startPosition;
		transform.rotation = Quaternion.Euler(Vector3.zero);

		switch(note.GetNoteType())
		{
		case NoteType.NOTE_TYPE_LEFT:
			sr.sprite = leftNoteSprite;
			break;

		case NoteType.NOTE_TYPE_RIGHT:
			sr.sprite = rightNoteSprite;
			break;

		default:
			sr.sprite = leftNoteSprite;
			Debug.LogError("Note Sprite Error");
			break;
		}

	}

	void Update () {
		Vector3 temp = transform.position;
		temp.x = temp.x - ( noteSpeed * Time.deltaTime );
		transform.position = temp;

		if( GameParameters.isNoteRotate )
		{
			Vector3 r = new Vector3(0.0f,0.0f,( rotateSpeed * Time.deltaTime ));
			transform.Rotate (r);
		}

		if( temp.x < -5.55f )
		{
			NoteManager.GetInstance().NextShowingNote();
			JudgeText.GetInstance().ShowJudgeText(JudgeType.JUDGE_TYPE_MISS);
			--GameParameters.HP;
		}
	}


	public float GetPosX()
	{
		return transform.position.x;
	}

	// mendokusai...
	public Note GetNote()
	{
		return note;
	}

}
