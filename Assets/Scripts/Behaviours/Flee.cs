using UnityEngine;
using System.Collections;

public class Flee : AgentBehaviour
{
	public override Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		m_steeringToReturn.m_linear = transform.position - m_target.transform.position;
		m_steeringToReturn.m_linear.Normalize();
		m_steeringToReturn.m_linear = m_steeringToReturn.m_linear * m_agent.m_maxAcceleration;
		return m_steeringToReturn;
	}
}