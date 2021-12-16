using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

/// <summary>
/// Place the labels for the Transitions in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum Transition
{
	NullTransition = 0, // Use this transition to represent a non-existing transition in your system
	SawPlayer = 1,
	LostPlayer = 2,
}

/// <summary>
/// Place the labels for the States in this enum.
/// Don't change the first label, NullTransition as FSMSystem class uses it.
/// </summary>
public enum StateID
{
	NullStateID = 0, // Use this ID to represent a non-existing State in your system
	PatrollingID = 1,
	ChasingPlayerID = 2,
}

//This script has been obtained and editted from Lab 4

public class EnemyBehaviour : MonoBehaviour
{

	public FSMSystem fsm;
	public GameObject player;
	public Transform[] wp;
	public Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();

	public float fieldOfViewAngle = 60f;
	public float sightRange = 300f;

	// Use this for initialization
	void Start()
	{
		MakeFSM();
	}

	private void MakeFSM()
	{


	
		PatrolState patrol = new PatrolState(gameObject, wp);
		patrol.AddTransition(Transition.SawPlayer, StateID.ChasingPlayerID);
		ChasePlayerState chase = new ChasePlayerState(gameObject, player);
		chase.AddTransition(Transition.LostPlayer, StateID.PatrollingID);

		fsm = new FSMSystem();
		fsm.AddState(patrol);
		fsm.AddState(chase);


	}

	public void AddTransition(Transition trans, StateID id)
	{
		map.Add(trans, id);
	}
	public void SetTransition(Transition t) { fsm.PerformTransition(t); }

	// Update is called once per frame
	void Update()
	{
		fsm.CurrentState.Reason(player, gameObject);
		fsm.CurrentState.Act(player, gameObject);
	}


	public bool PlayerInSight(GameObject player, GameObject npc)
	{
		Vector3 toPlayer = player.transform.position - npc.transform.position;
		float angle = Vector3.Angle(npc.transform.forward, toPlayer);




		if (angle <= fieldOfViewAngle)
		{
			RaycastHit hit;
			if (Physics.Raycast(new Vector3(npc.transform.position.x, npc.transform.position.y + 0.5f, npc.transform.position.z), toPlayer, out hit, sightRange))
			{

				if (hit.collider.tag == "Player1")
				{
					return true;
				}
			}
        }
		//    He can only be in sight if angle is less than the field of view.
		//    You should use Raycasting to determine if obstacles are blocking the view of the player.

		return false;
	}
}


public class PatrolState : FSMState
{
	private int currentWayPoint;
	private Transform[] waypoints;
	private EnemyAnimation enemyAnimation;
	private float patrolSpeed = 2.5f;

	public PatrolState(GameObject thisObject, Transform[] wp)
	{
		waypoints = wp;
		currentWayPoint = 0;
		stateID = StateID.PatrollingID;
		enemyAnimation = thisObject.GetComponent<EnemyAnimation>();
	}


	public override void Reason(GameObject player, GameObject npc)
	{
		//Check line of sight.
		if (npc.GetComponent<EnemyBehaviour>().PlayerInSight(player, npc))
		{
			
			npc.GetComponent<EnemyBehaviour>().SetTransition(Transition.SawPlayer);


		}
	}


	public override void Act(GameObject player, GameObject npc)
	{
	
		if (Vector3.Distance(npc.transform.position,waypoints[currentWayPoint].transform.position) < 0.5f)
		{
			
			currentWayPoint = (currentWayPoint + 1) % waypoints.Length;
			
		}





		//Update the target.
		enemyAnimation.setTarget(waypoints[currentWayPoint], patrolSpeed);
	}
}


public class ChasePlayerState : FSMState
{
	private EnemyAnimation enemyAnimation;
	private float chaseSpeed = 4f;
	private float stopDist = 6f;
	NavMeshAgent agent;
	public int waitingTime = 2;

	public ChasePlayerState(GameObject thisObject, GameObject tgt)
	{
		stateID = StateID.ChasingPlayerID;
		enemyAnimation = thisObject.GetComponent<EnemyAnimation>();
	}


	public override void Reason(GameObject player, GameObject npc)
	{
		//Check line of sight.
		if (!npc.GetComponent<EnemyBehaviour>().PlayerInSight(player, npc))
		{
			
			npc.GetComponent<EnemyBehaviour>().SetTransition(Transition.LostPlayer);
			Debug.Log("Lost Player");

		}
	}

	public override void Act(GameObject player, GameObject npc)
	{

		agent = npc.GetComponent<NavMeshAgent>();
		enemyAnimation = npc.GetComponent<EnemyAnimation>();
		enemyAnimation.setTarget(player.transform, chaseSpeed);
		npc.GetComponent<Enemy>().ShootPlayer();
		if (Vector3.Distance(player.transform.position, npc.transform.position) <= stopDist)
		{
			enemyAnimation.Stop();
		}
	}

}



