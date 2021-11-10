using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;

	public GameControllerScript refScript2;
	public Camera mainCamera;
	public int health = 10;
	private Rigidbody rb;


	void Start()
    {
		GameControllerScript refScript2 = GetComponent<GameControllerScript>();
		rb = GetComponent<Rigidbody>();


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
			//rb.MovePosition(Vector3.forward * Time.deltaTime * speed);
			//transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
			rb.AddRelativeForce(transform.InverseTransformDirection(Vector3.forward) * speed);
		}
		if (Input.GetKey("s"))
		{
			//transform.Translate(-Vector3.forward * Time.deltaTime * speed, Space.World);
			rb.AddRelativeForce(transform.InverseTransformDirection (- Vector3.forward) * speed);
		}
		if (Input.GetKey("d"))
		{
			//transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
			rb.AddRelativeForce(transform.InverseTransformDirection(Vector3.right) * speed);
		}
		if (Input.GetKey("a"))
		{
			//transform.Translate(-Vector3.right * Time.deltaTime * speed, Space.World);
			rb.AddRelativeForce(transform.InverseTransformDirection (- Vector3.right) * speed);
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
		if(health <= 0)
        {
			refScript2.winText.text = "You Died!";
			Invoke("QuitGame2", 2);

        }
    }

	void QuitGame2()
    {
		Application.Quit();
    }





}