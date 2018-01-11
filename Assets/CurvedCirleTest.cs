using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurvedCirleTest : MonoBehaviour {
	private Vector3[] vertices;
	public int circleVertices;
	public int rings;
	public float radius;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float x,y,z = 0, angle = 0, t = 0;

		vertices = new Vector3[circleVertices * (rings + 1)];
		for(int j = 0; j < rings + 1; j++){
			int start = j * circleVertices;
			t = (float)(j) / (float)rings;
			print(t);
			for(int i = start; i < start + circleVertices; i++){
				Vector3 direction = transform.forward;
				//direction = new Vector3(xd, yd, zd);
				x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);// * Mathf.Sin(angle2 * Mathf.Deg2Rad);
				y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);// * Mathf.Sin(angle2 * Mathf.Deg2Rad);
				//y = radius * Mathf.Cos(angle2 * Mathf.Deg2Rad);
				//z = radius * Mathf.Cos(angle2 * Mathf.Deg2Rad);
				
				Vector3 point = transform.position;//spline.GetPoint(t) - transform.position;
				//print(point);
				
				vertices[i] = Vector3.Cross(new Vector3(x,y,z), direction) + point;// + direction;
				angle += 360 / circleVertices;
				//yield return new WaitForSecondsRealtime(0.01f);
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
