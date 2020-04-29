using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : Controller
{

    private NavMeshAgent _navMeshAgent;
    protected override void Start()
    {
        base.Start();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if(_navMeshAgent != null)
        {
            _navMeshAgent.Warp(gameObject.transform.position);
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        if (!gameManager.aIEnabled)
        {
            character.Attacking = false;
            return;
        }
        if (_navMeshAgent == null) return;
        var target = gameManager.PlayersList.First().gameObject.transform.position;
        _navMeshAgent.SetDestination(target);
        character.LookingDirection = _navMeshAgent.destination;

        if (Vector3.Distance(target, transform.position) < character.attackWeaponRange.Value * 0.8f)
        {
            character.NormalAttack(Vector3.back);
        }
        else
        {
            character.Attacking = false;
            var w = _navMeshAgent.destination - transform.position;
            Debug.Log(w);
            character.MovingDirection = w;
        }

        Debug.DrawLine(transform.position, _navMeshAgent.destination * 3);
    }
}
