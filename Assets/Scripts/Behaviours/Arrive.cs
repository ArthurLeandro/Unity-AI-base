using UnityEngine;
using System.Collections;

public class Arrive : AgentBehaviour
{
	public float m_targetRadius;
	public float m_slowRadius;
	public float m_timeToTarget;

	private Vector3 m_direction;
	private Vector3 m_desiredVelocity;
	private float m_distance;
	private float m_targetSpeed = 0f;
	public override Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		m_direction = m_target.transform.position - transform.position;
		m_distance = m_direction.magnitude;
		if (m_distance > m_targetRadius)
		{
			if (m_distance > m_slowRadius)
				m_targetSpeed = m_agent.m_maxSpeed;
			else
				m_targetSpeed = m_agent.m_maxSpeed * m_distance / m_slowRadius;
			m_desiredVelocity = m_direction;
			m_desiredVelocity.Normalize();
			m_desiredVelocity *= m_targetSpeed;
			m_steeringToReturn.m_linear = m_desiredVelocity - m_agent.m_velocity;
			m_steeringToReturn.m_linear /= m_timeToTarget;
			if (m_steeringToReturn.m_linear.magnitude > m_agent.m_maxAcceleration)
			{
				m_steeringToReturn.m_linear.Normalize();
				m_steeringToReturn.m_linear *= m_agent.m_maxAcceleration;
			}
		}
		return m_steeringToReturn;
	}
}