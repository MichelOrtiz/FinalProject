using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFov : MonoBehaviour
{
    // Start is called before the first frame update
    private Mesh mesh;
    private Vector3 origin;
    private float fov;
    private float viewDistance;
    private float startingAngle;
    [SerializeField]
    private LayerMask layerMask;


    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 origin = Vector3.zero;
        int rayCount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount*3];

        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, MathUtils.GetVectorFromAngle(angle), viewDistance, layerMask);

            if (raycastHit2D.collider == null)
            {
                vertex = origin + MathUtils.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            
            vertexIndex++;
            angle -= angleIncrease;
        }


        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = MathUtils.GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }

    public void SetFov(float fov)
    {
        this.fov = fov;
    }
    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

    
}
