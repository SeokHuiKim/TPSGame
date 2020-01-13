using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : BossAction
{
    public Transform[] movePositions;

    public float shortDistance = 25f, longDistance = 50f;

    public float distanceBetweenDestination = 5f;

    [Tooltip("초기 최대치, 잔여 100 이상일때 무조건 부스트 사용")]
    public float boostInitialValue;
    public float boostMag = 2f;

    private Vector3 currentDestination, previousDestination;

    private float maxBoostValue, currentBoostValue;
    private Transform player;

    private void Start() {
        maxBoostValue = currentBoostValue = boostInitialValue;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public override void Action(bool _isGoClose)
    {
        control.NavControl(true);
        bool boost = false;

        Vector3 destination = transform.position;
        if (_isGoClose)
        {
            if(currentBoostValue >= 10f)
            {
                boost = true;
                Boost(boost);
            }
            else
            {
                Boost(boost);
            }
            destination = RandomNavmeshLocation(shortDistance);//GetClosePosition();
        }
        else
        {
            boost = true;
            Boost(boost);
            destination = RandomNavmeshLocation(longDistance);//GetFarPosition();
        }

        StartCoroutine(WaitForClose(destination, boost));
    }

    public Vector3 RandomNavmeshLocation(float _radius)
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        Vector3 randomDirection = Lect.RandomPosOnCenter(player.position, _radius, _radius);
        Vector3 finalPosition = GetClosePosition();
        randomDirection += player.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, _radius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    /// <param name="_enable">true일때 부스트, false일때 걷기</param>
    private void Boost(bool _enable)
    {
        if (_enable && !control.GetSally())
        {
            anim.Play(AnimState.Boost);
            control.NavSpeed(true, boostMag);
        }
        else
        {
            anim.Play(AnimState.Walk);
            control.NavSpeed(false, 0f);
        }
    }

    public void DecreaseBoostValue(float _value)
    {
        if (currentBoostValue - _value > 0)
        {
            currentBoostValue -= _value;
        }
        else
        {
            currentBoostValue = 0f;
        }
    }

    public void IncreaseBoostValue(float _value)
    {
        if(currentBoostValue + _value >= maxBoostValue)
        {
            currentBoostValue = maxBoostValue;
        }
        else
        {
            currentBoostValue += _value;
        }
    }

    private IEnumerator WaitForClose(Vector3 _destination, bool _boost)
    {
        float cantwait = 3f;
        control.SetNav(_destination);

        // 움직이길 기다림
        while (!control.IsNavMoving())
        {
            cantwait -= Time.fixedDeltaTime;
            if(cantwait <= 0f)
            {
                break;
            }
            anim.Play(AnimState.WalkStop);
            anim.Play(AnimState.BoostStop);
            yield return new WaitForFixedUpdate();
        }

        if (_boost)
        {
            anim.Play(AnimState.Boost);
        }
        else
        {
            anim.Play(AnimState.Walk);
        }

        // 멈추길 기다림
        while (control.IsNavMoving())
        {
            yield return new WaitForFixedUpdate();
        }

        anim.Play(AnimState.WalkStop);
        anim.Play(AnimState.BoostStop);

        control.NavControl(false);
        control.ActionControl(BossActions.Move, false);
    }    

    /// <summary>
    /// 가장 가까운 위치를 측정해서 반환함
    /// </summary>
    private Vector3 GetClosePosition()
    {
        int positionNumber = 0;

        Vector3 firstLow = movePositions[0].position;
        if (firstLow.Equals(currentDestination))
        {
            firstLow = movePositions[1].position;
        }

        float lowDistance = Lect.Distance(transform.position, firstLow);
        for(int i = 1; i < movePositions.Length; i++)
        {
            if (movePositions[i].position.Equals(currentDestination) ||
                movePositions[i].position.Equals(previousDestination))
            {
                continue;
            }

            float currentDistance = Lect.Distance(transform.position, movePositions[i].position);
            if(lowDistance > currentDistance)
            {
                positionNumber = i;
                lowDistance = currentDistance;
            }
        }

        previousDestination = currentDestination;
        currentDestination = movePositions[positionNumber].position;
        return currentDestination;
    }

    /// <summary>
    /// 가장 먼 위치를 측정해서 반환함
    /// </summary>
    private Vector3 GetFarPosition()
    {
        int positionNumber = 0;

        Vector3 firstHigh = movePositions[0].position;
        if (firstHigh.Equals(currentDestination))
        {
            firstHigh = movePositions[1].position;
        }

        float HighDistance = Lect.Distance(transform.position, firstHigh);
        for (int i = 1; i < movePositions.Length; i++)
        {
            if (movePositions[i].position.Equals(currentDestination) ||
                movePositions[i].position.Equals(previousDestination))
            {
                continue;
            }

            float currentDistance = Lect.Distance(transform.position, movePositions[i].position);
            if (HighDistance < currentDistance)
            {
                positionNumber = i;
                HighDistance = currentDistance;
            }
        }

        currentDestination = movePositions[positionNumber].position;
        return currentDestination;
    }

    
}
