using UnityEngine;
using System.Collections;

public class GaugeMovements : MonoBehaviour {
	private const float minHeight = -10.0f;
	private static GaugeMovements _instance = null;
	public static GaugeMovements GetInstance()
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

	void Update ()
	{
		if(GameParameters.itemGauge > 100)
		{
			GameParameters.itemGauge = 100;
		}

		// 100 : 0
		// 0 : -10

		//-10 / 100;
		float level = minHeight / 100; //gauge hard coding

		// -0.1
		float height = level * GameParameters.itemGauge;
		height = minHeight - height;

		Vector3 temp = transform.position;
		temp.x += (height - temp.x) * 3.0f * Time.deltaTime;
		transform.position = temp;
		

	}
}
