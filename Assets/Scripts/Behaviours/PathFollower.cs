using UnityEngine;
using System.Collections;
public class PathFollower : Seek
{
	public Path m_path;
	public float m_pathOffset = 0.0f;
	float m_currentParam;
	public override void Awake()
	{
		base.Awake();
		m_target = new GameObject();
		m_currentParam = 0f;
	}
	private float m_targetParam;
	public override Steering GetSteering()
	{
		m_currentParam = m_path.GetParam(transform.position, m_currentParam);
		m_targetParam = m_currentParam + m_pathOffset;
		m_target.transform.position = m_path.GetPosition(m_targetParam);
		return base.GetSteering();
	}
}