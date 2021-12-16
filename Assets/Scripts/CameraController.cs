using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour { 

	public GameObject player;

	private Vector3 offset;
	bool set = false;

	void Start(){

		
		Invoke("SetPosition", 10);

	}

	void LateUpdate(){


        if (set)
        {
			transform.position = new Vector3(player.transform.position.x,player.transform.position.y + 15f, player.transform.position.z);
		}

	}

	public void SetPosition()
    {
		set = true;
	}
}
