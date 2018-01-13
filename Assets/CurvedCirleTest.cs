using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurvedCirleTest : MonoBehaviour {
	private Vector3[] vertices;
	public int circleVertices;
	public Transform[] rings;
	public float radius;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float x,y,z = 0, angle = 0, t = 0;
		int ringCount = rings.Length;
		vertices = new Vector3[circleVertices * (ringCount + 1)];
		for(int j = 0; j < ringCount; j++){
			int start = j * circleVertices;
			t = (float)(j) / (float)ringCount;
			//print(t);
			for(int i = start; i < start + circleVertices; i++){
				Vector3 direction = rings[j].position;
				x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
				y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
				vertices[i] = rings[j].TransformPoint(new Vector3(x,y,0)); 
				angle += 360 / circleVertices;
			}
		}
	}

	private void OnDrawGizmos () {
		if (vertices == null) {
			return;
		}
		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}
}
