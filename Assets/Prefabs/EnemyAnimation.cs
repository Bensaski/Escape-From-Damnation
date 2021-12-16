using UnityEngine;
using System.Collections;

//This script has been obtained and editted from Lab 4
public class EnemyAnimation : MonoBehaviour {

	public float deadZone = 5f;					// The number of degrees for which the rotation isn't controlled by Mecanim.
	
	public float speedDampTime = 0.1f;				// Damping time for the Speed parameter.
	public float angularSpeedDampTime = 0.7f;		// Damping time for the AngularSpeed parameter
	public float angleResponseTime = 0.6f;			// Response time for turning an angle into angularSpeed.

	private UnityEngine.AI.NavMeshAgent nav;					// Reference to the nav mesh agent.
	private Animator anim;						// Reference to the Animator.

	public Transform target;					// Destination of the agent.


	void Awake ()
	{
		// Setting up the references.
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		anim = GetComponent<Animator>();
		
		// Making sure the rotation is controlled by Mecanim.
		nav.updateRotation = false;
		
		// Set the weights for the shooting and gun layers to 1.
		anim.SetLayerWeight(1, 1f);
		anim.SetLayerWeight(2, 1f);
		
		// We need to convert the angle for the deadzone from degrees to radians.
		deadZone *= Mathf.Deg2Rad;
	}
	
	
	void Update () 
	{
		if(target != null)
		{
	
			nav.SetDestination(target.transform.position);

			float speed = 0;
			float angularSpeed = 0;
			DetermineAnimParameters (out speed, out angularSpeed);

			//Set the values of the parameters to the animator.
			anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
			anim.SetFloat("AngularSpeed", angularSpeed, angularSpeedDampTime, Time.deltaTime);
			
		}
	}
	

	
	void DetermineAnimParameters (out float speed, out float angularSpeed)
	{
		
		speed = 0;
		speed = nav.desiredVelocity.magnitude;


		float angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
		

		if(Mathf.Abs(angle) < deadZone)
		{

			transform.LookAt( target );
			angle = 0f;
		}
		
		angularSpeed = angle / angleResponseTime;
	}
	
	
	float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
	{
		
		// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
		float angle = 0;

		// If the vector the angle is being calculated to is 0...
		if(toVector == Vector3.zero)
			// ... the angle between them is 0.
			return 0f;

		
		Vector3.Angle(fromVector, toVector);


		return angle;
	}
	
	public void setTarget(Transform target, float speed = 2.0f)
	{
		this.target = target;
		nav.speed = speed;
	}
	
	public void Stop()
	{
		nav.speed = 0f;
	}
}
