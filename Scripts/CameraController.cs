﻿using UnityEngine;

namespace Assets.Scripts
{
	public class CameraControler : MonoBehaviour
	{

		public GameObject Player;
	
		void Update ()
		{
			transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10f);
		}
	}
}
