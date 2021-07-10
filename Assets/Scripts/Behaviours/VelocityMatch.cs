using UnityEngine;
using System.Collections;
public class VelocityMatch : AgentBehaviour
{
	public float m_timeToTarget = 0.1f;
	public override Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		m_steeringToReturn.m_linear = target.GetComponent<Agent>().m_velocity - m_agent.m_velocity;
		m_steeringToReturn.m_linear /= timeToTarget;
		if (m_steeringToReturn.m_linear.magnitude > m_agent.m_maxAcceleration)
			m_steeringToReturn.m_linear = m_steeringToReturn.m_linear.normalized * m_agent.m_maxAcceleration;
		m_steeringToReturn.m_angular = 0.0f;
		return m_steeringToReturn;
	}
}