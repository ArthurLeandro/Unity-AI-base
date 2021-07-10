using UnityEngine;

public class Face : Align
{
	protected GameObject m_targetAux;

	public override void Awake()
	{
		base.Awake();
		m_targetAux = m_target;
		m_target = new GameObject();
		m_target.AddComponent<Agent>();
	}

	void OnDestroy()
	{
		Destroy(m_target);
	}

	private Vector3 m_direction;
	private float m_targetOrientation;
	public override Steering GetSteering()
	{
		m_direction = m_targetAux.transform.position - transform.position;
		if (m_direction.magnitude > 0.0f)
		{
			m_targetOrientation = Mathf.Atan2(m_direction.x, m_direction.z);
			m_targetOrientation *= Mathf.Rad2Deg;
			m_target.GetComponent<Agent>().m_orientation = m_targetOrientation;
		}
		return base.GetSteering();
	}
}