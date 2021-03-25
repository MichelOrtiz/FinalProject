using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFov : MonoBehaviour
{
    // Start is called before the first frame update
    private Mesh mesh;
    public Vector3 Origin { get; set; }
    public float FovAngle { get; set; }
    public float ViewDistance { get; set; }
    //public float AngleFromPlayer { get; set; }
    private float meshAngle;
    public FovType Fov { get; set; }
    private Material meshMaterial;
    public Material MeshMaterial
    {
        get { return meshMaterial; }
        set 
        {
            meshMaterial = value;
            GetComponent<MeshRenderer>().material = meshMaterial;
        }
    }
    
    private float startingAngle;
    [SerializeField] private LayerMask layerMask;

    /*public MeshFov()
    {
        FovAngle = 0;
        ViewDistance = 0;
    }*/

    public void Setup(float fovAngle, float viewDistance, Material meshMaterial, FovType fovType)
    {
        this.FovAngle = fovAngle;
        this.ViewDistance = viewDistance;
        this.MeshMaterial = meshMaterial;
        Fov = fovType;
        //SetAngle();

    }

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //origin = Vector3.zero;
        Origin = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        SetAngle();
        //mesh.RecalculateBounds();
        //Vector3 origin = Vector3.zero;
        //float angle = MathUtils.GetAngleBetween(this.transform.position, PlayerManager.instance.GetPosition());
        int rayCount = 12;
        //meshAngle = FovAngle;// / 1.5f;
        //meshAngle = (180 - FovAngle)/2;
        float angleIncrease = FovAngle / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount*3];

        vertices[0] = Origin;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            //RaycastHit2D raycastHit2D = Physics2D.Raycast(Origin, MathUtils.GetVectorFromAngle(meshAngle), ViewDistance);
            
            
            vertex = Origin + MathUtils.GetVectorFromAngle(meshAngle) * ViewDistance;
            /*if (raycastHit2D.collider == null)
            {
                vertex = origin + MathUtils.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }*/

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            
            vertexIndex++;
            meshAngle -= angleIncrease;
        }


        /*triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;*/

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private void SetAngle()
    {
        switch (Fov)
        {
            case FovType.CircularFront:
                meshAngle = FovAngle/2;
                break;
            case FovType.CircularUp:
                meshAngle = 90 + FovAngle/2;
                break;
            case FovType.CircularDown:
                meshAngle = 270 + FovAngle/2;
                break;
            case FovType.CompleteCircle:
                meshAngle = 0;
                FovAngle = 360;
                break;
            default:
                meshAngle = 0;
                FovAngle = 0;
                break;
        }
    }

    /*public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }*/

    /*public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = MathUtils.GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }*/
    
}
