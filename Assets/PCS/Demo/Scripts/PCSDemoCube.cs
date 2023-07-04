using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCS
{
	public class PCSDemoCube : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.name == "Plane")
			{
				Invoke("Delete", 2);
			}
		}

		void Delete()
		{
			Destroy(gameObject);
		}

	}
}