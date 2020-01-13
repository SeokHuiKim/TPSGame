using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//정리 필요함
public class BossFunnel : MonoBehaviour
{
    public Transform funnelsParent;
    public LineRenderer line;

    private int initialHealth;
    private float recovertyTimePer1Health;

    private float minDis, maxDis;

    private float moveSpeed, rotateSpeed;
    private float funnelCycle;
    private float yInterval;
    private float groundY, skyY;

    private int attackCount;
    private float attackCycle;
    private float attackSpeed;

    private int damage;

    [HideInInspector]
    public Transform boss;

    private Transform player, super;
    private Vector3 position;
    private Quaternion rot;
    private BoxCollider boxCollider;
    private Rigidbody rigid;

    private int currentHealth;
    private bool die;


    public PlayerHit playerHit;
    public PlayerHUD playerHUD;

    public void InitialVariable(int _initialHealth, float _recovertyTimePer1Health, float _maxDis, float _minDis,
        float _moveSpeed, float _rotateSpeed, float _funnelCycle, float _yInterval,
        float _groundY, float _skyY, int _attackCount, float _attackCycle, float _attackSpeed, int _damage)
    {
        initialHealth = _initialHealth;
        recovertyTimePer1Health = _recovertyTimePer1Health;
        maxDis = _maxDis;
        minDis = _minDis;
        moveSpeed = _moveSpeed;
        rotateSpeed = _rotateSpeed;
        funnelCycle = _funnelCycle;
        yInterval = _yInterval;
        groundY = _groundY;
        skyY = _skyY;
        attackCount = _attackCount;
        attackCycle = _attackCycle;
        attackSpeed = _attackSpeed;
        damage = _damage;

        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        line.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider = GetComponent<BoxCollider>();
        super = transform.parent;
        position = transform.localPosition;
        rot = transform.rotation;
        currentHealth = initialHealth;

        StartCoroutine(Follow());
    }

    private void Start() { }

    private IEnumerator Follow()
    {
        boxCollider.enabled = false;
        rigid.isKinematic = true;
        transform.parent = super;

        //Rotate
        float rotateTime = 1f;
        while (rotateTime > 0f)
        {
            if (transform.rotation.Equals(rot)) break;
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotateSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        transform.rotation = rot;

        //Follow
        while (true)
        {
            while (boss.Equals(null)) yield return new WaitForSeconds(1.0f);
            transform.position = Vector3.Lerp(transform.position, position, moveSpeed * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
        }
    }

    public bool GetIsAlive()
    {
        if(die)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void Damage(int _damage)
    {
        currentHealth -= _damage;
        if (currentHealth <= 0)
        {
            line.gameObject.SetActive(false);
            die = true;
            Action(false);
        }
    }

    /// <summary>true일 경우 추격 및 공격, false일 경우 장착 이동</summary>
    public void Action(bool isAttack)
    {
        StopAllCoroutines();
        if (isAttack)
        {
            rigid.isKinematic = false;
            boxCollider.enabled = true;
            StartCoroutine(Rotate());
            StartCoroutine(Cycle());
            StartCoroutine(Shoot());
        }
        else
        {
            rigid.useGravity = true;
            rigid.isKinematic = false;
        }
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            Lect.Rotate(transform, player.position, rotateSpeed);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Cycle()
    {
        transform.parent = funnelsParent;

        while (true)
        {
            Vector3 pos = Lect.RandomPosOnCenter(player.position, minDis, maxDis);
            float _y = 0f;
            _y = Random.Range(player.position.y - yInterval, player.position.y + yInterval);
            if (_y < groundY) _y = groundY;
            else if (_y > skyY) _y = skyY;
            pos.y = _y;

            float val = 0f;
            while (val < funnelCycle)
            {
                transform.position = Vector3.Lerp(transform.position, pos, moveSpeed * Time.fixedDeltaTime);

                val += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            int count = 0;
            while (count < attackCount)
            {
                count++;

                Vector3 dest = Lect.RandomPosOnCenter(player.position, 0f, 2f);

                line.enabled = true;
                line.SetPosition(1, dest + Vector3.up * 5f);
                
                if (Vector3.Distance(player.position, dest) < 0.5f)
                {
                    //맞았을 경우 대미지
                    if (!playerHit.IsInvincible)
                    {
                        playerHUD.DecreaseHp(damage);
                        //playerHit.FrontBackCheck(player, transform);
                        //playerHit.GunHit(1);
                        if (Flat.Instance.GetGround()) playerHit.BeHitGun();
                        else playerHit.BeHitAir();
                    }
                }

                float val2 = 1f;
                while (val2 > 0f)
                {
                    line.SetPosition(0, transform.position);

                    val2 -= Time.fixedDeltaTime * attackSpeed;
                    line.startWidth = val2;
                    line.endWidth = val2;

                    yield return new WaitForFixedUpdate();
                }
                line.enabled = false;
            }

            yield return new WaitForSeconds(Mathf.Abs(attackCycle));
        }
    }
}


/*
 * private IEnumerator RecoveryHealth()
    {
        while (currentHealth < maxHealth)
        {
            float time = 0f;
            while (time < recovertyTimePer1Health)
            {
                time += Time.fixedDeltaTime;
                yield return null;
            }

            currentHealth++;
        }

        die = false;
    }
    private IEnumerator Follow()
    {
        transform.parent = super;

        //Rotate
        float rotateTime = 1f;
        while (rotateTime > 0f)
        {
            if (transform.rotation.Equals(rot)) break;
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotateSpeed * Time.fixedDeltaTime);
            yield return null;
        }
        transform.rotation = rot;

        //Follow
        while (true)
        {
            while (boss.Equals(null)) yield return new WaitForSeconds(1.0f);
            transform.position = Vector3.Lerp(transform.position, position, moveSpeed * Time.fixedDeltaTime);

            yield return null;
        }
    }
 */
