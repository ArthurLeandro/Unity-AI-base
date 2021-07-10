using UnityEngine;
using System.Collections;

public class PathSegment
{
	public Vector3 m_to, m_from;

	public PathSegment() : this(Vector3.zero, Vector3.zero) { }
	public PathSegment(Vector3 p_to, Vector3 p_from)
	{
		m_to = p_to;
		m_from = p_from;
	}
}