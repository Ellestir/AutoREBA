using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCS
{
	public class ConveyorController : MonoBehaviour
	{
		public PCSConfig config;
		public float minSpeed, maxSpeed, changeInterval;

		// Start is called before the first frame update
		void Start()
		{
			InvokeRepeating("SetSpeed", 0, changeInterval);
		}

		// Update is called once per frame
		void Update()
		{

		}

		void SetSpeed()
		{
			float newSpeed = Random.Range(minSpeed, maxSpeed);
			config.SetSpeed(newSpeed);
		}
	}
}