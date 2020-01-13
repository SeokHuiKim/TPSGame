using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : EnemyBullet
{
    private Vector3 playerPos;

    protected override void AwakeReset()
    {
        base.AwakeReset();

        isBreakable = true;
        playerPos = player.position;
    }

    protected override void Move()
    {
        Vector3 dir = playerPos - transform.position;
        float dis = Vector3.SqrMagnitude(dir);
        transform.position += dir.normalized * moveSpeed * Time.fixedDeltaTime;    
    }
}
