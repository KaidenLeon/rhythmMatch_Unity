using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	private AudioSource bgmSource = null;
	private AudioSource seSource = null;

	[SerializeField]
	private AudioClip bgmClip = null;
	[SerializeField]
	private AudioClip leftNoteClip = null;
	[SerializeField]
	private AudioClip rightNoteClip = null;
	[SerializeField]
	private AudioClip shutterDownClip = null;
	[SerializeField]
	private AudioClip t1GaugeClip = null;
	[SerializeField]
	private AudioClip itemAttackClip = null;
	[SerializeField]
	private AudioClip itemDefenceClip = null;


	private static SoundManager _instance = null;
	public static SoundManager GetInstance()
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

	void Initialize()
	{
		if(!bgmClip || !leftNoteClip || !rightNoteClip)
		{
			Debug.LogError("audio clip not loaded");
			return;
		}

		bgmSource = gameObject.AddComponent<AudioSource>();
		bgmSource.playOnAwake = false;
		bgmSource.clip = bgmClip;

		seSource = gameObject.AddComponent<AudioSource>();
		seSource.playOnAwake = false;

	}

	public void PlayBGM()
	{
		if( !bgmSource || !bgmClip )
		{
			Debug.LogError("bgmSource or clip not loaded");
			return;
		}
		bgmSource.Play();
	}

	public void PlayLeftNote()
	{
		if( !seSource || !leftNoteClip )
		{
			Debug.LogError("seSource or leftNoteClip not loaded");
			return;
		}
		seSource.PlayOneShot(leftNoteClip);
	}

	public void PlayRightNote()
	{
		if( !seSource || !rightNoteClip )
		{
			Debug.LogError("seSource or rightNoteClip not loaded");
			return;
		}
		seSource.PlayOneShot(rightNoteClip);
	}

	public void PlayShutterDown()
	{
		if( !seSource || !shutterDownClip )
		{
			Debug.LogError("seSource or shutterDownClip not loaded");
			return;
		}
		seSource.PlayOneShot(shutterDownClip);
	}




	public void PlayT1GaugeFull()
	{
		if( !seSource || !t1GaugeClip )
		{
			Debug.LogError("seSource or t1GaugeClip not loaded");
			return;
		}
		seSource.PlayOneShot(t1GaugeClip);
	}

	public void PlayItemAttack()
	{
		if( !seSource || !itemAttackClip )
		{
			Debug.LogError("seSource or itemAttackClip not loaded");
			return;
		}
		seSource.PlayOneShot(itemAttackClip);
	}

	public void PlayItemDefence()
	{
		if( !seSource || !itemDefenceClip )
		{
			Debug.LogError("seSource or itemDefenceClip not loaded");
			return;
		}
		seSource.PlayOneShot(itemDefenceClip);
	}



	public float GetPlayTime()
	{
		return bgmSource.time;
	}

	// Update is called once per frame
	void Update () {
	}
	
}
