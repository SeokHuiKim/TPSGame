using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(JetControl))]
public class JetMove : EnemyMove
{
    public Transform fly;
    public float minY, maxY, flySpeed;
    public float minUpdownCycle, maxUpdownCycle;

    private bool alwaysMove;

    public override void EnableState()
    {
        if (alwaysMove) return;
        alwaysMove = true;

        base.EnableState();
        StartCoroutine(Fly());
    }

    private IEnumerator Fly()
    {
        float y = maxY;
        float spanTime = 0f;
        float cycle = Random.Range(minUpdownCycle, maxUpdownCycle);
        bool up = false;

        while (true)
        {
            spanTime += Time.deltaTime;
            if (spanTime > cycle)
            {
                up = !up;
                spanTime = 0f;
                cycle = Random.Range(minUpdownCycle, maxUpdownCycle);
            }

            if (y < minY) y = minY;
            else if (y > maxY) y = maxY;
            else
            {
                if (up) y += flySpeed * Time.fixedDeltaTime;
                else y -= flySpeed * Time.fixedDeltaTime;
            }
            fly.localPosition = new Vector3(0f, y, 0f);

            yield return null;
        }
    }

    protected override IEnumerator Move()
    {
        while (true)
        {
            if (wait)
            {
                float x = Mathf.Cos(Mathf.Deg2Rad * 45f) * radi;
                float y = Mathf.Sin(Mathf.Deg2Rad * 45f) * radi;
                Vector3 offset = new Vector3(x, 0, y);

                dest = transform.position + offset;
                SetDest2();

                yield return new WaitForSeconds(1f);
            }

            float dis = (transform.position - dest).sqrMagnitude;
            if (dis < stopDistance || !nav.hasPath) SetDestination();

            Lect.Rotate(transform, dest, control.rotateSpeed);

            yield return null;
        }
    }
}
