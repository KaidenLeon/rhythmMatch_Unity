using UnityEngine;
using System.Collections;

public class E_GaugeMovements : MonoBehaviour {

	private const float minHeight = -6.6f;
	private static E_GaugeMovements _instance = null;
	public static E_GaugeMovements GetInstance()
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
		if( E_GameParameters.itemGauge > 100 )
		{
			E_GameParameters.itemGauge = 100;
		}

		// 100 : -3.6
		// 0 : -6.6

		float level = (minHeight + 3.6f) / 100; //gauge hard coding

		// -0.03
		float height = level * E_GameParameters.itemGauge;
		height = minHeight - height;

		Vector3 temp = transform.position;
		temp.x += (height - temp.x) * 3.0f * Time.deltaTime;
		transform.position = temp;
		

	}
}
