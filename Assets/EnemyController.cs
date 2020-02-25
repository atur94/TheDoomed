using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterController characterController;
    // Start is called before the first frame update
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(_moveDirection);
    }
}
