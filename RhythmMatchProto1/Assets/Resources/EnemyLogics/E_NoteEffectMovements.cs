using UnityEngine;
using System.Collections;

public class E_NoteEffectMovements : MonoBehaviour {

	private static Vector3 startPosition = new Vector3(-4.21f, -2.18f, -2.0f);

	private static float rotateSpeed = 200.0f;

	private float xSpeed = 0.0f;
	private float ySpeed = 0.0f;

	void OnEnable()
	{
		transform.position = startPosition;
		transform.rotation = Quaternion.Euler(Vector3.zero);

		xSpeed = Random.Range(8.0f, 20.0f);
		ySpeed = Random.Range(15.0f, 30.0f);
	}

	void Update ()
	{

		Vector3 r = new Vector3(0.0f,0.0f,( rotateSpeed * Time.deltaTime ));
		transform.Rotate (r);

		Vector3 temp = transform.position;
		temp.x += xSpeed * Time.deltaTime;
		temp.y += ySpeed * Time.deltaTime;

		transform.position = temp;

		if( temp.y > 4.0f )
		{
			E_NoteManager.GetInstance().ReturnNoteEffectPool(gameObject);
		}
	}


}
