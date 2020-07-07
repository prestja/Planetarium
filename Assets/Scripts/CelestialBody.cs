using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour {
    [SerializeField] private CelestialBody parent;
    // classical parameters
    [SerializeField] private float t0; // temporal offset
    [SerializeField] private float m0; // mean motion at t0
    [SerializeField] private float semiMajorAxis;
    [SerializeField] private float orbitalPeriod;
    
    private float MeanMotion {
        get {
            return (2 * Mathf.PI) / orbitalPeriod;
        }
    }

    private float MeanAnomaly(float t) {
        return m0 + MeanMotion * (t - t0);
    }

    private void Update() {
        if (!parent) // stars or barycenters should not appear to move
            return;
        Debug.Log(MeanAnomaly(Time.time));
    }
}
