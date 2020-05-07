using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(CharacterController))]
public class EnemyController : Controller
{

    private NavMeshAgent _navMeshAgent;
    private int _numberOfRays = 10;
    private float _rayAngularSpeed = 20;
    private float _rayMoveAngle = 360;
    private float _currentAngle = 0f;
    private Vector3[] initialRays;
    public Vector3 initialPosition;
    private bool lostTrack = false;
    private CharacterController characterController;
    private NavMeshSurface aiSurface;
    private readonly float TRACKING_DELAY =1.5f;
    private float TRACKING_AFTER_VISION_LOSE = 5f;
    private readonly float TRACKING_SURFACE_COMPUTE_INTERVAL = 0.3f;
    private readonly float ALERT_TIME = 4f;
    private float _trackDelayCounter = 0f;
    private float _trackVisionCounter = 0f;
    private float _trackNavMeshComputerCounter = 0f;
    private float _alertTime_counter = 0f;
    private Vector3 _alertDirection;
    protected override void Start()
    {
        base.Start();
        _trackDelayCounter = TRACKING_DELAY;
        _trackNavMeshComputerCounter = TRACKING_SURFACE_COMPUTE_INTERVAL;
        TRACKING_AFTER_VISION_LOSE = TRACKING_AFTER_VISION_LOSE + 4 * ALERT_TIME;
        _trackVisionCounter = TRACKING_AFTER_VISION_LOSE;

        _alertTime_counter = ALERT_TIME;
        characterController = transform.GetComponent<CharacterController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if(_navMeshAgent != null)
        {
            _navMeshAgent.Warp(gameObject.transform.position);
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
        }
        initialRays = new Vector3[_numberOfRays];
        for (int i = 0; i < _numberOfRays; i++)
        {
            initialRays[i] = Quaternion.AngleAxis(i * (_rayMoveAngle / _numberOfRays), Vector3.up) * (Vector3.forward);
        }

        initialPosition = transform.position;
        aiSurface = FindObjectOfType<NavMeshSurface>();
        aiSurface.BuildNavMesh();
    }

    public TargetInfo Target
    {
        get => _target;
        set
        {
            if (_target == value) return;
            _target = value;
            OnPropertyChanged();
        }
    }

    protected override void Update()
    {
        base.Update();

        TargetInFieldOfView();

    }

    public LayerMask targetsLayerMask;
    private TargetInfo _target;

    bool TargetInFieldOfView()
    {
        for (int i = 0; i < _numberOfRays; i++)
        {
            if (Target != null)
            {
                if (_trackDelayCounter >= 0)
                {
                    _trackDelayCounter -= Time.deltaTime;
                }
                else if (_trackDelayCounter < 0)
                {
                    Target.isVisible = false;
                }
            }
            RaycastHit rayHit;
            Vector3 rayDir = Quaternion.AngleAxis(_currentAngle, Vector3.up) * (initialRays[i]);
            var modifiedPos = transform.position;
            modifiedPos.y = transform.position.y + characterController.height /3;
            Ray ray = new Ray(modifiedPos, rayDir);
            if (Physics.Raycast(ray, out rayHit, 20f))
            {
                if (rayHit.collider.gameObject.tag.Equals("Player"))
                {
                    _trackDelayCounter = TRACKING_DELAY;
                    _trackVisionCounter = TRACKING_AFTER_VISION_LOSE;
                    if (Target == null)
                    {
                        Target = TargetInfo.Create(rayHit.collider.gameObject.transform);

                    }

                    if (Target != null)
                    {
                        Target.isVisible = true;
                        Target.position = Target.transform.position;
                        Target.velocity = Target.characterController.velocity;

                        Target.predictedPosition = Target.transform.position;
                    }
                    
                }
            }
            Debug.DrawRay(modifiedPos, rayDir * 20f);
        }

        _currentAngle += 350f * Time.deltaTime;
        if (_currentAngle > _rayMoveAngle - 0.5f)
        {
            _currentAngle = 0f;
        }

        return true;
    }


    protected override void FixedUpdate()
    {

        if (!gameManager.aIEnabled || _navMeshAgent == null)
        {
            character.Attacking = false;

            return;
        }
        if (Target == null)
        {
            character.Attacking = false;

            if (Vector3.Distance(Vector3.ProjectOnPlane(transform.position, Vector3.up), Vector3.ProjectOnPlane(initialPosition, Vector3.up)) >= 5f)
            {
                _navMeshAgent.SetDestination(initialPosition);
                character.LookingDirection = initialPosition;
                character.MovingDirection = _navMeshAgent.steeringTarget - transform.position;
            }
            else
            {
                BeAlerted();
            }

            return;
        }
//        if (Vector3.Distance(Target.transform.position, transform.position) > 35f)
//        {
//            return;
//        }

        if (!Target.isVisible)
        {
            // idz do lokalizacji, w której cel był ostatnio
            if (_trackVisionCounter >= 0f)
            {
                _trackVisionCounter -= Time.fixedDeltaTime;
                _navMeshAgent.SetDestination(Target.position + Target.velocity * 2f);
                Target.predictedPosition += Target.velocity * Time.deltaTime;

                Debug.DrawRay(transform.position, 1.3f*(Target.predictedPosition - transform.position), Color.yellow);
                if(Vector3.Distance(Vector3.ProjectOnPlane(transform.position, Vector3.up),Vector3.ProjectOnPlane(_navMeshAgent.destination, Vector3.up)) >= 1f)
                {
                    character.MovingDirection = _navMeshAgent.steeringTarget - transform.position;
                    _alertTime_counter = ALERT_TIME;
                    character.LookingDirection = Target.predictedPosition;
                }
                else
                {
                    BeAlerted(true);
                }
            }
            else
            {
//                character.LookingDirection = Target.predictedPosition;
                Target = null;
            }
            
            character.Attacking = false;
            return;
        }
        _navMeshAgent.SetDestination(Target.predictedPosition);
        if (character.Attacking)
        {
            float bulletSpeed = 70f;
            _trackVisionCounter = TRACKING_AFTER_VISION_LOSE;
            if (character.weaponSlot.ItemInSlot is RangeWeaponBase rangeWeapon)
            {
                bulletSpeed = rangeWeapon.bulletSpeed;
            }

            var charControllerVelocity = Target.characterController.velocity;
            character.LookingDirection = predictedPosition(Target.transform.position + 0.1f * charControllerVelocity, transform.position,
                charControllerVelocity, bulletSpeed);
            Debug.DrawRay(transform.position, character.LookingDirection , Color.blue );
            
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.up * 10, Color.red);

//            _navMeshAgent.SetDestination(initialPosition - transform.position);
        }

        if (Vector3.Distance(Target.transform.position, transform.position) < character.attackWeaponRange.Value * 0.8f)
        {
            character.NormalAttack(Vector3.back);
        }
        else
        {
            character.Attacking = false;
            var w = _navMeshAgent.destination - transform.position;
            character.MovingDirection = _navMeshAgent.steeringTarget - transform.position;
            character.LookingDirection = _navMeshAgent.destination;
        }


        if (_trackNavMeshComputerCounter >= 0)
        {
            _trackNavMeshComputerCounter -= Time.fixedDeltaTime;
        }
        else
        {
            _trackNavMeshComputerCounter = TRACKING_SURFACE_COMPUTE_INTERVAL;
        }
        Debug.DrawLine(transform.position, _navMeshAgent.destination * 3);
    }

    private void BeAlerted(bool addTimeToTrackCounter = false)
    {
        character.MovingDirection = Vector3.zero;
        if (_alertTime_counter >= 0)
        {
            _alertTime_counter -= Time.fixedDeltaTime;
            character.LookingDirection = _alertDirection * 2 + transform.position;
            Debug.DrawRay(transform.position, _alertDirection * 3 + Vector3.up * 3, Color.cyan);
        }
        else
        {
            var randomAlert = Random.value * ALERT_TIME * 2;
            if (addTimeToTrackCounter)
                _trackVisionCounter += randomAlert;
            _alertTime_counter = ALERT_TIME + randomAlert;
            _alertDirection = (character.CurrentLookVector) * -1 + Random.insideUnitSphere;
        }
    }


    private Vector3 predictedPosition(Vector3 targetPosition, Vector3 shooterPosition, Vector3 targetVelocity, float projectileSpeed)
    {
        Vector3 displacement = targetPosition - shooterPosition;
        float targetMoveAngle = Vector3.Angle(-displacement, targetVelocity) * Mathf.Deg2Rad;
        //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
        if (targetVelocity.magnitude == 0 || targetVelocity.magnitude > projectileSpeed && Mathf.Sin(targetMoveAngle) / projectileSpeed > Mathf.Cos(targetMoveAngle) / targetVelocity.magnitude)
        {
            return targetPosition;
        }
        //also Sine Formula
        float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / projectileSpeed);
        return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
    }


    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
    }
}
