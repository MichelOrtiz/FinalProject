using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MBPasswordView : MonoBehaviour
{
    [SerializeField] private Sprite up;
    [SerializeField] private Sprite down;
    [SerializeField] private Sprite left;
    [SerializeField] private Sprite right;

    [SerializeField] private password password;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetImages(List<DireccionMiniJuegoMeduf> directions)
    {
        Debug.Log("password set");
        byte index = 0;
        List<Image> images = ScenesManagers.GetComponentsInChildrenList<Image>(gameObject);
        images.ForEach(i => i.enabled = false);
        foreach (var direction in directions)
        {
            switch (direction)
            {
                case DireccionMiniJuegoMeduf.Arriba:
                    images[index]. sprite = up;
                    break;
                case DireccionMiniJuegoMeduf.Abajo:
                    images[index].sprite = down;
                    break;
                case DireccionMiniJuegoMeduf.Izquierda:
                    images[index].sprite = left;
                    break;
                case DireccionMiniJuegoMeduf.Derecha:
                    images[index].sprite = right;
                    break;
            }
            images[index].enabled = true;
            index++;
        }
        images.FindAll(i => i.sprite == null).ForEach(i => i.enabled = false);
    }


    public void RemoveImage(byte index)
    {
        List<Image> images = ScenesManagers.GetComponentsInChildrenList<Image>(gameObject);
        images[index].enabled = false;
    }

}
