using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isGood = false;

    public virtual void OnInitialize(bool _isGood) {
        isGood = _isGood;
    }

    public virtual Target Get() => this;
}
