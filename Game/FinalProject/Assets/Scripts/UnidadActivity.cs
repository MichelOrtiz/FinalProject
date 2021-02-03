using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidadActivity : MonoBehaviour
{
    private enum PlayerState { Idle,Moving}
    private PlayerState stateUnit;
    private Animator animator;
    private Vector3 target;
    private RaycastHit2D hit;
    private Interactable focus;
    private float angle;
    public float speed = 2f;
    public float mov = 1f;
    #region Singleton
    public static UnidadActivity instance;
    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion
    public DialogueTrigger dialogue;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Es un inicio");
        animator = GetComponent<Animator>();
        angle = transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        #region Controles
        if (Input.GetKey(KeyCode.A))
        {
            target = transform.position;
            target.x -= mov;
            if (Input.GetKey(KeyCode.W))
            {
                target.y += mov;

            }
            if (Input.GetKey(KeyCode.S))
            {
                target.y -= mov;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            target = transform.position;
            target.x += mov;
            if (Input.GetKey(KeyCode.W))
            {
                target.y += mov;

            }
            if (Input.GetKey(KeyCode.S))
            {
                target.y -= mov;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            target = transform.position;
            target.y += mov;
            if (Input.GetKey(KeyCode.A))
            {
                target.x -= mov;

            }
            if (Input.GetKey(KeyCode.D))
            {
                target.x += mov;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            target = transform.position;
            target.y -= mov;
            if (Input.GetKey(KeyCode.A))
            {
                target.x -= mov;

            }
            if (Input.GetKey(KeyCode.D))
            {
                target.x += mov;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SearchInteraction();
            //dialogue.TriggerDialogue();
        }
        #endregion
        if (target == transform.position)
        {
            stateUnit = PlayerState.Idle;
        }
        else
        {
            stateUnit = PlayerState.Moving;
            FaceTarget();

        }
        
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        //UpdateAnima();
    }
    private void SearchInteraction()
    {
        Vector3 interactPoint = transform.position;
        interactPoint.x += 2f * Mathf.Cos(angle * Mathf.Deg2Rad);
        interactPoint.y += 2f * Mathf.Sin(angle * Mathf.Deg2Rad);
        hit = Physics2D.Raycast(transform.position, interactPoint, Vector2.Distance(transform.position, interactPoint));
        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, interactPoint, Color.red, 10f);
            Interactable interact = hit.collider.GetComponent<Interactable>();
            DialogueTrigger chat = hit.collider.GetComponent<DialogueTrigger>();
            QuestionTrigger what = hit.collider.GetComponent<QuestionTrigger>();
            if(interact != null)
            {
                SetFocus(interact);
                return;
            }
            if(chat != null)
            {
                SetFocus(chat);
                return;
            }
            if( what != null)
            {
                what.TriggerQuestion();
            }
            else
            {
                RemoveFocus();
            }
        }
        

    }
    private void UpdateAnima()
    {
        if (stateUnit == PlayerState.Moving)
        {
            animator.Play("Unidad_Moving");
        }
        if(stateUnit == PlayerState.Idle)
        {
            animator.Play("Unidad_Idle");
        }
    }
    private void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        focus.Fight();
    }
    private void SetFocus(DialogueTrigger newFocus)
    {
        newFocus.TriggerDialogue();
    }
    private void RemoveFocus()
    {
        focus = null;
    }
    private void FaceTarget()
    {
        Vector2 direction = target - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}
