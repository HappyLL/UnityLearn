﻿using UnityEngine;
using System.Collections;

//实现人物的移动和转向
public class UserPlayerController : MonoBehaviour 
{

	public float m_speed = 5;

	private Rigidbody m_rig;
	private Animator m_ani;
	private float m_rayDis = 100;
	private int m_floorMsk;
	//private RaycastHit m_hitPt;

	void Start()
	{
		m_rig = GetComponent<Rigidbody> ();
		m_ani = GetComponent<Animator> ();
		m_floorMsk = LayerMask.GetMask ("UserFloor");
		//m_hitPt = new RaycastHit ();
	}
	//涉及刚体的运动需要在物理帧更新
	void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");	
		float v = Input.GetAxis ("Vertical");
		Move (h , v);
		Turn ();
	}

	void Move(float h , float v)
	{
		Vector3 vMove = new Vector3 (h , 0 , v);
		vMove = Vector3.Normalize (vMove)*m_speed*Time.deltaTime;
		m_rig.MovePosition (vMove + transform.position);
		//Debug.Log ("the state: "+ (h != 0 || v != 0));
		m_ani.SetBool ("IsWalking" , h != 0 || v != 0);
	}

	void Turn()
	{
		//Debug.Log ("x: "+ Input.mousePosition.x + " y: " +Input.mousePosition.y);
		//得到在屏幕中鼠标点击为开始点 产生的射线
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, m_rayDis, m_floorMsk)) 
		{
			Vector3 rotion = hit.point - transform.position;
			rotion.y = 0;
			//Debug.Log ("x: "+rotion.x+" y:"+rotion.y);
			m_rig.MoveRotation(Quaternion.LookRotation (rotion));
		}
	}
}
