using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCS
{
	[System.Serializable]
	public class PCSRailingData
	{
		public List<bool> enabledStates = new List<bool>();
	}

	[System.Serializable]
	public class PCSPart
	{
		public enum LengthMode { Stretch, Repeat };

		public GameObject prefab;
		public GameObject gameObject;
		public bool mirror;
		public Vector3 positionOffset;
		public Renderer[] renderers;
		public GameObject parent;
		public LengthMode lengthMode;
	};

	public class PCSConfig : MonoBehaviour
	{
		public enum EditModes { None, Railings }

		struct PCSTransform{
			public Vector3 position;
			public Quaternion rotation;
			public Vector3 scale;
		};
				
		public EditModes editMode;

		public PCSPart belt;
		public PCSPart startCap;
		public PCSPart endCap;
		public PCSPart railing;
		public PCSPart railingStartCap;
		public PCSPart railingEndCap;
		public PCSPart railingDoubleCap;
		public PCSPart internals;
		public GameObject physicsParent;

		public PCSRailingData[] railingData;
		private GameObject[][] railingTempParents = new GameObject[2][];
		private GameObject[] railingTempParentsStartCap;
		private GameObject[] railingTempParentsEndCap;

		public int length = 18;
		public float width = 2f;
		public bool internalsEnabled = false;
		public int internalsCount = 3;
		public float speed = 0.6f;
		public Color32 colour = new Color32(50, 50, 50 , 255);

		public PCSConveyor pcsC;

		public bool settingsImported;
		
		public GameObject railingEditCollidersParent;
		public Dictionary<Collider, int> railingEditCollidersSideIndex;
		public Dictionary<Collider, int> railingEditCollidersRailingIndex;
		private List<Collider> visibleColliders;

		private PCSTransform parentTransform;

		public List<GameObject> conveyorChildren;// = new List<GameObject>();

		public bool ready = false;

		public PCSUVScroller[] uvS = new PCSUVScroller[4];
		
		public void CreatePCS()
		{
			deleteAllColliders();

			if (settingsImported)
			{
				CheckRailingData();
				visibleColliders = new List<Collider>();

				if (conveyorChildren != null)
				{
					foreach (GameObject obj in conveyorChildren)
					{
						DestroyImmediate(obj);
					}
				}

			

				Initialise();
				InstantiateObjects();
				CombineMeshes();
				InstantiateMaterials();
				CreatePhysicsComponenets();


				foreach (Collider c in visibleColliders)
				{
					Collider newC = gameObject.AddDuplicateCollider(c);
					newC.hideFlags = HideFlags.HideInInspector | HideFlags.NotEditable;
					DestroyImmediate(c);

				}

				ApplyTransform();

				conveyorChildren = new List<GameObject>();
				conveyorChildren.Add(belt.parent);
				conveyorChildren.Add(railing.parent);
				conveyorChildren.Add(startCap.parent);
				conveyorChildren.Add(endCap.parent);
				conveyorChildren.Add(internals.parent);
				conveyorChildren.Add(physicsParent);

			}

		}

		public void CheckRailingData()
		{
			if (railingData == null || railingData.Length != 2 || railingData[0] == null || railingData[1] == null)
			{
				railingData = new PCSRailingData[2];

				railingData[0] = new PCSRailingData();
				railingData[1] = new PCSRailingData();
			}

			for (int i = 0; i < 2; i++)
			{
				if (railingData[i].enabledStates == null)
					railingData[i].enabledStates = new List<bool>();

				if (railingData[i].enabledStates.Count != length + 2)
				{

					for (int j = railingData[i].enabledStates.Count; j < length + 2; j++)
					{
						if (railingData[i].enabledStates.Count > 0)
							railingData[i].enabledStates.Add(railingData[i].enabledStates[railingData[i].enabledStates.Count - 1]);
						else
							railingData[i].enabledStates.Add(true);
					}
					while (railingData[i].enabledStates.Count > length + 2)
					{
						railingData[i].enabledStates.RemoveAt(length + 2);
					};
				}


			}
		}

		void Initialise()
		{
			uvS = new PCSUVScroller[4];	

			//--------------------Initialise transform---------------------
			parentTransform.position = transform.position;
			parentTransform.rotation = transform.rotation;
			parentTransform.scale = transform.localScale;

			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			//-------------------------------------------------------------

			//--------------------Initialise internals---------------------
			if (internalsEnabled && internalsCount < 2)
				internalsCount = 2;
			//-------------------------------------------------------------



			//-----Get prefab widths and renderers, set prefab offsets-----//-----------------------------------------------------------------------
			float beltPrefabWidth, startCapPrefabWidth, endCapPrefabWidth;

			belt.renderers = new Renderer[1];
			belt.renderers[0] = belt.prefab.GetComponent<Renderer>();

			railing.renderers = new Renderer[railing.prefab.transform.childCount];
			for (int i = 0; i < railing.renderers.Length; i++)
			{
				railing.renderers[i] = railing.prefab.transform.GetChild(i).GetComponent<MeshRenderer>();
			}

			railingStartCap.renderers = new Renderer[railingStartCap.prefab.transform.childCount];
			for (int i = 0; i < railingStartCap.renderers.Length; i++)
			{
				railingStartCap.renderers[i] = railingStartCap.prefab.transform.GetChild(i).GetComponent<MeshRenderer>();
			}

			railingEndCap.renderers = new Renderer[railingEndCap.prefab.transform.childCount];
			for (int i = 0; i < railingEndCap.renderers.Length; i++)
			{
				railingEndCap.renderers[i] = railingEndCap.prefab.transform.GetChild(i).GetComponent<MeshRenderer>();
			}

			internals.renderers = new Renderer[1];
			internals.renderers[0] = internals.prefab.GetComponent<Renderer>();

			beltPrefabWidth = belt.renderers[0].bounds.size.z;
			startCapPrefabWidth = startCap.prefab.GetComponent<Renderer>().bounds.size.z;
			endCapPrefabWidth = endCap.prefab.GetComponent<Renderer>().bounds.size.z;

			belt.positionOffset = new Vector3(0, 0, beltPrefabWidth);
			startCap.positionOffset = new Vector3(0, 0, startCapPrefabWidth);
			endCap.positionOffset = new Vector3(0, 0, endCapPrefabWidth);
			//--------------------------------------------------------------------------------------------------------------------------------------


			//--------------------Create parent objects--------------------
			belt.parent = new GameObject("Belt");
			belt.parent.transform.parent = transform;
			belt.parent.transform.localPosition = PCSUtils.GetBeltTopCenter(startCap.positionOffset, belt.positionOffset, length);
			belt.parent.hideFlags = HideFlags.HideInHierarchy;
			uvS[2] = belt.parent.AddComponent<PCSUVScroller>();
			uvS[2].speed = speed / 0.2f;



			railing.parent = new GameObject("Railing");
			railing.parent.transform.parent = transform;
			railing.parent.transform.localPosition = PCSUtils.GetBeltTopCenter(startCap.positionOffset, belt.positionOffset, length);
			railing.parent.hideFlags = HideFlags.HideInHierarchy;
			if (editMode == EditModes.Railings)
				railing.parent.SetActive(false);


			for (int j = 0; j < railing.prefab.transform.childCount; j++)
			{
				GameObject child = new GameObject(railing.prefab.transform.GetChild(j).name);
				child.transform.parent = railing.parent.transform;
				child.transform.localPosition = Vector3.zero;
				child.hideFlags = HideFlags.HideInHierarchy;
			}

			startCap.parent = new GameObject("Start Cap");
			startCap.parent.transform.parent = transform;
			startCap.parent.transform.localPosition = PCSUtils.GetStartCapTopEdge(startCap.positionOffset);
			startCap.parent.hideFlags = HideFlags.HideInHierarchy;

			railingStartCap.parent = new GameObject("Railing Start Cap");
			railingStartCap.parent.transform.parent = startCap.parent.transform;
			railingStartCap.parent.transform.position = Vector3.zero;
			railingStartCap.parent.hideFlags = HideFlags.HideInHierarchy;
			if (editMode == EditModes.Railings)
				railingStartCap.parent.SetActive(false);

			for (int j = 0; j < railingStartCap.prefab.transform.childCount; j++)
			{
				GameObject child = new GameObject(railingStartCap.prefab.transform.GetChild(j).name);
				child.transform.parent = railingStartCap.parent.transform;
				child.transform.localPosition = Vector3.zero;
				child.hideFlags = HideFlags.HideInHierarchy;
			}

			endCap.parent = new GameObject("End Cap");
			endCap.parent.transform.parent = transform;
			endCap.parent.transform.localPosition = PCSUtils.GetEndCapTopEdge(startCap.positionOffset, belt.positionOffset, length);
			endCap.parent.hideFlags = HideFlags.HideInHierarchy;

			railingEndCap.parent = new GameObject("Railing End Cap");
			railingEndCap.parent.transform.parent = endCap.parent.transform;
			railingEndCap.parent.transform.localPosition = Vector3.zero;
			railingEndCap.parent.hideFlags = HideFlags.HideInHierarchy;
			if (editMode == EditModes.Railings)
				railingEndCap.parent.SetActive(false);

			for (int j = 0; j < railingEndCap.prefab.transform.childCount; j++)
			{
				GameObject child = new GameObject(railingEndCap.prefab.transform.GetChild(j).name);
				child.transform.parent = railingEndCap.parent.transform;
				child.transform.localPosition = Vector3.zero;
				child.hideFlags = HideFlags.HideInHierarchy;
			}

			internals.parent = new GameObject("Internals");
			internals.parent.transform.parent = transform;
			internals.parent.transform.localPosition = Vector3.zero; //PCSUtils.GetBeltTopCenter(startCap.positionOffset, belt.positionOffset, length);
			internals.parent.hideFlags = HideFlags.HideInHierarchy;
			uvS[3] = internals.parent.AddComponent<PCSUVScroller>();
			uvS[3].speed = speed / (0.18f*Mathf.PI);

			physicsParent = new GameObject("Physics");
			physicsParent.transform.parent = transform;
			physicsParent.transform.localPosition = Vector3.zero;
			physicsParent.hideFlags = HideFlags.HideInHierarchy;
			//-------------------------------------------------------------
		}

		void InstantiateObjects()
		{
			//Set start point for instantiating objects
			Vector3 instantiatePosition = startCap.positionOffset;


			//Instantiate belt start cap
			startCap.gameObject = InstantiateCap(startCap.prefab, startCap.parent, instantiatePosition, startCap.mirror, "Belt Start Cap");
			startCap.gameObject.hideFlags = HideFlags.HideInHierarchy;
			uvS[0] = startCap.gameObject.AddComponent<PCSUVScroller>();
			uvS[0].speed = speed / 0.2f;


			//-----------------Initialse railing variables-----------------
			GameObject[][] railings = new GameObject[2][];
			int[] railingCount = GetRailingCount();
			railings[0] = new GameObject[railingCount[0]];
			railings[1] = new GameObject[railingCount[1]];
			int[] currentRailing = { 0, 0 };
			bool[] newR = { false, false };
			bool[] newMR = { false, false };

			//----------------Instantiate railing start cap----------------
			railingTempParentsStartCap = new GameObject[2];
			for (int i = 0; i < 2; i++)
			{
				if (railingData[i].enabledStates[0])
					railingTempParentsStartCap[i] = InstantiateRailingPiece(i, 0, instantiatePosition);
			}
			//-------------------------------------------------------------

			//Create railing parents
			CreateRailingParents(railingCount);
			//-------------------------------------------------------------


			//----------------Instantiate belt and railings----------------//---------------------------------------------------------------------------------
			if (belt.lengthMode == PCSPart.LengthMode.Stretch)
			{
				belt.gameObject = Instantiate(belt.prefab, transform);
				belt.gameObject.transform.localPosition = instantiatePosition;
				belt.gameObject.transform.localScale = Vector3.Scale(belt.gameObject.transform.localScale, new Vector3((width-0.4f)/1.6f, 1, length));
				if (belt.mirror)
					belt.gameObject.transform.localScale = Vector3.Scale(belt.gameObject.transform.localScale, new Vector3(1, 1, -1));
				belt.gameObject.transform.parent = belt.parent.transform;
			}

			if (railing.lengthMode == PCSPart.LengthMode.Stretch)
			{
				Vector3 startInstPos = instantiatePosition;
				int[] pieceLength = { 1, 1 };
				GameObject[] middlePart = new GameObject[2];
				bool[] incrementCurrentRailing = { false, false };

				for (int i = 0; i < length+2; i++)
				{
					for (int j = 0; j < 2; j++)
					{
						//First part of railing
						if (railingData[j].enabledStates[i] && !newR[j])
						{

							if (i > 0 && i <= length)
							{
								railings[j][currentRailing[j]] = InstantiateRailingPiece(j, i, instantiatePosition);

								for (int k = 0; k < railing.prefab.transform.childCount; k++)
								{
									railings[j][currentRailing[j]].transform.GetChild(0).parent = railingTempParents[j][currentRailing[j]].transform.GetChild(k);
								}
								GameObject.DestroyImmediate(railings[j][currentRailing[j]]);
								incrementCurrentRailing[j] = true;
							}

							newR[j] = true;
						}
						//Middle part of railing
						else if (railingData[j].enabledStates[i] && (i <= length && railingData[j].enabledStates[i + 1]) && newR[j])// && railingData[j].enabledStates[i2])
						{
							if (!newMR[j])
							{
								if (i > 0 && i <= length)
								{
									railings[j][currentRailing[j]] = InstantiateRailingPiece(j, i, instantiatePosition);
									middlePart[j] = railings[j][currentRailing[j]];
									incrementCurrentRailing[j] = true;
								}

								newMR[j] = true;
							}
							else
								pieceLength[j]++;
						}
						//End part of railing
						else if (railingData[j].enabledStates[i] && newR[j] && (i <= length && !railingData[j].enabledStates[i + 1]))
						{
							if (i > 0 && i <= length)
							{
								railings[j][currentRailing[j]] = InstantiateRailingPiece(j, i, instantiatePosition);

								for (int k = 0; k < railing.prefab.transform.childCount; k++)
								{
									railings[j][currentRailing[j]].transform.GetChild(0).parent = railingTempParents[j][currentRailing[j]].transform.GetChild(k);
								}
								GameObject.DestroyImmediate(railings[j][currentRailing[j]]);
								incrementCurrentRailing[j] = true;
							}
						}
						//Next after end part of railing
						else if ((!railingData[j].enabledStates[i] || i > length) && newR[j])
						{
							if (newMR[j])
							{
								middlePart[j].transform.localScale = Vector3.Scale(middlePart[j].transform.localScale, new Vector3(1, 1, pieceLength[j]));

								for (int k = 0; k < railing.prefab.transform.childCount; k++)
								{
									MeshFilter mf = middlePart[j].transform.GetChild(0).gameObject.GetComponent<MeshFilter>();
									mf.sharedMesh = Instantiate(mf.sharedMesh);

									//Debug.Log(k + ", " + pieceLength[j]);
									middlePart[j].transform.GetChild(0).gameObject.ScaleUVs(new Vector2(pieceLength[j], 1));
									//Debug.Log(middlePart[j].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale);

									middlePart[j].transform.GetChild(0).parent = railingTempParents[j][currentRailing[j]].transform.GetChild(k);
								}

								GameObject.DestroyImmediate(middlePart[j]);

								middlePart[j] = null;

							}

							pieceLength[j] = 1;
							if(incrementCurrentRailing[j])
								currentRailing[j]++;
							newR[j] = false;
							newMR[j] = false;
						}
							
					}

					if (i > 0 && i <= length)
						instantiatePosition += belt.positionOffset;
				}

				if (belt.lengthMode == PCSPart.LengthMode.Repeat)
					instantiatePosition = startInstPos;
			}

			for (int i = 0; i < length; i++)
			{
				if (belt.lengthMode == PCSPart.LengthMode.Repeat)
				{
					//----------------------Instantiate belt-----------------------
					belt.gameObject = Instantiate(belt.prefab, transform);
					belt.gameObject.transform.localPosition = instantiatePosition;
					if (belt.mirror)
						belt.gameObject.transform.localScale = Vector3.Scale(belt.gameObject.transform.localScale, new Vector3(1, 1, -1));
					belt.gameObject.transform.parent = belt.parent.transform;
					//-------------------------------------------------------------
				}


				//---------------------Instantiate railings--------------------
				if (railing.lengthMode == PCSPart.LengthMode.Repeat)
				{
					for (int j = 0; j < 2; j++)
					{
						if (railingData[j].enabledStates[i + 1])
						{
							railings[j][currentRailing[j]] = InstantiateRailingPiece(j, i + 1, instantiatePosition);

							for (int k = 0; k < railing.prefab.transform.childCount; k++)
							{
								railings[j][currentRailing[j]].transform.GetChild(0).parent = railingTempParents[j][currentRailing[j]].transform.GetChild(k);
							}
							GameObject.DestroyImmediate(railings[j][currentRailing[j]]);
							newR[j] = true;
						}
						else if (newR[j])
						{
							currentRailing[j]++;
							newR[j] = false;
						}
					}
				}
				//-------------------------------------------------------------


				//Increase instantiate position
				if (railing.lengthMode == PCSPart.LengthMode.Repeat || belt.lengthMode == PCSPart.LengthMode.Repeat)
					instantiatePosition += belt.positionOffset;
			}
			//------------------------------------------------------------------------------------------------------------------------------------------------


			//Instantiate belt end cap
			endCap.gameObject = InstantiateCap(endCap.prefab, endCap.parent, instantiatePosition, endCap.mirror, "Belt End Cap");
			endCap.gameObject.hideFlags = HideFlags.HideInHierarchy;
			uvS[1] = endCap.gameObject.AddComponent<PCSUVScroller>();
			uvS[1].speed = speed / 0.2f;

			//-----------------Instantiate railing end cap-----------------
			railingTempParentsEndCap = new GameObject[2];
			for (int i = 0; i < 2; i++)
			{
				if (railingData[i].enabledStates[railingData[i].enabledStates.Count - 1])
					railingTempParentsEndCap[i] = InstantiateRailingPiece(i, railingData[i].enabledStates.Count - 1, instantiatePosition);

			}
			//-------------------------------------------------------------


			//--------------------Instantiate internals--------------------
			if (internalsEnabled)
			{
				GameObject internalPiece = Instantiate(internals.prefab);
				internalPiece.transform.parent = internals.parent.transform;
				internalPiece.transform.localPosition = PCSUtils.GetStartCapTopEdge(startCap.positionOffset);
				internalPiece.transform.localScale = Vector3.Scale(internalPiece.transform.localScale, new Vector3((width - 0.4f) / 1.6f, 1, 1));
				if (internals.mirror)
					internalPiece.transform.localScale = Vector3.Scale(internalPiece.transform.localScale, new Vector3(1, 1, -1));

				

				if (internalsCount > 2)
				{
					float dist = internals.positionOffset.z + belt.positionOffset.z * length; //Vector3.Distance(PCSUtils.GetStartCapTopEdge(startCap.positionOffset), PCSUtils.GetEndCapTopEdge(startCap.positionOffset, belt.positionOffset, length));
					//float gap = dist / (internalsCount - 2);
					//dist -= gap;
					float sep = dist / (internalsCount-1);
					for (int i = 1; i < internalsCount; i++)
					{
						internalPiece = Instantiate(internals.prefab);
						internalPiece.transform.parent = internals.parent.transform;
						internalPiece.transform.localPosition = PCSUtils.GetStartCapTopEdge(startCap.positionOffset) + new Vector3(0, 0, sep * i);
						internalPiece.transform.localScale = Vector3.Scale(internalPiece.transform.localScale, new Vector3((width - 0.4f) / 1.6f, 1, 1));
						//internalPiece.transform.localPosition += 
						if (internals.mirror)
							internalPiece.transform.localScale = Vector3.Scale(internalPiece.transform.localScale, new Vector3(1, 1, -1));
					}
				}
				else
				{
					internalPiece = Instantiate(internals.prefab);
					internalPiece.transform.parent = internals.parent.transform;
					internalPiece.transform.localPosition = PCSUtils.GetEndCapTopEdge(startCap.positionOffset, belt.positionOffset, length);
					internalPiece.transform.localScale = Vector3.Scale(internalPiece.transform.localScale, new Vector3((width - 0.4f) / 1.6f, 1, 1));
					if (!internals.mirror)
						internalPiece.transform.localScale = Vector3.Scale(internalPiece.transform.localScale, new Vector3(1, 1, -1));
				}
			}


			//-------------------------------------------------------------

		}

		GameObject InstantiateCap(GameObject capPrefab, GameObject capParent, Vector3 instantiatePosition, bool mirrorCap, string name)
		{
			GameObject cap = Instantiate(capPrefab, transform);
			cap.name = name;
			cap.transform.localPosition = instantiatePosition;
			cap.transform.localScale = Vector3.Scale(cap.transform.localScale, new Vector3((width - 0.4f) / 1.6f, 1, 1));
			if (mirrorCap)
				cap.transform.localScale = Vector3.Scale(cap.transform.localScale, new Vector3(1, 1, -1));
			cap.transform.parent = capParent.transform;

			return cap;
		}

		GameObject InstantiateRailingPiece(int side, int index, Vector3 instantiatePosition)
		{
			instantiatePosition = instantiatePosition + new Vector3(side == 0 ?  ((width - 0.4f)/2) + 0.1f : -(((width - 0.4f) / 2) + 0.1f), 0, 0);

			GameObject railingPiece;

			//Start Cap
			if (index == 0)
			{
				if (railingData[side].enabledStates[index + 1])
				{
					railingPiece = Instantiate(railingStartCap.prefab, transform);
					railingPiece.transform.localPosition = instantiatePosition;

					if (railingStartCap.mirror)
						railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, -1));
					else
						railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, 1));
				}
				else
				{
					railingPiece = Instantiate(railingDoubleCap.prefab, transform);
					railingPiece.transform.localPosition = instantiatePosition;

					if (railingStartCap.mirror)
						railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, -1));
					else
						railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, 1));
				}
			}
			//End Cap
			else if (index == railingData[side].enabledStates.Count - 1)
			{
				if (railingData[side].enabledStates[index - 1])
				{
					railingPiece = Instantiate(railingEndCap.prefab, transform);
					railingPiece.transform.localPosition = instantiatePosition;

					if (railingEndCap.mirror)
						railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, -1));
					else
						railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, 1));
				}
				else
				{
					railingPiece = Instantiate(railingDoubleCap.prefab, transform);
					railingPiece.transform.localPosition = instantiatePosition;

					if (railingEndCap.mirror)
						railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, -1));
					else
						railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, 1));
				}
			}
			//Regular railingPiece - double cap
			else if (!railingData[side].enabledStates[index - 1] && !railingData[side].enabledStates[index + 1])
			{
				railingPiece = Instantiate(railingDoubleCap.prefab, transform);
				railingPiece.transform.localPosition = instantiatePosition;

				if (railingDoubleCap.mirror)
					railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, -1));
				else
					railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, 1));
			}
			//Regular railingPiece - start cap
			else if (!railingData[side].enabledStates[index - 1])
			{
				railingPiece = Instantiate(railingStartCap.prefab, transform);
				railingPiece.transform.localPosition = instantiatePosition + Vector3.forward * railingStartCap.renderers[1].bounds.size.z;

				if (railingStartCap.mirror)
					railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, -1));
				else
					railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, 1));
			}
			//Regular railingPiece - end cap
			else if (!railingData[side].enabledStates[index + 1])
			{
				railingPiece = Instantiate(railingEndCap.prefab, transform);
				railingPiece.transform.localPosition = instantiatePosition;

				if (railingEndCap.mirror)
					railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, -1));
				else
					railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, 1));
			}
			//Regular railingPiece - middle piece
			else
			{
				railingPiece = Instantiate(railing.prefab, transform);
				railingPiece.transform.localPosition = instantiatePosition;

				if (railing.mirror)
					railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, -1));
				else
					railingPiece.transform.localScale = Vector3.Scale(railingPiece.transform.localScale, new Vector3(side == 0 ? 1 : -1, 1, 1));
			}

			return railingPiece;
		}

		void CombineMeshes()
		{
			//Merge belt
			belt.parent.CombineChildMeshes(belt.renderers[0].sharedMaterial);

			//Merge railing, part 1 - combine same children (on same side)
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < railingTempParents[i].Length; j++)
				{
					for (int k = 0; k < railingTempParents[i][j].transform.childCount; k++)
						railingTempParents[i][j].transform.GetChild(k).gameObject.CombineChildMeshes(railing.renderers[k].sharedMaterial);
				}
			}

			if (editMode == EditModes.None)
			{
				//Create railing colliders
				CreateColliders();
			}

			//Merge railing, part 2 - move colliders to parent
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < railingTempParents[i].Length; j++)
				{
					for (int k = 0; k < railing.prefab.transform.childCount; k++)
					{
						if (editMode == EditModes.None)
						{
							BoxCollider oldCollider = railingTempParents[i][j].transform.GetChild(0).GetComponent<BoxCollider>();
							visibleColliders.Add(railing.parent.AddDuplicateCollider(oldCollider));
							BoxCollider.DestroyImmediate(oldCollider);
						}

						railingTempParents[i][j].transform.GetChild(0).parent = railing.parent.transform.GetChild(k);
					}

					GameObject.DestroyImmediate(railingTempParents[i][j]);
				}
			}


			//Merge railing, part 3 - merge same children (both sides)
			for (int i = 0; i < railing.parent.transform.childCount; i++)
			{
				railing.parent.transform.GetChild(i).gameObject.CombineChildMeshes(railing.renderers[i].sharedMaterial);
			}


			//Merge railing start cap, part 1 - move colliders to parent
			for (int i = 0; i < 2; i++)
			{
				if (railingData[i].enabledStates[0])
				{
					for (int j = 0; j < railingStartCap.prefab.transform.childCount; j++)
					{
						if (editMode == EditModes.None)
						{
							BoxCollider oldCollider = railingTempParentsStartCap[i].transform.GetChild(0).GetComponent<BoxCollider>();
							visibleColliders.Add(railingStartCap.parent.AddDuplicateCollider(oldCollider));
							BoxCollider.DestroyImmediate(oldCollider);
						}

						railingTempParentsStartCap[i].transform.GetChild(0).parent = railingStartCap.parent.transform.GetChild(j);
					}
					GameObject.DestroyImmediate(railingTempParentsStartCap[i]);
				}
			}


			//Merge railing start cap, part 2 - merge same children (both sides)
			for (int i = 0; i < railingStartCap.prefab.transform.childCount; i++)
			{
				railingStartCap.parent.transform.GetChild(i).gameObject.CombineChildMeshes(railingStartCap.renderers[i].sharedMaterial);
			}

			
			//Merge railing end cap, part 1 - move colliders to parent
			for (int i = 0; i < 2; i++)
			{
				if (railingData[i].enabledStates[length + 1])
				{
					for (int j = 0; j < railingEndCap.prefab.transform.childCount; j++)
					{
						if (editMode == EditModes.None)
						{
							BoxCollider oldCollider = railingTempParentsEndCap[i].transform.GetChild(0).GetComponent<BoxCollider>();
							visibleColliders.Add(railingEndCap.parent.AddDuplicateCollider(oldCollider));
							BoxCollider.DestroyImmediate(oldCollider);
						}

						railingTempParentsEndCap[i].transform.GetChild(0).parent = railingEndCap.parent.transform.GetChild(j);
					}
					GameObject.DestroyImmediate(railingTempParentsEndCap[i]);
				}
			}

			
			//Merge railing end cap, part 2 - merge same children (both sides)
			for (int i = 0; i < railingEndCap.prefab.transform.childCount; i++)
			{
				railingEndCap.parent.transform.GetChild(i).gameObject.CombineChildMeshes(railingEndCap.renderers[i].sharedMaterial);
			}

			//Merge internals
			internals.parent.CombineChildMeshes(internals.renderers[0].sharedMaterial);
		}

		void CreateColliders()
		{
			//Railing colliders
			for (int i = 0; i < railing.prefab.transform.childCount; i++)
			{
				for (int j = 0; j < railingTempParents[0].Length; j++)
				{
					railingTempParents[0][j].transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
				}

				for (int j = 0; j < railingTempParents[1].Length; j++)
				{
					railingTempParents[1][j].transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
				}
			}

			//Railing start cap colliders
			for (int i = 0; i < railingStartCap.prefab.transform.childCount; i++)
			{
				if (railingData[0].enabledStates[0])
				{
					if (railingTempParentsStartCap[0].transform.localScale != Vector3.one)
						railingTempParentsStartCap[0].FixScale(railingStartCap.renderers);
					railingTempParentsStartCap[0].transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
				}

				if (railingData[1].enabledStates[0])
				{
					if (railingTempParentsStartCap[1].transform.localScale != Vector3.one)
						railingTempParentsStartCap[1].FixScale(railingStartCap.renderers);
					railingTempParentsStartCap[1].transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
				}
			}

			//Railing end cap colliders
			for (int i = 0; i < railingEndCap.prefab.transform.childCount; i++)
			{
				if (railingData[0].enabledStates[railingData[0].enabledStates.Count - 1])
				{
					if (railingTempParentsEndCap[0].transform.localScale != Vector3.one)
						railingTempParentsEndCap[0].FixScale(railingEndCap.renderers);
					railingTempParentsEndCap[0].transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
				}

				if (railingData[1].enabledStates[railingData[1].enabledStates.Count - 1])
				{
					if (railingTempParentsEndCap[1].transform.localScale != Vector3.one)
						railingTempParentsEndCap[1].FixScale(railingEndCap.renderers);
					railingTempParentsEndCap[1].transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
				}
			}
		}

		void CreatePhysicsComponenets()
		{
			BoxCollider beltCollider = belt.parent.AddComponent<BoxCollider>();
			beltCollider.size = new Vector3(beltCollider.size.x, beltCollider.size.y-0.01f, PCSUtils.GetBeltLength(belt.prefab, length, startCap.prefab, endCap.prefab));
			visibleColliders.Add(beltCollider);

			//MeshCollider cStart = startCap.gameObject.AddComponent<MeshCollider>();
			//cStart.convex = true;
			//visibleColliders.Add(cStart);

			//MeshCollider cEnd = endCap.gameObject.AddComponent<MeshCollider>();
			//cEnd.convex = true;
			//visibleColliders.Add(cEnd);

			BoxCollider conveyorCollider = physicsParent.AddComponent<BoxCollider>();
			conveyorCollider.size = new Vector3(belt.renderers[0].bounds.size.x * ((width - 0.4f) / 1.6f), 0.01f, PCSUtils.GetBeltLength(belt.prefab, length, startCap.prefab, endCap.prefab));
			conveyorCollider.center = belt.parent.transform.localPosition - new Vector3(0, 0.005f, 0);
			Rigidbody coveyorRB = physicsParent.AddComponent<Rigidbody>();
			coveyorRB.isKinematic = true;
			pcsC = physicsParent.AddComponent<PCSConveyor>();
			pcsC.speed = speed;

		}

		void ApplyTransform()
		{
			transform.position = parentTransform.position;
			transform.rotation = parentTransform.rotation;
			transform.localScale = parentTransform.scale;
		}

		int[] GetRailingCount()
		{
			int[] railingCount = { 0, 0 };
			for (int i = 0; i < 2; i++)
			{

				bool rail = false;
				for (int j = 1; j < railingData[i].enabledStates.Count - 1; j++)
				{
					if (!railingData[i].enabledStates[j] && rail)
					{

						rail = false;
					}
					if (railingData[i].enabledStates[j] && !rail)
					{
						railingCount[i]++;
						rail = true;
					}


				}
			}

			return railingCount;
		}

		void CreateRailingParents(int[] railingCount)
		{
			for (int i = 0; i < 2; i++)
			{
				railingTempParents[i] = new GameObject[railingCount[i]];
				for (int j = 0; j < railingCount[i]; j++)
				{
					railingTempParents[i][j] = new GameObject("tempRailingParent");
					railingTempParents[i][j].transform.parent = transform;
					railingTempParents[i][j].transform.localPosition = PCSUtils.GetBeltTopCenter(startCap.positionOffset, belt.positionOffset, length);

					for (int k = 0; k < railing.prefab.transform.childCount; k++)
					{
						GameObject child = new GameObject(railing.prefab.transform.GetChild(k).name);
						child.transform.parent = railingTempParents[i][j].transform;
						child.transform.localPosition = Vector3.zero;
					}
				}
			}
		}



		public void deleteAllColliders()
		{
			foreach (Collider c in GetComponents<Collider>())
			{
				DestroyImmediate(c);
			}
		}


		[ContextMenu("Attempt Fix")]
		public void Fix()
		{
			int childCount = transform.childCount;

			for (int i = 0, index =0; i < childCount; i++)
			{
				if (transform.GetChild(index).name == "Belt" || transform.GetChild(index).name == "Railing" || transform.GetChild(index).name == "Start Cap" || transform.GetChild(index).name == "End Cap" || transform.GetChild(index).name == "Internals" || transform.GetChild(index).name == "Physics")
					DestroyImmediate(transform.GetChild(index).gameObject);
				else
					index++;
			}

			deleteAllColliders();

			CreatePCS();
		}

		[ContextMenu("Reset Style Settings")]
		public void ResetSettings()
		{
			belt.prefab = null;
			startCap.prefab = null;
			endCap.prefab = null;
			railing.prefab = null; ;
			railingStartCap.prefab = null; 
			railingEndCap.prefab = null;
			railingDoubleCap.prefab = null;
			internals.prefab = null;
			settingsImported = false;
		}


		[ContextMenu("Delete All Children and Colliders")]
		public void deleteAllChildren()
		{
			//if (editMode == EditModes.Railings)
			//	editMode = EditModes.None;

			int childCount = transform.childCount;

			for (int i = 0; i < childCount; i++)
			{
				DestroyImmediate(transform.GetChild(0).gameObject);
			}

			//Debug.Log("Children Destroyed (" + transform.childCount + "/" + childCount + " Remaining)");

			foreach (Collider c in GetComponents<Collider>())
			{
				DestroyImmediate(c);
			}
		}

		public void EditRailings()
		{
			if (editMode == EditModes.Railings)
			{
				railing.parent.SetActive(false);
				railingStartCap.parent.SetActive(false);
				railingEndCap.parent.SetActive(false);
				railingEditCollidersSideIndex = new Dictionary<Collider, int>();
				railingEditCollidersRailingIndex = new Dictionary<Collider, int>();
			}
			else
			{
				railing.parent.SetActive(true);
				railingStartCap.parent.SetActive(true);
				railingEndCap.parent.SetActive(true);

			}
		}

		public void InstantiateMaterials()
		{
			MeshRenderer beltParentRenderer = belt.parent.GetComponent<MeshRenderer>();
			MeshRenderer startCapRenderer = startCap.gameObject.GetComponent<MeshRenderer>();
			MeshRenderer endCapRenderer = endCap.gameObject.GetComponent<MeshRenderer>();
			MeshRenderer internalsRenderer = internals.parent.GetComponent<MeshRenderer>();
			Material railingMat = Instantiate(railing.parent.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().sharedMaterial);


			beltParentRenderer.sharedMaterial = Instantiate(beltParentRenderer.sharedMaterial);
			//beltParentRenderer.sharedMaterial.mainTextureScale = new Vector2(1, length);
			belt.parent.ScaleUVs(new Vector2(width,length));
			//startCap.gameObject.transform.GetChild(1).gameObject.ScaleUVs(new Vector2(width, 1));
			//endCap.gameObject.transform.GetChild(1).gameObject.ScaleUVs(new Vector2(width, 1));

			startCapRenderer.sharedMaterial = Instantiate(startCapRenderer.sharedMaterial);
			endCapRenderer.sharedMaterial = Instantiate(endCapRenderer.sharedMaterial);

			internalsRenderer.sharedMaterial = Instantiate(internalsRenderer.sharedMaterial);

			foreach(MeshRenderer r in railing.parent.GetComponentsInChildren<MeshRenderer>())
			{
				if (r.sharedMaterial.name == "RailColor")
					r.sharedMaterial = railingMat;
			}

			foreach (MeshRenderer r in railingStartCap.parent.GetComponentsInChildren<MeshRenderer>())
			{
				if (r.sharedMaterial.name == "RailColor")
					r.sharedMaterial = railingMat;
			}

			foreach (MeshRenderer r in railingEndCap.parent.GetComponentsInChildren<MeshRenderer>())
			{
				if (r.sharedMaterial.name == "RailColor")
					r.sharedMaterial = railingMat;
			}

			railingMat.color = colour;
		}

		public void SetSpeed(float v)
		{
			if (pcsC != null)
			{
				pcsC.speed = v;

				for (int i = 0; i < 3; i++)
					uvS[i].speed = v / 0.2f;

				uvS[3].speed = v / (0.18f*Mathf.PI);



				speed = v;
			}
			else
				Debug.LogWarning("Cannot set conveyor speed before conveyor has been created");
		}

		private void OnDrawGizmos()
		{
			if (editMode == EditModes.Railings)
			{
				RenderRailingGizmos();
			}
		}

		void RenderRailingGizmos()
		{
			for (int i = 0; i < 2; i++)
			{
				if (railingData[i].enabledStates != null)
				{
					Vector3 instantiatePosition = startCap.positionOffset;
					for (int j = 0; j < railingData[i].enabledStates.Count; j++)
					{
						for (int k = 0; k < railing.prefab.transform.childCount; k++)
						{
							if (railing.renderers != null && k < railing.renderers.Length && railing.renderers[k] != null)
							{

								Gizmos.color = railingData[i].enabledStates[j] ? Color.green : Color.red;

								Vector3 centre = railing.renderers[k].bounds.center;
								Vector3 offset = new Vector3(((width - 0.4f) / 2) + 0.1f, 0 ,0);
								centre.x *= (i == 0 ? 1 : -1);
								offset.x *= (i == 0 ? 1 : -1);
								Vector3 position = instantiatePosition + centre + offset;
								Vector3 size = railing.renderers[k].bounds.size;

								Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
							
								Gizmos.DrawWireCube(position, size);
							}
						}
						instantiatePosition += belt.positionOffset;
					}
				}
			}
		}

	}
}