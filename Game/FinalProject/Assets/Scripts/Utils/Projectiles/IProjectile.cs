using UnityEngine;
using System.Collections;
public interface IProjectile
{
    void ProjectileAttack();
    void ShotProjectile(Transform from, Vector3 to);
}