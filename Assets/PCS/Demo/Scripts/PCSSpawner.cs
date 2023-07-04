using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCS
{
	public class PCSSpawner : MonoBehaviour
	{
		public bool repeat;
		public float interval;
		public float scale;
		
		// Start is called before the first frame update
		void Start()
		{
			if (repeat)
				InvokeRepeating("Spawn", 0, interval);
			else
				Spawn();
		}

		// Update is called once per frame
		void Update()
		{

		}

		void Spawn()
		{
			GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			obj.transform.parent = transform;
			obj.transform.localPosition = Vector3.zero;
			obj.transform.localScale *= Random.Range(0.7f * scale, 1.3f * scale);
			obj.AddComponent<Rigidbody>();
			obj.AddComponent<PCSDemoCube>();
			obj.GetComponent<MeshRenderer>().material.color = Color.Lerp(new Color(0.78f, 0.68f, 0.44f), Color.black, 0.7f*Random.value);  // new Color(Random.value, Random.value, Random.value);
		}
	}
}
