using UnityEngine;
public interface ILaser
{
    //void ShotLaser(Vector2 from, Vector2 to);    
    Transform ShotPos { get; }
    Vector2 EndPos { get; }
    void ShootLaser(Vector2 from, Vector2 to);
    void LaserAttack();
}