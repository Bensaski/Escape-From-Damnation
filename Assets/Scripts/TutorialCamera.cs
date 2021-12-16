using UnityEngine;
using System.Collections;

public class TutorialCamera: MonoBehaviour
{

	public GameObject player;

	void Start()
	{

	}

	void LateUpdate()
	{


	
			transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 15f, player.transform.position.z);
		

	}

}
