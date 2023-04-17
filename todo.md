#### Technical
 - [ ] the script `WolfAgent.cs` (in Assets) needs to be debugged so that it doesn't spit out errors during training.
 - [ ] the script should be so that whenever the wolf noclips from the platform, it gets moved back to the place of origin. That part *doesn't currently work*. 
 - [ ] when starting up the training process, I (Maciek) get an error after a ~minute: ![[Pasted image 20230417181924.png]]
 If others get this error as well, it needs to be resolved. 
  - [ ] Think about a more lightweight ragdoll, so that the model doesn't have such a hard time working with physics

#### RL
 - [ ] need to figure out how to enable Soft Actor-Critic. Supposedly ML-Agents has an
 - [ ] need to make *double extra extra sure* that the learning process is working as it's supposed to:
	- [ ] what should be the settings for `BehaviorParameters` component of `WolfAgent`?
	- [ ] are there any components we should implement? [The docs](https://docs.unity3d.com/Packages/com.unity.ml-agents@1.0/api/Unity.MLAgents.Agent.html) mention `DecisionRequester`, `StatsRecorder` etc. Which one of these do we need? Which ones we don't need?
 - [ ] On a more theoretical level, we need to think how to motivate it to stand on its own two legs. I think this is basically 95% of the challenge.