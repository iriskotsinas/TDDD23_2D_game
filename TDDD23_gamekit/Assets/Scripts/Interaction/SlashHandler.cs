using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashHandler : MonoBehaviour
{
    [SerializeField] TrailRenderer SlashTrail;

    public void ShowSlash()
    {
        SlashTrail.emitting = true;
    }

    public void HideSlash()
    {
        SlashTrail.emitting = false;
    }
}