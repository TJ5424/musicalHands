using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;

public class ChucKTest : MonoBehaviour
{
    HandVisualizer handVisualizer;

    float[] quantizedScale = { 261.63f, 293.66f, 329.63f, 349.23f, 392.00f, 440.00f, 493.88f, 523.25f };

    void Start()
    {
        handVisualizer = FindObjectOfType<HandVisualizer>();
        if (handVisualizer == null)
        {
            Debug.LogError("HandVisualizer components not assigned for both hands.");
            return;
        }

        GetComponent<ChuckSubInstance>().RunCode(@"
            440 => global float InFreqA;
            440 => global float InFreqB;
            SinOsc s => dac;
            SinOsc t => dac;
            0.2 => s.gain;
            0.2 => t.gain;
            while (true)
            {
                10::ms => now;
                InFreqA => s.freq;
                InFreqB => t.freq;
            }
        ");
    }

    void Update()
    {
        if (handVisualizer == null)
            return;

        float rightPosition = handVisualizer.xAxisPositionR;
        float frequencyA = MapToQuantizedFrequency(rightPosition * 2000);
        GetComponent<ChuckSubInstance>().SetFloat("InFreqA", frequencyA);

        float leftPosition = handVisualizer.xAxisPositionL;
        float frequencyB = MapToQuantizedFrequency(leftPosition * 2000);
        GetComponent<ChuckSubInstance>().SetFloat("InFreqB", frequencyB);
    }

    float MapToQuantizedFrequency(float frequency)
    {
        float minDistance = Mathf.Infinity;
        float closestFrequency = 0f;

        foreach (float note in quantizedScale)
        {
            float distance = Mathf.Abs(note - frequency);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestFrequency = note;
            }
        }

        return closestFrequency;
    }
}
