using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage; // 총 데미지

    public float fireRate; // 연사속도
    public float reloadTime; // 장전시간

    public int currentBullet; //현재 총알
    public int reloadBullet; // 장전되는 총알

    public Transform muzzle; //총알의 발사 위치
    public AudioClip audioclip; // 총 사격 사운드
}
