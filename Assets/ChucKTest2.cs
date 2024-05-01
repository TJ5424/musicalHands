using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;

public class ChucKTest2 : MonoBehaviour
{
    HandVisualizer handVisualizer; // Add a reference to the HandVisualizer class

    // Start is called before the first frame update
    void Start()
    {
        // Find the HandVisualizer component in the scene
        handVisualizer = FindObjectOfType<HandVisualizer>();
        if (handVisualizer == null)
        {
            Debug.LogError("HandVisualizer component not found in the scene.");
            return;
        }

        // Your ChucK code execution remains the same
        GetComponent<ChuckSubInstance>().RunCode(@"
            440 => global float InFreq;
            SinOsc s => dac;
            while( true )
            {
                0.2 => s.gain;
                InFreq => s.freq;
                10::ms => now;
            }
        ");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if handVisualizer is null to avoid errors
        if (handVisualizer == null)
            return;

        // Access the xAxisPosition property from the HandVisualizer class
        float position = handVisualizer.xAxisPositionR;

        // Scale these values to an appropriate range for frequency and send to ChucK
        GetComponent<ChuckSubInstance>().SetFloat("InFreq", position * 1000);
    }
}
