using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName= "New FootstepCollection", menuName = "Create New Footstep Collection")]
public class FootstepCollection : ScriptableObject
{
    public List<AudioClip> footstepSounds = new List<AudioClip>();
}
