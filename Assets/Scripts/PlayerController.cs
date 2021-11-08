using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;

	public GameControllerScript refScript2;
	public Camera mainCamera;
	public int health = 10;


	void Start()
    {
		GameControllerScript refScript2 = GetComponent<GameControllerScript>();

		

	}
    private void Update()
    {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane plane = new Plane(Vector3.up, Vector3.zero);
		float distance;
		if (plane.Raycast(ray, out distance))
		{
			Vector3 target = ray.GetPoint(distance);
			Vector3 direction = target - transform.position;
			float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, rotation, 0);
		}
	}

    void FixedUpdate(){

		if (Input.GetKey("w"))
		{
			transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
		}
		if (Input.GetKey("s"))
		{
			transform.Translate(-Vector3.forward * Time.deltaTime * speed, Space.World);
		}
		if (Input.GetKey("d"))
		{
			transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
		}
		if (Input.GetKey("a"))
		{
			transform.Translate(-Vector3.right * Time.deltaTime * speed, Space.World);
		}

		if (Input.GetKeyDown("left shift") & Input.GetKey("w"))
		{
			/*
			Debug.Log("shift");
			transform.position = transform.forward * 10;
			*/
		}




		refScript2.setPositionText();
		refScript2.setVelocity();
		refScript2.setLowestDistanceText();
		refScript2.setHealthText();
		refScript2.setEnemyCounter();
		refScript2.setEnemyCountText();




	}



	void recieveDamage(int damage)
    {
		health = health - damage;
		Debug.Log(health);
    }





}