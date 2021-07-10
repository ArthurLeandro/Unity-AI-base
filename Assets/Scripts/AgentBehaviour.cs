using UnityEngine;
using System.Collections;

public class AgentBehaviour : MonoBehaviour
{
	public GameObject m_target;
	protected Agent m_agent;
	protected Steering m_steeringToReturn;
	public float m_maxSpeed;
	public float m_maxAcceleration;
	public float m_maxRotation;
	public float m_maxAngularAcceleration;
	public float m_weight = 1.0f;
	public int m_priority = 1;

	public virtual void Awake()
	{
		m_agent = gameObject.GetComponent<Agent>();
	}

	public virtual void Update()
	{
		m_agent.SetSteering(GetSteering(), m_weight);
	}

	public Vector3 OriginAsVector(float p_orientation)
	{
		Vector3 vector = Vector3.zero;
		float cache = p_orientation * Mathf.Deg2Rad;
		vector.x = Mathf.Sin(cache) * 1.0f;
		vector.z = Mathf.Cos(cache) * 1.0f;
		return vector.normalized;
	}

	public float MapToRange(float p_rotation)
	{
		p_rotation %= 360.0f;
		if (Mathf.Abs(p_rotation) > 180.0f)
		{
			if (p_rotation < 0.0f)
				p_rotation += 360.0f;
			else
				p_rotation -= 360.0f;
		}
		return p_rotation;
	}

	public virtual Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		return m_steeringToReturn;
	}
}