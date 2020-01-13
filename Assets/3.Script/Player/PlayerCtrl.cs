using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // 이동 변화값
    public float moveSpeed;
    public float dashSpeed;
    private float applySpeed;
    public float jumpUpForce;
    public float jumpDownForce;

    [Tooltip("공중유지 낙하속도")]
    public float minFallSpeed = 20f;

    // 행동에 따른 상태변수
    private bool isStop;

    // 캐릭터 회전 민감도
    public float charLookSensitivity;

    public GameObject booster1, booster2;

    private Rigidbody theRid;
    public PlayerHUD theHUD;

    void Start()
    {
        theRid = GetComponent<Rigidbody>();
        applySpeed = moveSpeed;
    }

    void Update()
    {
        
        SpeedChange();
    }

    void FixedUpdate()
    {
        //Jump();
        Move();
        CharacterRotation();
    }

    private void Move()
    {
        if (!isStop)
        {
            Vector3 horizontal = transform.right * Input.GetAxisRaw("Horizontal");
            Vector3 vertical = transform.forward * Input.GetAxisRaw("Vertical");
            Vector3 move = (horizontal + vertical).normalized * applySpeed;
            theRid.MovePosition(transform.position + move * Time.fixedDeltaTime);
        }
    }

    private void SpeedChange()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && theHUD.CurrentSp() > 0)
        {
            applySpeed = dashSpeed;
            booster1.SetActive(true);
            booster2.SetActive(true);
            theHUD.DecreaseSp(3);
        }
        else
        {
            booster1.SetActive(false);
            booster2.SetActive(false);
            applySpeed = moveSpeed;
        }
    }

    public void Fall(bool _faster)
    {
        float spd = jumpDownForce;
        if (_faster)
        {
            spd *= 2f;
        }
        theRid.AddForce(Vector3.down * spd, ForceMode.Impulse);
    }

    private void Jump()
    {
        if ((Input.GetKey(KeyCode.Space) && theHUD.CurrentSp() > 0))
        {
            theHUD.DecreaseSp(3);
            theRid.AddForce(Vector3.up * jumpUpForce, ForceMode.Impulse);
        }
        else
        {
            theRid.AddForce(Vector3.down * jumpDownForce, ForceMode.Impulse);
        }
    }

    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0, yRotation, 0) * charLookSensitivity;
        theRid.MoveRotation(theRid.rotation * Quaternion.Euler(characterRotationY));
    }

    public void SetStop(bool _stop) { isStop = _stop; }
}
