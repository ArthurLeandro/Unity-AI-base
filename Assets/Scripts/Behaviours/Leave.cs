using UnityEngine;

public class Leave : AgentBehaviour
{
	public float m_escapeRadius;
	public float m_dangerRadius;
	public float m_timeToTarget = .1f;

	private Vector3 m_direction;
	private float m_distance;
	private float m_reduce;
	private float m_targetSpeed;
	private Vector3 m_desiredVelocity;

	public override Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		m_direction = m_target.transform.position - transform.position;
		m_distance = m_direction.magnitude;
		if (m_distance < m_dangerRadius)
		{
			if (m_distance < m_escapeRadius)
				m_reduce = 0f;
			else
				m_reduce = m_distance / m_dangerRadius * m_agent.m_maxSpeed;
			m_targetSpeed = m_agent.m_maxSpeed - m_reduce;
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