using UnityEngine;
using System.Collections;
public class AvoidWall : Seek
{
	public float m_avoidDistance;
	public float m_lookAhead;
	public override void Awake()
	{
		base.Awake();
		m_target = new GameObject();
	}

	Vector3 m_position;
	Vector3 m_rayVector;
	Vector3 m_direction;
	public override Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		m_position = transform.position;
		m_rayVector = m_agent.m_velocity.normalized * m_lookAhead;
		m_direction = m_rayVector;
		RaycastHit hit;
		if (Physics.Raycast(m_position, m_direction, out hit, m_lookAhead))
		{
			m_position = hit.point + hit.normal * m_avoidDistance;
			m_target.transform.position = m_position;
			m_steeringToReturn = base.GetSteering();
		}
		return m_steeringToReturn;
	}
}