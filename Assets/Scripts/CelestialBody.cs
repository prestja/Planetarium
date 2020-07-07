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
    [SerializeField] private float eccentricity;

    private float MeanMotion {
        get {
            return (2 * Mathf.PI) / orbitalPeriod;
        }
    }

    private float MeanAnomaly(float t) {
        return m0 + MeanMotion * (t - t0);
    }

    private float EccentricAnomaly(float t) {
        float anomaly = eccentricity < 0.08 ? MeanAnomaly(t) : Mathf.PI;
        float anomalyPrev = (MeanAnomaly(t) + Mathf.PI) * 4.0f;

        int itr = 0;
        while (Mathf.Abs(anomaly - anomalyPrev) > 1E-12) {
            itr++;
            if (itr > 1000)
                break;
            anomalyPrev = anomaly;
            anomaly = anomaly -
                (anomaly - eccentricity * Mathf.Sin(anomaly) - MeanAnomaly(t)) /
                (1 - eccentricity * Mathf.Cos(anomaly));
        }
        return anomaly;
    }

    private float TrueAnomaly(float t) {
        float e2 = EccentricAnomaly(t) / 2.0f;
        float t1 = Mathf.Sqrt(1 + eccentricity);
        float t2 = Mathf.Sqrt(1 - eccentricity);
        return 2.0f * Mathf.Atan2(t1 * Mathf.Sin(e2), t2 * Mathf.Cos(e2));
    }

    private void Update() {
        if (!parent) // stars or barycenters should not appear to move
            return;
        //Debug.Log(MeanAnomaly(Time.time));
        //Debug.Log(EccentricAnomaly(Time.time));
    }

    private void OnGUI() {
        if (!parent)
            return;
        GUI.Label(new Rect(0, 0, 200, 40), "Mean anomaly: " + MeanAnomaly(Time.time));
        GUI.Label(new Rect(0, 20, 200, 40), "Eccentric anomaly: " + EccentricAnomaly(Time.time));
        GUI.Label(new Rect(0, 40, 200, 40), "True anomaly: " + TrueAnomaly(Time.time));
    }
}
