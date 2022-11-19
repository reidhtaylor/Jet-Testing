using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRelay : Target
{
    public Target actualTarget;
    public override Target Get() => actualTarget;
}
