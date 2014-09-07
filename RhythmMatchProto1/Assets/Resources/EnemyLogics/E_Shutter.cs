using UnityEngine;
using System.Collections;

public class E_Shutter : MonoBehaviour {

	private const float maxHeight = -0.51f;
	private static Vector3 startPosition = new Vector3(maxHeight, 2.0f, -2.0f);
	private bool soundOnce = true;

	void Start ()
	{
		Initialize();
	}

	void Update ()
	{

		// 0 : -3.6
		// 10 : -0.51

		// -3.09  -> -0.309
		float level = (3.6f - 0.51f) / 10; //hp max hard coding
		float height = level * E_GameParameters.HP;
		height += -3.6f;

		if( E_GameParameters.HP <= 0 )
		{
			height = -3.6f;
		}

		Vector3 temp = transform.position;
		temp.x += (height - temp.x) * 3.0f * Time.deltaTime;
		transform.position = temp;


		if( (temp.x < -3.4f) && soundOnce)
		{
			E_SoundManager.GetInstance().PlayShutterDown();
			soundOnce = false;
		}
	}


	public void Initialize()
	{
		transform.position = startPosition;
	}
}
