using UnityEngine;

public class MakeCity : MonoBehaviour
{
    public GameObject[] buildings;

    void Start()
    {
        Perlin surface = new Perlin();
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        float scaleX = this.transform.localScale.x;
        float scaleZ = this.transform.localScale.z;

        for (int v = 0; v < vertices.Length; v++)
        {
            float perlinValue = surface.Noise(
                vertices[v].x * 2 + 0.1365143f,
                vertices[v].z * 2 + 1.21688f
            ) * 20;
            
            perlinValue = Mathf.Clamp(perlinValue, 0, 1);
            float density = perlinValue * (buildings.Length - 1);
            int buildingIndex = Mathf.FloorToInt(density);

            Instantiate(buildings[buildingIndex],
                new Vector3(vertices[v].x * scaleX, vertices[v].y, vertices[v].z * scaleZ),
                buildings[buildingIndex].transform.rotation);
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.gameObject.AddComponent<MeshCollider>();
    }
}