using UnityEngine;
using System.Collections;

public class DifferentAnimatorDeath : MonoBehaviour {
    public Animator remoteAnimator;
    public Animation animationToPlay;
    public void Die()
    {
        if(remoteAnimator != null && animationToPlay != null)
        {
            remoteAnimator.Play(animationToPlay.name);
        }
    }
}
