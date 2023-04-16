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
        public int countJoints = 17;
        public Rigidbody[] motors = new Rigidbody[17];

        // Other objects
        public Vector3 controlSignal = Vector3.zero;
        public Transform root;
        public Transform target;

        // Start is called before the first frame update
        public override void Initialize()
        {
                Dictionary<string, int> motorIndex = new Dictionary<string, int> {
                        {"spine1", 0}, {"spine2", 1}, {"neck", 2},
                        {"clavicle_l", 3}, {"shoulder_l", 4}, {"elbow_l", 5}, {"fheel_l", 6},
                        {"clavicle_r", 7}, {"shoulder_r", 8}, {"elbow_r", 9}, {"fheel_r", 10},
                        {"hip_l", 11}, {"knee_l", 12}, {"bheel_l", 13},
                        {"hip_r", 14}, {"knee_r", 15}, {"bheel_r", 16}
                };

               /*  Rigidbody[] allRigidbodies = GetComponentsInChildren<Rigidbody>();

                foreach (Rigidbody rb in allRigidbodies) {
                        int i = motorIndex[rb.gameObject.name];
                        motors[i] = rb;
                }

                */
                // get target at a random position on the floor 
                target.localPosition = new Vector3(Random.value * 8 - 6, 0.5f, Random.value * 8 - 3.5f);
        }

        public override void OnEpisodeBegin() 
        {
                if (root.localPosition.y < 0) {
                        root.localPosition = new Vector3(0.1f, 0.9f, 0);
                }

                // get target at a random position on the floor 
                target.localPosition = new Vector3(Random.value * 8 - 6, 0.5f, Random.value * 8 - 3.5f);
       
        }
        


        public override void CollectObservations(VectorSensor sensor) 
        {
                // get target's position
                sensor.AddObservation(target.localPosition);

                // get joint positions and rotations
                for (int i = 0; i < countJoints; ++i) {
                        sensor.AddObservation(motors[i].position);
                        sensor.AddObservation(motors[i].rotation);
                }
        }

        // apply output of neural net as torque to each joint
         public virtual void OnActionReceived(float[] vectorAction) {
                for (int i = 0; i < countJoints; ++i) {
                        // get torque
                        controlSignal.x = vectorAction[3*i];
                        controlSignal.y = vectorAction[3*i + 1];
                        controlSignal.z = vectorAction[3*i + 2];
                        // apply it to the motor
                        motors[i].AddTorque(controlSignal);   
                } 

                // Rewards
                float distanceToTarget = Vector3.Distance(root.localPosition, target.localPosition);
                SetReward(Mathf.Exp(-(distanceToTarget*distanceToTarget)));

                if (distanceToTarget < 1.42f) {
                        EndEpisode();
                }

                // if an agent fell off the platform, reset episode
                if (root.localPosition.y < 0) {
                        EndEpisode();
                }
        }
}

