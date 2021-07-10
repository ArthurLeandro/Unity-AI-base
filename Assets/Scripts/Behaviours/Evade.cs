using UnityEngine;
using System.Collections;

public class Evade : Flee
{
	public float m_maxPrediction;
	private GameObject m_targetAuxiliary;
	private Agent m_targetAgent;

	public override void Awake()
	{
		base.Awake();
		m_targetAgent = m_target.GetComponent<Agent>();
		m_targetAuxiliary = m_target;
		m_target = new GameObject();
	}

	void OnDestroy()
	{
		Destroy(m_targetAuxiliary);
	}

	private Vector3 m_direction = new Vector3();
	private float m_distance = 0.0f;
	private float m_speed = 0.0f;
	private float m_prediction = 0.0f;
	public override Steering GetSteering()
	{
		m_direction = m_targetAuxiliary.transform.position - transform.position;
		m_distance = m_direction.magnitude;
		m_speed = m_agent.m_velocity.magnitude;
		m_prediction = (m_speed <= m_distance / m_maxPrediction) ? m_maxPrediction : m_distance / m_speed;
		m_target.transform.position = m_targetAuxiliary.transform.position;
		m_target.transform.position += m_targetAgent.m_velocity * m_prediction;
		return base.GetSteering();
	}
}