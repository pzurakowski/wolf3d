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
        int countJoints = 17;
        Rigidbody[] motors[countJoints];

        // Other objects
        int controlSignal = Vector3.zero;

        // Start is called before the first frame update
        public override void Start()
        {
                Dictionary<string, int> motorIndex = new Dictionary<string, int> {
                        {"spine1", 0}, {"spine2", 1}, {"neck", 2},
                        {"clavicle_l", 3}, {"shoulder_l", 4}, {"elbow_l", 5}, {"fheel_l", 6},
                        {"clavicle_r", 7}, {"shoulder_r", 8}, {"elbow_r", 9}, {"fheel_r", 10},
                        {"hip_l", 11}, {"knee_l", 12}, {"bheel_l", 13},
                        {"hip_r", 14}, {"knee_r", 15}, {"bheel_r", 16}
                }

                Rigidbody[] allRigidbodies = GetComponentsInChildren<Rigidbody>();

                foreach (Rigidbody rb in allRigidbodies) {
                        int i = motorIndex[rb.gameObject.name];
                        motors[i] = rb;
                }
        }

        public override void onEpisodeBegin() 
        {
                // TODO need to code in constraints:
                //
                // if wolf falls off the map, reset his position and velocity, get Target into a random position 
        }
        


        void CollectObservations(VectorSensor sensor) 
        {
                // TODO need to collect obs and push them on to Sensor
                //
        }

        // apply output of neural net as torque to each joint
        void OnActionReceived(float[] vectorAction) 
        {
                for (int i = 0; i < countJoints; ++i) {
                        // get torque
                        controlSignal.x = vectorAction[3*i];
                        controlSignal.y = vectorAction[3*i + 1];
                        controlSignal.z = vectorAction[3*i + 2];
                        // apply it to the motor
                        motors[i].AddTorque(controlSignal);             // I wonder if the fact that
                                                                          // we're applying torques 
                                                                          // asynchronously will actually be visible
                                                                          // in the simulation?
                } 
        }
}
