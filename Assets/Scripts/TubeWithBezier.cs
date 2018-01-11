using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BezierSpline))]
public class TubeWithBezier : MonoBehaviour {
	public float radius;
	public int circleVertices;
	public int rings;
	private Vector3[] vertices;
	private Mesh mesh;
	BezierSpline spline;

	void Awake(){
		spline = GetComponent<BezierSpline>();
		StartCoroutine(Generate());
	}

	private IEnumerator Generate () {
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";

		float x,y,z = 0, angle = 0, t = 0;

		vertices = new Vector3[circleVertices * (rings + 1)];
		for(int j = 0; j < rings + 1; j++){
			int start = j * circleVertices;
			t = (float)(j) / (float)rings;
			print(t);
			for(int i = start; i < start + circleVertices; i++){
				x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
				y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
				Vector3 direction = spline.GetVelocity(t).normalized;
				float xd = radius * direction.x * direction.y;
				float yd = radius * (1 - direction.y);
				float zd = radius * direction.z * direction.y;
				direction = new Vector3(xd, yd, zd);
				Vector3 point = spline.GetPoint(t) - transform.position;
				//print(point);
				vertices[i] = new Vector3(x,y,z) + point;// + direction;
				angle += 360 / circleVertices;
				yield return new WaitForSecondsRealtime(0.01f);
			}
		}
		mesh.vertices = vertices;

		int[] triangles = new int[(circleVertices) * (rings + 1) * 6];
		int vert = 0, vertRef = 0, circle = 0, total = 0;
		for(int r = 0; r < rings; r++){
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
