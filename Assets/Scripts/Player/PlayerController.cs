using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Target
{
    [Header("Thrust")]
    public float glideSpeed = 80;
    [Range(0, 1)]
    public float brakeMultiplier = 0.35f;
    public float thrustGain = 40;
    public float thrustAcceleration = 15;

    [Header("Lift")]
    public float liftPower = 0.5f;
    public float liftAcceleration = 4;

    [Header("Steer")]
    public float steerPower = 0.5f;
    public float steerAcceleration = 4;

    [Header("Roll")]
    public float rollPower = 1.5f;
    public float rollAcceleration = 4;

    [Header("Die")]
    public GameObject explosionEffect;
    public float explosionShakePower = 2;
    public float explosionShakeLength = 3;

    [Header("Display")]
    public Transform display;
    public Vector3 rightTiltAngles;
    public Vector3 initialAngles;
    public Vector3 leftTiltAngles;

    [Header("Camera")]
    public Transform cameraLookPoint;
    public float verticalLookAheadDistance = 5;
    public float horizontalLookAheadDistance = 10;
    public PlayerCameraShake shaker;

    private float thrust = 0;
    private float liftControl = 0;
    private float steerControl = 0;
    private float rollControl = 0;
    
    private Rigidbody rb;
    private JetInputHandler inputHandler;

    void Start() {
        rb = GetComponent<Rigidbody>();
        inputHandler = GetComponent<JetInputHandler>();
    }

    void Update() {
        // float toFullSpeed = Mathf.Clamp(thrust / (glideSpeed * 0.7f), 0, 1);
        float toFullSpeed = 1;

        float targetThrust = glideSpeed;
        if (inputHandler.thrustValue > 0) targetThrust += thrustGain * inputHandler.thrustValue;
        else if (inputHandler.thrustValue < 0) targetThrust = glideSpeed * brakeMultiplier * Mathf.Abs(inputHandler.thrustValue);

        thrust = Mathf.Lerp(thrust, targetThrust, thrustAcceleration * Time.deltaTime);
        liftControl = Mathf.Lerp(liftControl, inputHandler.liftValue, liftAcceleration * Time.deltaTime) * toFullSpeed;
        steerControl = Mathf.Lerp(steerControl, inputHandler.steerValue, steerAcceleration * Time.deltaTime) * toFullSpeed;
        rollControl = Mathf.Lerp(rollControl, inputHandler.rollValue, rollAcceleration * Time.deltaTime);

        rb.velocity = transform.forward * thrust;
        rb.angularVelocity = (transform.right * liftPower * liftControl) + (transform.up * steerPower * steerControl) + (transform.forward * -rollControl * rollPower);

        cameraLookPoint.localPosition = Vector3.zero + (Vector3.up * -liftControl * verticalLookAheadDistance) + (Vector3.right * steerControl * horizontalLookAheadDistance);
        display.localEulerAngles = Lerp3(leftTiltAngles, initialAngles, rightTiltAngles, (steerControl + 1) / 2);
    }

    public void Die() {
        shaker.Shake(explosionShakePower, 1, explosionShakeLength);
        gameObject.SetActive(false);
        Instantiate(explosionEffect, transform.position, Random.rotation);
        rb.isKinematic = true;

        if (MapBounds.instance == null) {
            Debug.Log("No MapBounds instance exists which is used to start Coroutines on this Object", this);
            return;
        }
        MapBounds.instance.StartCoroutine(Respawn(3));
    }
    private IEnumerator Respawn(float delay = 3) {
        yield return new WaitForSeconds(delay);

        transform.position = MapBounds.RandomPoint();
        transform.rotation = Quaternion.LookRotation(Vector3.forward);

        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        gameObject.SetActive(true);
    }

    Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float t) {
        if (t == 0.5f) return b;
        else if (t > 0.5f) return Vector3.Lerp(b, c, (t - 0.5f) * 2);
        else return Vector3.Lerp(a, b, t * 2);
    }
}
