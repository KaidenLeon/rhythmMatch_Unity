using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
public class NoteManager : MonoBehaviour {

	[SerializeField]
	private GameObject noteObject = null;
	[SerializeField]
	private GameObject noteEffectObject = null;

	private int poolSize = 30;
	private Queue<GameObject> notePool = new Queue<GameObject>();
	private Queue<GameObject> noteEffectPool = new Queue<GameObject>();

	private List<Note> noteDatas = new List<Note>();
	private List<NoteMovements> noteShowing = new List<NoteMovements>();

	private static NoteManager _instance = null;
	public static NoteManager GetInstance()
	{
		return _instance;
	}
	
	void Start()
	{
		if (_instance == null)
		{
			_instance = this;
			Initialize();
		}		
		else
		{
			Destroy (gameObject);
		}
	}

	private void Initialize()
	{
		if( !noteObject )
		{
			Debug.LogError("note load error crash...");
			return;
		}
		if( !noteEffectObject )
		{
			Debug.LogError("note effect load error crash...");
			return;
		}

		notePool.Clear();

		for( int i=0; i<poolSize; ++i )
		{
			GameObject note = Instantiate(noteObject) as GameObject;
			note.SetActive(false);
			notePool.Enqueue(note);

			GameObject noteEffect = Instantiate(noteEffectObject) as GameObject;
			noteEffect.SetActive(false);
			noteEffectPool.Enqueue(noteEffect);
		}

		LoadNoteData();
	}

	private void LoadNoteData()
	{
		//noteDatas.Add(new Note(4.0f, NoteType.NOTE_TYPE_LEFT));
		//noteDatas.Add(new Note(6.0f, NoteType.NOTE_TYPE_LEFT));
		//noteDatas.Add(new Note(8.0f, NoteType.NOTE_TYPE_RIGHT));


		try
		{
			XmlDocument xml = new XmlDocument();
			//xml.Load("path");

			// test code
			TextAsset xmlText = (TextAsset) Resources.Load("Music/Sakuranbo_Hard/Sakuranbo_note_hard");
			xml.LoadXml(xmlText.text);

			XmlElement element = xml.DocumentElement;
			XmlNodeList childList = element.ChildNodes;

			for (int i = 0; i < childList.Count; i++)
			{
				XmlElement child = (XmlElement)childList[i];

				string[] split1 = child.InnerText.Split('-');
				float endTime = Convert.ToSingle(split1[0])/1000;
				string type = split1[1];
				//Debug.Log("endTime:"+endTime + " type:"+type);

				if(type.Equals("left"))
				{
					noteDatas.Add(new Note(endTime, NoteType.NOTE_TYPE_LEFT));
				}
				else
				{
					noteDatas.Add(new Note(endTime, NoteType.NOTE_TYPE_RIGHT));
				}
			}
			
		}
		
		catch (Exception ex)
		{
			return;
		}

	}

	public void ReturnNotePool(GameObject note)
	{
		note.SetActive(false);
		notePool.Enqueue(note);
	}

	public void ReturnNoteEffectPool(GameObject noteEffect)
	{
		noteEffect.SetActive(false);
		noteEffectPool.Enqueue(noteEffect);
	}

	public void RunNoteEffect()
	{
		GameObject noteEffect = noteEffectPool.Dequeue();
		noteEffect.SetActive(true);
	}

	void Update ()
	{
		if( GameParameters.HP < 1 )
		{
			return;
		}

		if( noteDatas.Count > 0 )
		{
			float playTime = SoundManager.GetInstance().GetPlayTime();
			Note firstNote = noteDatas[0];

			float startTime = firstNote.GetEndTime() - GameParameters.noteTime;

			if( playTime > startTime )
			{
				GameObject note = notePool.Dequeue();
				note.SetActive(true);
				NoteMovements movements = note.GetComponent<NoteMovements>();
				movements.Initialize(firstNote);
				noteShowing.Add(movements);

				noteDatas.RemoveAt(0);

				// send Network packet
				NetworkManager.GetInstance().SendNoteStart( firstNote.GetNoteType() );
			}
		}
	}


	public NoteMovements SeekShowingNote()
	{
		if(noteShowing.Count > 0)
		{
			return noteShowing[0];
		}

		return null;
	}

	public void NextShowingNote()
	{
		if( noteShowing.Count > 0 )
		{
			ReturnNotePool(noteShowing[0].gameObject);
			noteShowing.RemoveAt(0);
		}
	}


}

