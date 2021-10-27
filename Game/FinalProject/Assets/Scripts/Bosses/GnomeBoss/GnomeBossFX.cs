using System.Collections.Generic;
using UnityEngine;

public class GnomeBossFX : MonoBehaviour
{
    #region Line
    [Header("Line")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform lineStartPos;
    #endregion

    #region FovReference
    [Header("FovReference")]
    [SerializeReference] private GnomeFov currentFov;
    [SerializeField] private List<string> fovNames;
    [SerializeField] private GameObject targetMesh;    
    private PlayerManager player;
    #endregion

    #region StateFx
    [Header("StateFx")]
    [SerializeField] private Color colorWhenEnabled;
    [SerializeField] private Color colorWhenDisabled;
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    #endregion

    void Start()
    {
        player = PlayerManager.instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        line.SetPosition(0, lineStartPos.position);
        if (currentFov == null)
        {
            FindCurrentFov();
        }
        else
        {
            if (currentFov.justAttacked)
            {
                line.enabled = false;
                spriteRenderer.color = colorWhenDisabled;
                animator.enabled = false;
            }
            else
            {
                if (!animator.enabled)
                {
                    animator.enabled = true;
                }
                line.enabled = true;
                spriteRenderer.color = colorWhenEnabled;
                line.SetPosition(1, targetMesh.transform.position); 

                spriteRenderer.flipX = player.GetPosition().x < transform.position.x;
            }
        }

    }


    void FindCurrentFov()
    {
        foreach (var fovName in fovNames)
        {
            currentFov = ScenesManagers.GetObjectsOfType<GnomeFov>().Find(f => f.name == fovName);
            targetMesh = currentFov?.GetComponentInChildren<MeshRenderer>().gameObject;
            if (currentFov != null) return;
        }
    }
}