using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : EnemyAction
{
    public Vector3 ifCenterNull;
    public float minDis = 30f, maxDis = 30f;

    public float stopDistance = 100f;

    public float radi = 10f;

    protected NavMeshAgent nav;
    protected Vector3 dest;
    protected Transform player; //target

    private bool alreadyStart;

    private void Start()
    {
        alreadyStart = true;
        StartCoroutine(Move());
    }

    public override void AwakeReset()
    {
        base.AwakeReset();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if(minDis.Equals(0f) || maxDis.Equals(0f))
            minDis = 30f; maxDis = 30f;
    }

    public override void EnableState()
    {
        base.EnableState();
        if(!alreadyStart) StartCoroutine(Move());
    }

    public override void DisableState()
    {
        StopAllCoroutines();
        base.DisableState();
    }

    protected virtual void SetDestination()
    {
        SetDest1();
        SetDest2();
    }

    protected void SetDest1()
    {
        Vector3 temp = ifCenterNull;
        if (player != null)
            if(player.gameObject.activeSelf)
                temp = player.position;
        float dis = (transform.position - dest).sqrMagnitude;
        while (dis < 50f)
        {
            dest = Lect.RandomPosOnCenter(temp, minDis, maxDis);
            dis = (transform.position - dest).sqrMagnitude;
        }
        dest = new Vector3(dest.x, 0f, dest.z);
    }
    protected void SetDest2()
    {
        NavMeshPath path = new NavMeshPath();
        while (path.status == NavMeshPathStatus.PathComplete)
            nav.CalculatePath(dest, path);
        nav.SetDestination(dest);
    }

    protected virtual IEnumerator Move()
    {
        while (true)
        {
            if (wait)
            {
                nav.SetDestination(transform.position);
                Lect.Rotate(transform, dest, control.rotateSpeed);
            }

            float dis = (transform.position - dest).sqrMagnitude;
            if (dis < stopDistance || !nav.hasPath) SetDestination();

            Lect.Rotate(transform, dest, control.rotateSpeed);

            yield return null;
        }
    }
}
