using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Controller : MonoBehaviour
{
    public bool IsPlayer { get; set; }

    public Character character;

    private Vector3 _moveDirection = Vector3.zero;

    public LayerMask enemiesLayerMask;

    public GameManager gameManager;

    protected virtual void Start()
    {
        character = GetComponent<Character>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void ActivePlayerControl()
    {
        if (!IsPlayer) return;
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        // Move the controller
        //        if(_moveDirection.magnitude > 0.01f)


        //        Collider[] enemies = CheckForEnemiesInRange();
        //        Transform closestEnemy = GetClosestEnemyInRange(enemies);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        character.MovingDirection = _moveDirection;

        if (Physics.Raycast(ray, out hit))
        {
            character.LookingDirection = Vector3.ProjectOnPlane(hit.point, Vector3.up);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                character.NormalAttack(character.LookingDirection);
            }
            else
            {
                character.Attacking = false;
            }
        }
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void Update()
    {


        ActivePlayerControl();
        if (Input.GetKeyDown(KeyCode.K))
        {
//            character.ApplyDisable(new Slow(character, 0.3f, 2, 2, true));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
//            character.ApplyDisable(new Stun(character, 2f, 1));
            character.TakeDamage(new Damage(5f, 5f, null));
            character.BurnMana(5f);
        }
    }

    private Collider[] CheckForEnemiesInRange()
    {
        var enemies = Physics.OverlapSphere(transform.position, 15f, enemiesLayerMask);
        foreach (var enemy in enemies)
        {


        }
        return enemies;
    }

    private Transform GetClosestEnemyInRange(Collider[] enemies)
    {
        if (enemies == null || enemies.Length == 0) return null;
        if (enemies.Length == 1) return enemies[0].gameObject.transform;

        Collider closestEnemy = enemies[0];
        float distance = (closestEnemy.transform.position - transform.position).magnitude;
        for (int i = 1; i < enemies.Length; i++)
        {
            var dist = (enemies[i].transform.position - transform.position).magnitude;
            if (dist < distance) closestEnemy = enemies[i];
        }

        return closestEnemy.transform;
    }
}
