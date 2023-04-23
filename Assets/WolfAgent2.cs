// training loop for wolf agent
// uses Soft Actor-Critic implementation bundled with ML-Agents

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class WolfAgent2 : Agent
{       //multiagent reinforcment learnment
        // Actionable objects
        public int countJoints = 21;
        public Rigidbody[] motors = new Rigidbody[21];

        // Other objects
        public Vector3 controlSignal = Vector3.zero;
        public Transform root;
        public Transform target;

        // Start is called before the first frame update
        public override void Initialize()
        {
                Dictionary<string, int> motorIndex = new Dictionary<string, int> {
                        {"head", 0}, {"corps_first", 1}, {"corps_second", 2},
                        {"corps_third", 3}, {"shoulder_left_front", 4}, {"shoulder_right_front", 5}, 
                        {"forearm_right_front", 6},
                        {"forearm_left_front", 7}, {"knee_right_back", 8}, {"knee_left_back", 9}, {"knee_right_front", 10},
                        {"knee_left_front", 11}, {"ankle_right_back", 12}, {"ankle_right_front", 13},
                        {"ankle_left_back", 14}, {"ankle_right_back", 15}, {"hip_right_back", 16}, {"hip_left_back", 17}, 
                        {"tail_first",18}, {"tail_second",19}, {"tail_third",20}
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
         public override void OnActionReceived(ActionBuffers actions) {
                for (int i = 0; i < countJoints; ++i) {
                        // get torque
                        controlSignal.x = actions.ContinuousActions[3*i];
                        controlSignal.y = actions.ContinuousActions[3*i + 1];
                        controlSignal.z = actions.ContinuousActions[3*i + 2];

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

