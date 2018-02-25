// AREA.ML.NN - AREA.ML.NN - Perceptron.cs
// Created at: 2018 02 25 下午 11:34
// Updated At: 2018 02 26 上午 12:54
// By: Furion Mashiou

using System;
using UnityEngine;

[Serializable]
public class TrainingSet {

    #region Fields

    public double[] input;
    public double output;

    #endregion

}

public class Perceptron : MonoBehaviour {

    #region Fields

    public TrainingSet[] ts;
    private double[] weights = { 0, 0 };
    private double bias;
    private double totalError;

    #endregion

    #region Methods

    private double dot(double[] v1, double[] v2) {
        if (v1 == null || v2 == null)
            return -1;
        if (v1.Length != v2.Length)
            return -1;

        double d = 0;
        for (var x = 0; x < v1.Length; x++) {
            d += v1[x] * v2[x];
        }
        return d + bias;
    }

    private void initWeights() {
        for (var i = 0; i < weights.Length; i++) {
            weights[i] = UnityEngine.Random.Range(-1.0f, 1.0f);
        }
        bias = UnityEngine.Random.Range(-1.0f, 1.0f);
    }

    private void train(int epochs) {
        initWeights();
        for (var e = 0; e < epochs; e++) {
            totalError = 0;
            for (var t = 0; t < ts.Length; t++) {
                updateWeights(t);
                Debug.Log(string.Format("W1:{0} W2:{1} B:{2}", weights[0], weights[1], bias));
            }
            Debug.Log("TOTAL ERROR: " + totalError);
        }
    }

    double calcOutput(double[] data) {
        if (dot(weights, data) > 0) {
            return 1;
        }
        return 0;
    }

    private double activate(int i) {
        if (dot(weights, ts[i].input) > 0) {
            return 1;
        }
        return 0;
    }

    private void updateWeights(int j) {
        double error = ts[j].output - activate(j);
        totalError += Mathf.Abs((float)error);
        for (var i = 0; i < weights.Length; i++) {
            weights[i] = weights[i] + error * ts[j].input[i];
        }
        bias += error;
    }

    private void Start() {
        // run epoch
        train(8);

        // test it 
        Debug.Log(calcOutput(new double[] { 0, 0 }));
        Debug.Log(calcOutput(new double[] { 1, 0 }));
        Debug.Log(calcOutput(new double[] { 0, 1 }));
        Debug.Log(calcOutput(new double[] { 1, 1 }));
    }

    #endregion

};