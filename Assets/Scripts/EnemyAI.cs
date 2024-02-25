using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public List<Transform> PatrolPoints;
    public PlayerController Player;
    public float ViewAngel;

    private NavMeshAgent _navMeshAgent;
    private bool _isPlayerNoticed;

    // Start is called before the first frame update
    void Start()
    {
        InitComponentLinks();
        ChaseUpdate();
        PickNewPatrolPoint();
    }
    private void InitComponentLinks()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        NoticePlayerUpdate();

        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = Player.transform.position;
        }

        PatrolUpdate();
    }
    private void NoticePlayerUpdate()
    {
        var direction = Player.transform.position - transform.position;
        _isPlayerNoticed = false;
        if (Vector3.Angle(transform.forward, direction) < ViewAngel)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if(hit.collider.gameObject == Player.gameObject)
                {
                    _isPlayerNoticed = true;
                } 
            }
        }
    }
     
    private void PatrolUpdate()
    {
        if (_navMeshAgent.remainingDistance == 0)
        {
            PickNewPatrolPoint();
        }
    }

    private void PickNewPatrolPoint()
    {
        _navMeshAgent.destination = PatrolPoints[Random.Range(0, PatrolPoints.Count)].position;
    }
    private void ChaseUpdate()
    {
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = Player.transform.position;
        }
    }
}
