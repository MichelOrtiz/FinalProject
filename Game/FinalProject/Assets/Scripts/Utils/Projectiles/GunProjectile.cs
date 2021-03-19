using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{
    public static GunProjectile instance;
    private void Awake() {
        if(instance!=null){
            return;
        }
        instance = this;
    }
    public Item prueba;
    public GameObject projectilePrefab;
    public Transform shotPoint;
    public float offset;
    private Vector3 mousePosition;
    private void Update() {
        mousePosition = GameCamera.instance.GetMousePosition();
        Vector3 dif = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(dif.y,dif.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f,0f,rotZ + offset);
        /* Cosas para probar
        if(Input.GetMouseButtonDown(0)){
            ShotObject(prueba);
        }
        */
    }
    public void StartAiming(){
        PlayerManager.instance.isAiming = true;
    }
    public void StopAiming(){
        PlayerManager.instance.isAiming = false;
    }
    
    public void ShotObject(Item item){
        GameObject projectile = Instantiate(projectilePrefab,shotPoint.position,transform.rotation);
        projectile.GetComponent<ObjProjectile>().SetItem(item);
    }
}
