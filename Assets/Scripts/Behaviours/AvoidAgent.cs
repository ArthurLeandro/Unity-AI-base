using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AvoidAgent : AgentBehaviour
{
	public float m_collisionRadius = 0.4f;
	GameObject[] m_targets;

	void Start()
	{
		m_targets = GameObject.FindGameObjectsWithTag("Agent");
	}
	private float m_shortestTime = Mathf.Infinity;
	private GameObject m_firstTarget = null;
	private float m_firstMinSeparation = 0.0f;
	private float m_firstDistance = 0.0f;
	private Vector3 m_firstRelativePos = Vector3.zero;
	private Vector3 m_firstRelativeVel = Vector3.zero;
	Vector3 m_relativePos;
	Agent m_targetAgent;
	Vector3 m_relativeVel;
	float m_relativeSpeed;
	float m_timeToCollision;
	float m_distance;
	float m_minSeparation;
	public override Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		foreach (GameObject t in m_targets)
		{
			m_targetAgent = t.GetComponent<Agent>();
			m_relativePos = t.transform.position - transform.position;
			m_relativeVel = m_targetAgent.m_velocity - m_agent.m_velocity;
			m_relativeSpeed = m_relativeVel.magnitude;
			m_timeToCollision = Vector3.Dot(m_relativePos, m_relativeVel);
			m_timeToCollision /= m_relativeSpeed * m_relativeSpeed * -1;
			m_distance = m_relativePos.magnitude;
			m_minSeparation = m_distance - m_relativeSpeed *
			m_timeToCollision;
			if (m_minSeparation > 2 * m_collisionRadius)
				continue;
			if (m_timeToCollision > 0.0f && m_timeToCollision < m_shortestTime)
			{
				m_shortestTime = m_timeToCollision;
				m_firstTarget = t;
				m_firstMinSeparation = m_minSeparation;
				m_firstRelativePos = m_relativePos;
				m_firstRelativeVel = m_relativeVel;
			}
		}
		if (m_firstTarget != null)
		{
			if (m_firstMinSeparation <= 0.0f || m_firstDistance < 2 * m_collisionRadius)
				m_firstRelativePos = m_firstTarget.transform.position;
			else
				m_firstRelativePos += m_firstRelativeVel * m_shortestTime;
			m_firstRelativePos.Normalize();
			m_steeringToReturn.m_linear = -m_firstRelativePos * m_agent.m_maxAcceleration;
		}
		return m_steeringToReturn;
	}
}