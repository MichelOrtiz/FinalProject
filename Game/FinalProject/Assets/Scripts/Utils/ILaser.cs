using UnityEngine;
public interface ILaser
{
    //void ShotLaser(Vector2 from, Vector2 to);    
    public Transform ShotPos { get; }
    void ShootLaser(Vector2 from, Vector2 to);
    void LaserAttack();
}