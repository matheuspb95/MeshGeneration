using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTeste : MonoBehaviour {
	private Vector3[] vert;
	int[] tris;
	private Mesh mesh;

	void Awake(){
		StartCoroutine(Generate());
	}

	private IEnumerator Generate () {
		yield return null;
		
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";

		vert = new Vector3[5];
		vert[0] = Vector3.forward;
		vert[1] = Vector2.left;
		vert[2] = Vector2.up;
		vert[3] = Vector2.right;
		vert[4] = Vector2.down;

		tris = new int[] {
			0,1,2,
			0,2,3,
			0,3,4,
			0,4,1,
			0,1,2
		};

		mesh.vertices = vert;
		mesh.triangles = tris;		
	}

	private void OnDrawGizmos () {
		if (vert == null) {
			return;
		}
		Gizmos.color = Color.black;
		for (int i = 0; i < vert.Length; i++) {
			Gizmos.DrawSphere(vert[i], 0.1f);
		}
	}
}
