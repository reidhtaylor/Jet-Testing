using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetWeapon : MonoBehaviour
{
    public bool firing { get; set; } = false;

    public void Process(bool _firing) {
        firing = _firing;
    }
}
