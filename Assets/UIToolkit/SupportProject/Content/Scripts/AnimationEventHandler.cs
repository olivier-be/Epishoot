using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public AudioSource FootstepSound;
    public AudioClip[] FootstepClips;
    
    void OnFootstep()
    {
        FootstepSound.PlayOneShot(FootstepClips[Random.Range(0, FootstepClips.Length)]);
    }
}
