using UnityEngine;
using System.Collections;

public class UIcontroller : MonoBehaviour {
	// will be attached to the cube, hopefully?
	public MovementObj cube;
	public GameObject sparkle;
	public float UImovementSpeed; 
	public float UIrotationSpeed;

	private float sparkleTimer = 0;
	private float sparkleTimerMax = 0.1f;

	Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		cube = GetComponent<MovementObj> ();
		cube.setMovementSpeed (UImovementSpeed);
		cube.setRotationSpeed (UIrotationSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		targetPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y,
			Input.mousePosition.z);
		targetPosition.z = 10.0f;
		targetPosition = Camera.main.ScreenToWorldPoint (targetPosition);
		turnTowardsCursor (targetPosition);
		cube.move (1);

		sparkleTimer += Time.deltaTime;
		if (sparkleTimer >= sparkleTimerMax) {
			sparkleTimer = 0;
			Vector3 pos = transform.position;
			float randRange = 0.15f;
			Vector3 randVar = new Vector3 (UnityEngine.Random.Range (-randRange, randRange),
				                  UnityEngine.Random.Range (-randRange, randRange));
			GameObject spark = (GameObject)Instantiate (sparkle, pos + randVar, Quaternion.Euler (0, 0, 0));
			spark.GetComponent<Sparkle> ().spinrate = UnityEngine.Random.Range (2, 8);
		}
	}


	// based off from from BasicEnemyController T.T
	private void turnTowardsCursor(Vector3 targetPosition)
	{
		Vector3 diff = targetPosition - transform.position;
		//print(diff);
		float targetAngle = Mathf.Atan(diff.y / diff.x) * 180 / Mathf.PI + 90;
		if (diff.x > 0)
			targetAngle = 180 + targetAngle;
		targetAngle = (int)targetAngle;
		float objAngle = transform.rotation.eulerAngles.z;

		if (NocturneDefinitions.quickestRotation(objAngle, targetAngle))
		{
			cube.rotate(1f);
		}
		else
		{
			cube.rotate(-1f);
		}
	}
}
