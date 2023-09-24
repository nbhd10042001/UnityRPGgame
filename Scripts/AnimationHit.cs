using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHit : MonoBehaviour
{
    [SerializeField] private string nameHit;
    public void ReleaseAnimationHit()
    {
        SpawmManager.Instance.ReleaseHitAnimation(this, nameHit);
    }
}
