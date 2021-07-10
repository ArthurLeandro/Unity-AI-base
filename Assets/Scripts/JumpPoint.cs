using UnityEngine;
public class JumpPoint
{
	public Vector3 m_jumpLocation;
	public Vector3 m_landingLocation;
	//The change in position from jump to landing
	public Vector3 m_deltaPosition;
	public JumpPoint()
	: this(Vector3.zero, Vector3.zero)
	{
	}
	public JumpPoint(Vector3 a, Vector3 b)
	{
		this.m_jumpLocation = a;
		this.m_landingLocation = b;
		this.m_deltaPosition = this.m_landingLocation - this.m_jumpLocation;
	}
}