using UnityEngine;
public class PointerArrowReferenceSelf : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<PointerArrow>().OriginalReference = gameObject;
    }
}