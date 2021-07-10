using UnityEngine;
using System.Collections;

public class Seek : AgentBehaviour
{
	public override Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		m_steeringToReturn.m_linear = m_target.transform.position - transform.position;
		m_steeringToReturn.m_linear.Normalize();
		m_steeringToReturn.m_linear = m_steeringToReturn.m_linear * m_agent.m_maxAcceleration;
		return m_steeringToReturn;
	}
}