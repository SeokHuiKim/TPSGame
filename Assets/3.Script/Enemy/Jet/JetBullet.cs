using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class JetBullet : EnemyBullet
{
    public Transform fly;

    private NavMeshAgent nav;

    protected override void AwakeReset()
    {
        base.AwakeReset();

        isBreakable = true;

        fly.localPosition = new Vector3(0f, transform.position.y, 0f);
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        nav = GetComponent<NavMeshAgent>();
        nav.speed = moveSpeed;
        nav.angularSpeed = rotateSpeed;
        nav.enabled = true;
    }

    protected override void Look() { }
    protected override void Move()
    {
        float val = moveSpeed * Time.fixedDeltaTime;
        if (player.position.y < fly.position.y) val *= -1f;
        fly.position = new Vector3(0f, fly.position.y + val, 0f);

        nav.SetDestination(player.position);
    }
}
