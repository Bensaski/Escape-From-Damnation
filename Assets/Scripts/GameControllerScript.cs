using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
	public int count;
	public int numPickups = 2;

	public Text winText;
	public Text enemyCountText;
	public Text positionText;
	public Text velocityText;
	public Text lowestDistanceText;
	public Text healthText;
	public GameObject[] Enemies;
	int enemyCount = 0;
	public static bool pauseGame;

	float positionX;
	float positionY;
	float positionZ;
	Vector3 lastPos;
	public Vector3 currentPos;
	public Transform Sphere;

	public PlayerController refScript;
	public float LowestDistance;
	public float currentDistance;
	public Vector3 playerPos;
	public GameObject[] pickups;
	GameObject pickclosest;

	private LineRenderer lineRenderer;
	public float gameModeNumber;
	public GameObject PauseMenu;


	void Start()
	{
		refScript = GetComponent<PlayerController>();
		pickups = GameObject.FindGameObjectsWithTag("PickUp");
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		gameModeNumber = 0;

		count = 0;
		winText.text = "";
		PauseMenu.SetActive(false);



	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("escape"))
		{
			pauseGame = !pauseGame;
			PauseGame();
		}

		if (Input.GetKeyDown("space"))
		{
			if (gameModeNumber == 0)
			{
				gameModeNumber = 1;
			}

			else if (gameModeNumber == 1)
			{
				
				gameModeNumber = 0;
			}
		}

		if (gameModeNumber == 0)
		{
			positionText.gameObject.SetActive(false);
			velocityText.gameObject.SetActive(false);
			lowestDistanceText.gameObject.SetActive(false);
		}

		if (gameModeNumber == 1)
        {
			positionText.gameObject.SetActive(true);
			velocityText.gameObject.SetActive(true);
			lowestDistanceText.gameObject.SetActive(true);
        }



	}

	public void setEnemyCounter()
    {
		Enemies = GameObject.FindGameObjectsWithTag("Enemy");
		enemyCount = 0;
		foreach (GameObject j in Enemies)
		{
			if (j.activeInHierarchy)
			{

				enemyCount++;
			}
		}

	}
	public void setEnemyCountText()
	{
		enemyCountText.text = "Enemy Count : " + enemyCount.ToString();
		if(enemyCount <= 0)
        {
			//winText.text = "You Win!";
        }

	}
	public void setPositionText()

	{

		positionX = Sphere.transform.position.x;
		positionY = Sphere.transform.position.y;
		positionZ = Sphere.transform.position.z;


		positionText.text = "X: " + positionX.ToString("0.00") + " Y " + positionY.ToString("0.00") + " Z " + positionZ.ToString("0.00");


	}
	public void setHealthText()
    {
		healthText.text = "Health : " + refScript.health;
    }
	

	public void setLowestDistanceText()
    {
		lowestDistanceText.text = "Closest Pickup: " + LowestDistance.ToString("0.00") + " Units Away";
    }

	public void setVelocity()
	{

		currentPos = new Vector3(positionX, positionY, positionZ);
		float velocity = ((currentPos - lastPos).magnitude / Time.deltaTime);
		lastPos = new Vector3(positionX, positionY, positionZ);
		velocityText.text = "Velocity : " + velocity.ToString("0.0");

	}

	public void GetDistance()
	{


		
		lineRenderer.SetWidth(0.1f, 0.1f);
		LowestDistance = 10000;

		playerPos = currentPos;

		

		foreach (GameObject pick in pickups)
		{
			
			pick.GetComponent<Renderer>().material.color = Color.white;
			lineRenderer.SetPosition(0, playerPos);

			currentDistance = Vector3.Distance(playerPos, pick.transform.position);
			if (pick.active == true)
			{
				if (currentDistance < LowestDistance)
				{

					pickclosest = pick;
					LowestDistance = currentDistance;

					lineRenderer.SetPosition(1, pick.transform.position);
					//pick.GetComponent<Renderer>().material.color = Color.blue;
				}
			}
			else
			{

			}
		}
		pickclosest.GetComponent<Renderer>().material.color = Color.blue;
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "PickUp")
		{
			if (refScript.health < 10)
			{
				refScript.health += 1;
				Destroy(other.gameObject);
			}
		}
		if (other.gameObject.tag == "PowerUp")
		{
			PlayerPrefs.SetInt("Shots", 2);
		}

	}

	void PauseGame()
	{
		if (pauseGame == true)
		{
			Time.timeScale = 0f;
			PauseMenu.SetActive(true);
		}
		else
		{
			Time.timeScale = 1;
			PauseMenu.SetActive(false);
		}
	}

}
