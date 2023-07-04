using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCS
{
	[RequireComponent(typeof(Rigidbody))]
	public class PCSConveyor : MonoBehaviour
	{
		Rigidbody rb;
		public float speed;

		private void Start()
		{
			rb = GetComponent<Rigidbody>();
		}

		void FixedUpdate()
		{
			transform.position -= transform.forward * speed * Time.fixedDeltaTime;
			Physics.SyncTransforms();
			rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
		}

	}
}
