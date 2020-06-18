using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    public override void LateInitialization()
    {
        base.LateInitialization();
        FindObjectOfType<PlayerUIController>().BindUIToCharacter(this);
    }
    
    private KeyValuePair<int, string> _group = new KeyValuePair<int, string>(1, "Player");

    public override KeyValuePair<int, string> @group => _group;
    private GameManager gameManager;
    private int raysNumber = 50;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        DrawFOVRays();
    }

    public override void Start()
    {
        base.Start();
        gameManager = FindObjectOfType<GameManager>();
    }

    List<TimedTarget> targetsInFov = new List<TimedTarget>();
    private Vector3 CharacterCenter { get => (transform.position + Vector3.up * (characterController.height / 3)); }

    private const float TARGET_VISIBLE_TIME = 2f;
    private void DrawFOVRays()
    {
        float angle = fieldOfViewAngle.Value / 2;
        float resolution = angle * 2 / raysNumber;
        for (var index = 0; index < targetsInFov.Count; index++)
        {
            var timedTarget = targetsInFov[index];
            timedTarget.timeLeft -= Time.fixedDeltaTime;
            if (timedTarget.timeLeft < 0) targetsInFov.Remove(timedTarget);
        }

        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, fieldOfViewRangeDark.Value);
        
        for (int i = 0; i < targetsInRange.Length; i++)
        {
            Transform t = targetsInRange[i].transform;
            if(t == transform) continue;
            if (t.gameObject.GetComponent<Character>() is Character target)
            {

                var distance = (t.position - transform.position).magnitude;
                if (distance > fieldOfViewRangeDark.Value) break;
                var dirToTarget = (t.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, dirToTarget) < angle)
                {
                    if (!Physics.Raycast(new Ray(transform.position, t.position), out var hit, distance))
                    {
                        TimedTarget tt = new TimedTarget(target, TARGET_VISIBLE_TIME);
                        bool exist = false;
                        for (int j = 0; j < targetsInFov.Count; j++)
                        {
                            if (targetsInFov[j] == tt)
                            {
                                targetsInFov[j].timeLeft = TARGET_VISIBLE_TIME;
                                exist = true;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            targetsInFov.Add(tt);
                        }
                    }
                }
            }
        }

        var castedList = targetsInFov.Select(t => t.character);
        for (var index = 0; index < gameManager.CharacterList.Count; index++)
        {
            var characterStatus = gameManager.CharacterList[index];
            if (characterStatus == this) continue;
            
            if (!castedList.Contains(characterStatus))
            {
                if(characterStatus.statusBar != null)
                {
                    characterStatus.statusBar.gameObject.SetActive(false);
                }
            }
        }
    }

    class TimedTarget
    {
        public readonly Character character;
        public float timeLeft;

        public TimedTarget(Character character, float timeVisible)
        {
            this.character = character;
            this.timeLeft = timeVisible;
        }

        public override int GetHashCode()
        {
            return character.GetHashCode();
        }
    }


}