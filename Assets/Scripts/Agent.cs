using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour
{
	public float m_maxSpeed;
	public float m_maxAcceleration;
	public float m_orientation;
	public float m_rotation;
	public Vector3 m_velocity;
	protected Steering m_steering;
	public float m_maxRotation;
	public float m_maxAngularAcceleration;
	public float m_priorityThreshold = 0.2f;
	private Dictionary<int, List<Steering>> m_groups;

	void Start()
	{
		m_velocity = Vector3.zero;
		m_steering = new Steering();
		m_groups = new Dictionary<int, List<Steering>>();
	}

	public void SetSteering(Steering p_steering)
	{
		this.m_steering = p_steering;
	}
	public void SetSteering(Steering p_steering, float p_weight)
	{
		this.m_steering.m_linear += (p_weight * p_steering.m_linear);
		this.m_steering.m_angular += (p_weight * p_steering.m_angular);
	}

	public void SetSteering(Steering p_steering, int p_priority)
	{
		if (!m_groups.ContainsKey(p_priority))
		{
			m_groups.Add(p_priority, new List<Steering>());
		}
		m_groups[p_priority].Add(p_steering);
	}

	private Vector3 m_displacement = new Vector3();
	private Quaternion m_quaterionBase = new Quaternion();
	public virtual void Update()
	{
		m_displacement = m_velocity * Time.deltaTime;
		m_orientation += m_rotation * Time.deltaTime;
		if (m_orientation < 0.0f)
			m_orientation += 360f;
		else if (m_orientation > 360f)
			m_orientation -= 360f;
		this.transform.Translate(m_displacement, Space.World);
		this.transform.rotation = m_quaterionBase;
		this.transform.Rotate(Vector3.up, m_orientation);
	}

	public virtual void LateUpdate()
	{
		m_steering = GetPrioritySteering();
		m_groups.Clear();
		m_velocity += m_steering.m_linear * Time.deltaTime;
		m_rotation += m_steering.m_angular * Time.deltaTime;
		if (m_velocity.magnitude > m_maxSpeed)
		{
			m_velocity.Normalize();
			m_velocity *= m_maxSpeed;
		}
		if (m_steering.m_angular < 0.005f)
			m_rotation = 0.0f;
		if (m_steering.m_linear.sqrMagnitude == 0.0f)
			m_velocity = Vector3.zero;
		Steering.ResetValues(m_steering);
	}
	private Steering GetPrioritySteering()
	{
		Steering steering;
		float sqrThreshold = m_priorityThreshold * m_priorityThreshold;
		foreach (List<Steering> group in m_groups.Values)
		{
			steering = new Steering();
			foreach (Steering singleSteering in group)
			{
				//blend by weight
				/*
					steering.linear += singleSteering.linear * m_weight;
					steering.angular += singleSteering.angular * m_weight;
				*/
				steering.m_linear += singleSteering.m_linear;
				steering.m_angular += singleSteering.m_angular;
			}
			if (steering.m_linear.sqrMagnitude > sqrThreshold || Mathf.Abs(steering.m_angular) > m_priorityThreshold)
			{
				return steering;
			}
		}
		return null;
	}
}