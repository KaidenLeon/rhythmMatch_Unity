using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
public class E_NoteManager : MonoBehaviour {

	[SerializeField]
	private GameObject noteObject = null;
	[SerializeField]
	private GameObject noteEffectObject = null;

	private int poolSize = 30;
	private Queue<GameObject> notePool = new Queue<GameObject>();
	private Queue<GameObject> noteEffectPool = new Queue<GameObject>();
	private List<E_NoteMovements> noteShowing = new List<E_NoteMovements>();

    private static E_NoteManager _instance = null;
    public static E_NoteManager GetInstance()
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

	public void StartNote(NoteType noteType)
	{
		GameObject note = notePool.Dequeue();
		note.SetActive(true);
		E_NoteMovements movements = note.GetComponent<E_NoteMovements>();
		movements.Initialize(noteType);
		noteShowing.Add(movements);
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

