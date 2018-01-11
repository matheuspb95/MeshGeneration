using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCircle : MonoBehaviour {
	public float radius;
	public int circleVertices;
	public List<Transform> points = new List<Transform>();

	void Awake(){
		StartCoroutine(Generate());
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private Vector3[] vertices;
	private Mesh mesh;
	private IEnumerator Generate () {
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";

		float x,y,z = 0, angle = 0;

		vertices = new Vector3[circleVertices * points.Count];
		for(int j = 0; j < points.Count; j++){
			int start = j * circleVertices;
			for(int i = start; i < start + circleVertices; i++){
				x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
				y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
				vertices[i] = new Vector3(x,y,z) + points[j].position;
				angle += 360 / circleVertices;
				yield return new WaitForSecondsRealtime(0.01f);
			}
			z++;
		}
		mesh.vertices = vertices;

		int[] triangles = new int[(circleVertices) * (points.Count) * 6];
		int vert = 0, vertRef = 0, circle = 0, total = 0;
		for(int r = 0; r < points.Count - 1; r++){
			circle = r * circleVertices;
			string result;
			for(vertRef = 0; vertRef < circleVertices - 1; vertRef++){
				vert = (vertRef * 6) + (circleVertices * 6) * r;
				result = "[";
				result += triangles[(0 + vert)] = vertRef + circle;
				result += triangles[(1 + vert)] = vertRef + 1 + circle;
				result += triangles[(2 + vert)] = circleVertices + vertRef + circle;
				result += "][";
				result += triangles[(3 + vert)] = vertRef + 1 + circle;
				result += triangles[(5 + vert)] = circleVertices + vertRef + circle;
				result += triangles[(4 + vert)] = vertRef + circleVertices + 1 + circle;
				//print(result + "]");
			}
			vert = (vertRef * 6) + (circleVertices * 6) * r;
			result = "[";
			result += triangles[(2 + vert)] = vertRef + circle;
			result += triangles[(1 + vert)] = vertRef + 1 + circle;
			result += triangles[(0 + vert)] = circle;
			result += "][";
			result += triangles[(4 + vert)] = vertRef + 1 + circle;
			result += triangles[(5 + vert)] = circleVertices + vertRef + circle;
			result += triangles[(3 + vert)] = vertRef + circle;
			//print(result + "]");
		}
		mesh.triangles = triangles;

		mesh.RecalculateNormals();
	}

	/*
	private void OnDrawGizmos () {
		if (vertices == null) {
			return;
		}
		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}
	 */

}
