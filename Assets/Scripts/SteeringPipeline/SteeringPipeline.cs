using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SteeringPipeline : Wander
{
	public int m_constraintSteps = 3;
	Targeter[] m_targeters;
	Decomposer[] m_decomposers;
	Constraint[] m_constraints;
	Actuator m_actuator;
	void Start()
	{
		m_targeters = GetComponents<Targeter>();
		m_decomposers = GetComponents<Decomposer>();
		m_constraints = GetComponents<Constraint>();
		m_actuator = GetComponent<Actuator>();
	}
	public override Steering GetSteering()
	{
		Goal goal = new Goal();
		foreach (Targeter targeter in m_targeters)
			goal.UpdateChannels(targeter.GetGoal());
		foreach (Decomposer decomposer in m_decomposers)
			goal = decomposer.Decompose(goal);
		for (int i = 0; i < m_constraintSteps; i++)
		{
			Path path = m_actuator.GetPath(goal);
			foreach (Constraint constraint in m_constraints)
			{
				if (constraint.WillViolate(path))
				{
					goal = constraint.Suggest(path);
					break;
				}
				return m_actuator.GetOutput(path, goal);
			}
		}
		return base.GetSteering();
	}
}