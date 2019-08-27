using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeEnemy : EnemyBase
{
    public MeleeEnemy(int hp, int mp) : base(hp, mp)
    {
    }

    public override void BaseAttack()
    {
        LogSystem.LogError("Player's attacked");
    }

    public override void Move()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    private Transform player;
    [SerializeField]
    private LayerMask layerBarrier;
    [SerializeField]
    private float distanceToSwitchLand;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Move();

        GizmoManager.DrawLine(transform.position, transform.position + Vector3.down * distanceToSwitchLand);
        if(canSwitchLand && Time.frameCount % 10 == 0)
            if (NearBarrier())
            {
                //TODO: left or right
                var randDirect = Random.Range(0, 100) / 50;

                MoveAround((MoveAroundDirect)randDirect);
            }
    }

    private bool NearBarrier()
    {
        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(transform.position, Vector2.down, distanceToSwitchLand, layerBarrier))
        {
            LogSystem.LogWarning(hit.transform.name);

            return true;
        }

        return false;
    }
}