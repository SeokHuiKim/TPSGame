using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingame : MonoBehaviour
{
    public AudioClip gameScreen;

    private void Awake()
    {
        SoundManager.Instance.PlayBGM(gameScreen);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Gun"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Gun"), LayerMask.NameToLayer("Gun"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("CloseWeapon"), true);
    }
}
