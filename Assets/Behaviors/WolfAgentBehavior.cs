// training loop for wolf agent
// uses Soft Actor-Critic implementation bundled with ML-Agents

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class WolfAgent : Agent
{

        // Actionable objects
        ConfigurableJoint hip_l;
        ConfigurableJoint hip_r;
        ConfigurableJoint knee_l;
        ConfigurableJoint knee_r;
        ConfigurableJoint bheel_l;
        ConfigurableJoint bheel_r;
        ConfigurableJoint spine1;
        ConfigurableJoint spine2;
        ConfigurableJoint clavicle_l;
        ConfigurableJoint clavicle_r;
        ConfigurableJoint shoulder_l;
        ConfigurableJoint shoulder_r;
        ConfigurableJoint elbow_l;
        ConfigurableJoint elbow_r;
        ConfigurableJoint fheel_l;
        ConfigurableJoint fheel_r;
        ConfigurableJoint neck;
        
    // Start is called before the first frame update
        void Start()
        {
                // get objects that are part of the learning process
                hip_l = GetComponent<
        }

        void onEpisodeBegin() 
        {


    void CollectObservations(VectorSensor sensor) 
    {

    }

    void OnActionReceived(float[] vectorAction) 
    {

    }

}
