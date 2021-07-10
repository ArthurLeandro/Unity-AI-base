using UnityEngine;

public class Wander : Face
{
	public float m_offset;
	public float m_radius;
	public float m_rate;

	public override void Awake()
	{
		m_target = new GameObject();
		m_target.transform.position = transform.position;
		base.Awake();
	}

	private float m_wanderOrientation, m_targetOrientation;
	private Vector3 m_orientationVec, m_targetPosition;
	public override Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		m_wanderOrientation = Random.Range(-1.0f, 1.0f) * m_rate;
		m_targetOrientation = m_wanderOrientation + m_agent.m_orientation;
		m_orientationVec = OriginAsVector(m_agent.m_orientation);
		m_targetPosition = (m_offset * m_orientationVec) + transform.position;
		m_targetPosition = m_targetPosition + (OriginAsVector(m_targetOrientation) * m_radius);
		m_targetAux.transform.position = m_targetPosition;
		m_steeringToReturn = base.GetSteering();
		m_steeringToReturn.m_linear = m_targetAux.transform.position - transform.position;
		m_steeringToReturn.m_linear.Normalize();
		m_steeringToReturn.m_linear *= m_agent.m_maxAcceleration;
		return m_steeringToReturn;
	}
}