using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    private JetInputHandler input;
    private JetWeapon[] weapons;

    void Start() {
        input = GetComponent<JetInputHandler>();
        weapons = GetComponentsInChildren<JetWeapon>(true);
    }

    void Update() {
        for (int i = 0; i < weapons.Length; i++) weapons[i].Process(input.firing);
    }
}
