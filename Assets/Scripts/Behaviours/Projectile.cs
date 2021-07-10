using UnityEngine;
using System.Collections;
public class Projectile : MonoBehaviour
{
	private bool m_set = false;
	private Vector3 m_firePos;
	private Vector3 m_direction;
	private float m_speed;
	private float m_timeElapsed;
	void Update()
	{
		if (m_set)
		{
			m_timeElapsed += Time.deltaTime;
			transform.position = m_firePos + m_direction * m_speed * m_timeElapsed;
			transform.position += Physics.gravity * (m_timeElapsed * m_timeElapsed) / 2.0f;
			// extra validation for cleaning the scene
			if (transform.position.y < -1.0f)
				Destroy(this.gameObject);// or set = false; and hide it
		}
	}
	public void Set(Vector3 p_firePos, Vector3 p_direction, float p_speed)
	{
		this.m_firePos = p_firePos;
		this.m_direction = p_direction.normalized;
		this.m_speed = p_speed;
		transform.position = p_firePos;
		set = true;
	}

	public float GetLandingTime(float p_height = 0.0f)
	{
		Vector3 position = transform.position;
		float time = 0.0f;
		float valueInt = (m_direction.y * m_direction.y) * (m_speed * m_speed);
		valueInt = valueInt - (Physics.gravity.y * 2 * (m_position.y - p_height));
		valueInt = Mathf.Sqrt(valueInt);
		float valueAdd = (-m_direction.y) * m_speed;
		float valueSub = (-m_direction.y) * m_speed;
		valueAdd = (valueAdd + valueInt) / Physics.gravity.y;
		valueSub = (valueSub - valueInt) / Physics.gravity.y;
		if (float.IsNaN(valueAdd) && !float.IsNaN(valueSub))
			return valueSub;
		else if (!float.IsNaN(valueAdd) && float.IsNaN(valueSub))
			return valueAdd;
		else if (float.IsNaN(valueAdd) && float.IsNaN(valueSub))
			return -1.0f;
		time = Mathf.Max(valueAdd, valueSub);
		return time;
	}
	public Vector3 GetLandingPos(float p_height = 0.0f)
	{
		Vector3 landingPos = Vector3.zero;
		float time = GetLandingTime();
		if (time < 0.0f)
			return landingPos;
		landingPos.y = p_height;
		landingPos.x = m_firePos.x + m_direction.x * m_speed * m_time;
		landingPos.z = m_firePos.z + m_direction.z * m_speed * m_time;
		return landingPos;
	}
	public static Vector3 GetFireDirection(Vector3 p_startPos, Vector3 p_endPos, float p_speed)
	{
		Vector3 direction = Vector3.zero;
		Vector3 delta = p_endPos - p_startPos;
		float a = Vector3.Dot(Physics.gravity, Physics.gravity);
		float b = -4 * (Vector3.Dot(Physics.gravity, delta) + p_speed * p_speed);
		float c = 4 * Vector3.Dot(delta, delta);
		if (4 * a * c > b * b)
			return direction;
		float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a));
		float time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a));
		float time;
		if (time0 < 0.0f)
		{
			if (time1 < 0)
				return direction;
			time = time1;
		}
		else
		{
			if (time1 < 0)
				time = time0;
			else
				time = Mathf.Min(time0, time1);
		}
		direction = 2 * delta - Physics.gravity * (time * time);
		direction = direction / (2 * p_speed * time);
		return direction;
	}
}