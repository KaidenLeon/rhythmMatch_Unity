using UnityEngine;
using System.Collections;

public class E_NoteMovements : MonoBehaviour {

	[SerializeField]
	private Sprite leftNoteSprite = null;
	[SerializeField]
	private Sprite rightNoteSprite = null;

	private const float moveLength = 2.95f;
	private float noteSpeed = 0.0f;

	private static Vector3 startPosition = new Vector3(-1.92f, 1.36f, -1.0f);
	private static float rotateSpeed = 100.0f;


	private E_Note note = null;
	private SpriteRenderer sr = null;

	void Awake()
	{
		sr = gameObject.GetComponent<SpriteRenderer>(); 
	}

	public void Initialize(NoteType noteType)
	{
		noteSpeed = moveLength / E_GameParameters.noteTime;
		//Debug.Log("noteSpeed:"+noteSpeed);
		//Debug.Log("startTime:" + SoundManager.GetInstance().GetPlayTime());
		if(noteSpeed < 0.0f)
		{
			Debug.LogError("NoteSpeedCalcError:" + noteSpeed);
		}


		transform.position = startPosition;
		transform.rotation = Quaternion.Euler(Vector3.zero);

		switch(noteType)
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
		Debug.Log(E_GameParameters.isNoteRotate);
		if( E_GameParameters.isNoteRotate )
		{
			Vector3 r = new Vector3(0.0f,0.0f,( rotateSpeed * Time.deltaTime ));
			transform.Rotate (r);
		}

	}



}
