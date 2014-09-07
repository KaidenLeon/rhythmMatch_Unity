using UnityEngine;
using System.Collections;

public class Shutter : MonoBehaviour {

	private const float maxHeight = 10.3f;
	private static Vector3 startPosition = new Vector3(maxHeight, 0.0f, -5.0f);
	private bool soundOnce = true;

	void Start ()
	{
		Initialize();
	}

	void Update ()
	{

		float level = maxHeight / 10; //hp max hard coding
		float height = level * GameParameters.HP;

		if( GameParameters.HP <= 0 )
		{
			height = 0.0f;
		}

		Vector3 temp = transform.position;
		temp.x += (height - temp.x) * 3.0f * Time.deltaTime;
		transform.position = temp;


		if( (temp.x < 0.1f) && soundOnce)
		{
			SoundManager.GetInstance().PlayShutterDown();
			soundOnce = false;
		}
	}


	public void Initialize()
	{
		transform.position = startPosition;
	}
}
