using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJetMachine : AI
{
    public AIJetState state;

    public Rigidbody rb;
    public MeshRenderer mesh;
    public Material enemyMaterialOverride;

    [Header("Thrust")]
    public float glideSpeed = 80;
    
    [Header("Wander")]
    public float wanderVerticalStrength = 1;
    public float wanderVerticalSpeed = 0.3f;
    public float wanderHorizontalStrength = 1;
    public float wanderHorizontalSpeed = 0.3f;

    [Header("Pursue")]
    public float pursueSpeed = 80;
    
    [Header("Course Correction")]
    [Range(0, 1)]
    public float correctionPrecision = 0.7f;
    public float verticalCorrectionSpeed = 25;
    public float horizontalCorrectionSmoothing = 7;

    [Header("Obstacle Avoidance")]
    public EyeCatcher verticalCatcher;
    public float avoidVerticalSpeed = 4;
    public EyeCatcher horizontalCatcher;
    public float avoidHorizontalSpeed = 4;

    public bool isCrashing { get; set; } = false;
    public static Material enemyMat;

    public List<Target> targets { get; set; } = new List<Target>();
    public delegate void OnTargetAdded(Target t);
    public OnTargetAdded onTargetAdded;

    void Start() {        
        state = new AIJetWanderState(this);
        state.FirstState();
    }

    void Update() {
        state.Process();
    }

    public override void OnInitialize(bool isGood) {
        base.OnInitialize(isGood);
        if (!isGood) {
            if (enemyMat == null) enemyMat = enemyMaterialOverride != null ? Object.Instantiate(enemyMaterialOverride) : mesh.material;
            mesh.material = enemyMat;
        }
    }


    // public void OnTriggerEnter(Collider col) {
    //     Target t = col.GetComponent<Target>();
    //     if (t != null && !targets.Contains(t.Get())) {
    //         targets.Add(t.Get());
    //         onTargetAdded?.Invoke(t.Get());
    //     }
    // }
    // public void OnTriggerExit(Collider col) {
    //     Target t = col.GetComponent<Target>();
    //     if (t != null && targets.Contains(t.Get())) {
    //         targets.Remove(t.Get());
    //     }
    // }
}
