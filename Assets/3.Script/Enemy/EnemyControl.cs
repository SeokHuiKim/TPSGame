using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    Attack,
    Move
}

public class EnemyControl : MonoBehaviour
{
    public float rotateSpeed = 0.2f; //Jet0.5, Tank0.2

    public int maxHealth;
    private int currentHealth;

    [HideInInspector]
    public bool attacking;
    [HideInInspector]
    public EnemyAnim anim;
    [HideInInspector]
    public Transform player;

    protected Dictionary<EnemyState, EnemyAction> states = 
        new Dictionary<EnemyState, EnemyAction>();

    public void Damage(int _value)
    {
        currentHealth -= _value;
        if(currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        StopAllCoroutines();
        foreach (EnemyAction s in states.Values)
        {
            s.StopAllCoroutines();
        }
        Destroy(gameObject);
    }

    protected void ResetState(EnemyAction attack, EnemyAction move)
    {
        states.Add(EnemyState.Attack, attack);
        states.Add(EnemyState.Move, move);
    }

    private void Start()
    {
        anim = GetComponent<EnemyAnim>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartSet();
        currentHealth = maxHealth;
    }

    public virtual void StartSet()
    {
        SetState(EnemyState.Move);
    }

    public virtual void SetState(EnemyState state)
    {
        foreach (EnemyAction s in states.Values)
            s.DisableState();

        states[state].enabled = true;
        states[state].EnableState();
    }

    public void WaitMove(bool b)
    {
        states[EnemyState.Move].wait = b;
    }
}
