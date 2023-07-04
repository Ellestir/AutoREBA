using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCS
{

	public class PCSUVScroller : MonoBehaviour
	{
		public float speed = 0.5f;
		Material m;

		// Use this for initialization
		void Start()
		{
			m = GetComponent<MeshRenderer>().sharedMaterial;
			m.mainTextureOffset = Vector2.zero;
		}

		// Update is called once per frame
		void Update()
		{
			float yOffset = m.mainTextureOffset.y - speed * Time.deltaTime;
			yOffset = yOffset % 1;
			m.mainTextureOffset = new Vector2(0, yOffset);
		}
	}

}
