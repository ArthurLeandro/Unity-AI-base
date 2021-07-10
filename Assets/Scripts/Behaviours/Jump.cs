using UnityEngine;
using System.Collections.Generic;
public class Jump : VelocityMatch
{
	public JumpPoint m_jumpPoint;
	//Keeps track of whether the jump is achievable
	bool m_canAchieve = false;
	//Holds the maximum vertical jump velocity
	public float m_maxYVelocity;
	public Vector3 m_gravity = new Vector3(0, -9.8f, 0);
	private Projectile m_projectile;
	private List<AgentBehaviour> m_behaviours;
	public void Isolate(bool p_state)
	{
		foreach (AgentBehaviour b in behaviours)
			b.m_enabled = !p_state;
		this.m_enabled = p_state;
	}
	public void DoJump()
	{
		m_projectile.m_enabled = true;
		Vector3 direction;
		direction = Projectile.GetFireDirection(m_jumpPoint.m_jumpLocation, m_jumpPoint.m_landingLocation, m_agent.m_maxSpeed);
		m_projectile.Set(m_jumpPoint.m_jumpLocation, direction, m_agent.m_maxSpeed, false);
	}
	protected void CalculateTarget()
	{
		m_target = new GameObject();
		m_target.AddComponent<Agent>();
		//Calculate the first jump time
		float sqrtTerm = Mathf.Sqrt(2f * m_gravity.y * m_jumpPoint.m_deltaPosition.y + m_maxYVelocity * m_agent.maxSpeed);
		float time = (m_maxYVelocity - m_sqrtTerm) / m_gravity.y;
		//Check if we can use it, otherwise try the other time
		if (!CheckJumpTime(time))
		{
			time = (m_maxYVelocity + m_sqrtTerm) / m_gravity.y;
		}
	}
	private bool CheckJumpTime(float p_time)
	{
		//Calculate the planar speed
		float vx = jumpPoint.deltaPosition.x / p_time;
		float vz = jumpPoint.deltaPosition.z / p_time;
		float speedSq = vx * vx + vz * vz;
		//Check it to see if we have a valid solution
		if (m_speedSq < m_agent.m_maxSpeed * m_agent.m_maxSpeed)
		{
			m_target.GetComponent<Agent>().m_velocity = new Vector3(vx, 0f, vz);
			m_canAchieve = true;
			return true;
		}
		return false;
	}
	public override void Awake()
	{
		base.Awake();
		this.enabled = false;
		m_projectile = gameObject.AddComponent<Projectile>();
		m_behaviours = new List<AgentBehaviour>();
		AgentBehaviour[] abs;
		abs = gameObject.GetComponents<AgentBehaviour>();
		foreach (AgentBehaviour b in abs)
		{
			if (b == this)
				continue;
			m_behaviours.Add(b);
		}
	}
	public override Steering GetSteering()
	{
		Steering.ResetValues(m_steeringToReturn);
		//Steering steering = new Steering();
		// Check if we have a trajectory, and create one if not.
		if (m_jumpPoint != null && m_target == null)
		{
			CalculateTarget();
		}
		//Check if the trajectory is zero. If not, we have no		acceleration.
		if (!m_canAchieve)
		{
			return steering;
		}
		//Check if we've hit the jump point
		if (Mathf.Approximately((transform.position - m_target.transform.position).magnitude, 0f) && Mathf.Approximately((m_agent.velocity - m_target.GetComponent<Agent>().m_velocity).magnitude, 0f))
		{
			DoJump();
			return m_steeringToReturn;
		}
		return base.GetSteering();
	}
}