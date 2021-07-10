using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
	public List<GameObject> m_nodes;
	List<PathSegment> m_segments;

	void Start()
	{
		m_segments = GetSegments();
	}

	public List<PathSegment> GetSegments()
	{
		List<PathSegment> segments = new List<PathSegment>();
		Vector3 src, dst;
		PathSegment seg;
		for (int i = 0; i < m_nodes.Count - 1; i++)
		{
			src = m_nodes[i].transform.position;
			dst = m_nodes[i + 1].transform.position;
			seg = new PathSegment(src, dst);
			segments.Add(seg);
		}
		return segments;
	}

	public float GetParam(Vector3 p_pos, float p_lastParam)
	{
		float param = 0f;
		float tempParam = 0f;
		PathSegment currentSegment = null;
		foreach (PathSegment ps in m_segments)
		{
			tempParam += Vector3.Distance(ps.m_to, ps.m_from);
			if (p_lastParam <= tempParam)
			{
				currentSegment = ps;
				break;
			}
		}
		if (currentSegment != null)
		{
			Vector3 currPos = p_pos - currentSegment.m_to;
			Vector3 segmentDirection = currentSegment.m_from - currentSegment.m_to;
			segmentDirection.Normalize();
			Vector3 pointInSegment = Vector3.Project(currPos, segmentDirection);
			param = tempParam - Vector3.Distance(currentSegment.m_to, currentSegment.m_from);
			param += pointInSegment.magnitude;
		}
		return param;
	}

	public Vector3 GetPosition(float p_param)
	{
		Vector3 position = Vector3.zero;
		PathSegment currentSegment = null;
		float tempParam = 0f;
		foreach (PathSegment ps in m_segments)
		{
			tempParam += Vector3.Distance(ps.m_to, ps.m_from);
			if (p_param <= tempParam)
			{
				currentSegment = ps;
				break;
			}
		}
		if (currentSegment != null)
		{
			Vector3 segmentDirection = currentSegment.m_from - currentSegment.m_to;
			segmentDirection.Normalize();
			tempParam -= Vector3.Distance(currentSegment.m_to, currentSegment.m_from);
			tempParam = p_param - tempParam;
			position = currentSegment.m_to + segmentDirection * tempParam;
		}
		return position;
	}
	void OnDrawGizmos()
	{
		Vector3 direction;
		Color tmp = Gizmos.color;
		Gizmos.color = Color.magenta;
		Vector3 src, dst;
		for (int i = 0; i < m_nodes.Count - 1; i++)
		{
			src = m_nodes[i].transform.position;
			dst = m_nodes[i + 1].transform.position;
			direction = dst - src;
			Gizmos.DrawRay(src, direction);
		}
		Gizmos.color = tmp;
	}
}