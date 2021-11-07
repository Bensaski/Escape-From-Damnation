using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;

	public GameControllerScript refScript2;
	public Camera mainCamera;
	public int health = 10;
	public GameObject rot;


	







	



	void Start()
    {
		GameControllerScript refScript2 = GetComponent<GameControllerScript>();

		

	}
    private void Update()
    {
		//Vector3 mousePos = Input.mousePosition;

		//transform.LookAt(mousePos);
		/*
		Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayLength;

		if (groundPlane.Raycast(cameraRay, out rayLength))
		{
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);
			Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

			transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
		}*/

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
		float horAxis = Input.GetAxis("Horizontal");
		float verAxis = Input.GetAxis("Vertical");

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

		//Vector3 movement = new Vector3(horAxis,0.0f,verAxis);
		//transform.position += transform.position + movement * speed * Time.deltaTime;


		//GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);



		refScript2.setPositionText();
			refScript2.setVelocity();
			//refScript2.GetDistance();
			refScript2.setLowestDistanceText();
			refScript2.setHealthText();




	}


	void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag == "PickUp")
        {
			other.gameObject.SetActive(false);
			refScript2.count++;
			refScript2.setCountText();
        }
    }

	void recieveDamage(int damage)
    {
		health = health - damage;
		Debug.Log(health);
    }





}