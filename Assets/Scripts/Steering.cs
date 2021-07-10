using UnityEngine;
using System.Collections;

public class Steering
{
	public float m_angular;
	public Vector3 m_linear;
	public Steering()
	{
		m_angular = 0.0f;
		m_linear = new Vector3(0f, 0f, 0f);
	}
	public static void ResetValues(Steering p_sterringToReset)
	{
		p_sterringToReset.m_angular = 0.0f;
		p_sterringToReset.m_linear = Vector3.zero;
	}
}