using UnityEngine;

public class Align : AgentBehaviour
{
	public float m_targetRadius;
	public float m_slowRadius;
	public float m_timeToTarget;


	private float m_targetOrientation;
	private float m_rotation;
	private float m_rotationSize;
	private float m_targetRotation;
	private float m_angularAcceleration;
	public override Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		m_targetOrientation = m_target.GetComponent<Agent>().m_orientation;
		m_rotation = m_targetOrientation - m_agent.m_orientation;
		m_rotation = MapToRange(m_rotation);
		m_rotationSize = Mathf.Abs(m_rotation);
		if (m_rotationSize > m_targetRadius)
		{
			if (m_rotationSize > m_slowRadius)
				m_targetRotation = m_agent.m_maxRotation;
			else
				m_targetRotation = m_agent.m_maxRotation / m_rotationSize * m_slowRadius;
			m_targetRotation *= m_rotation / m_rotationSize;
			m_steeringToReturn.m_angular = m_targetRotation - m_agent.m_rotation;
			m_steeringToReturn.m_angular /= m_timeToTarget;
			m_angularAcceleration = Mathf.Abs(m_steeringToReturn.m_angular);
			if (m_angularAcceleration > m_agent.m_maxAcceleration)
			{
				m_steeringToReturn.m_angular /= m_angularAcceleration;
				m_steeringToReturn.m_angular *= m_agent.m_maxAngularAcceleration;
			}
		}
		return m_steeringToReturn;
	}
}