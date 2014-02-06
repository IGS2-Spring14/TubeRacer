using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SplineCombiner : MonoBehaviour 
{
	public GameObject[] pathSegments;
	public bool computePath = false;
	public bool eraseChildren = false;

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
		if(eraseChildren == true)
		{
			eraseChildren = false;
			Debug.Log("Destroying transforms");
			foreach(Transform xform in transform)
			{
				DestroyImmediate(xform.gameObject);
			}
			
		}

		if(computePath == true)
		{
			computePath = false;
			CombineSegments();
		}
	}

	void CombineSegments()
	{
		//Debug.Log("Combining " + pathSegments.Length + " segments");
		for(int i = 0; i < pathSegments.Length; i++)
		{
			GameObject segment = pathSegments[i];

			//Debug.Log("  Adding " + segment.transform.childCount + " nodes");
			foreach(Transform childNode in segment.transform)
			{
				var go = new GameObject();
				go.transform.parent = transform;
				go.transform.position = childNode.position;
				go.transform.rotation = childNode.rotation;
				go.name = i + "_" + childNode.gameObject.name;
			}
		}
	}
}
