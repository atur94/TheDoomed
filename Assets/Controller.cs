using UnityEngine;
using UnityEngine.UIElements;

public class Controller : MonoBehaviour
{
    public CharacterController characterController;

    public Player player;

    // Start is called before the first frame update
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private bool _isMoving = false;
    private Vector3 _moveDirection = Vector3.zero;

    public LayerMask enemiesLayerMask;

    [SerializeField]
    private Vector3 lookVector;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        player = GetComponent<Player>();
    }

    private void Update()
    {

        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        _moveDirection *= speed;
        _isMoving = _moveDirection.magnitude > 0.1f;
        _moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(_moveDirection * Time.deltaTime);
        if(characterController.velocity.magnitude > 1f)
        {
            var position = transform.position;
            lookVector = Vector3.Lerp(lookVector,Vector3.ProjectOnPlane(position + _moveDirection, Vector3.up), 0.2f);
            lookVector.y = position.y;
            transform.LookAt(lookVector);
        }

        Collider[] enemies = CheckForEnemiesInRange();
        Transform closestEnemy = GetClosestEnemyInRange(enemies);
        if (Input.GetButton("NormalAttack"))
        {
            if(!_isMoving)
            {
                player.Attack(closestEnemy);
            }
        }

    }

    private Collider[] CheckForEnemiesInRange()
    {
        var enemies = Physics.OverlapSphere(transform.position, 15f, enemiesLayerMask);
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
