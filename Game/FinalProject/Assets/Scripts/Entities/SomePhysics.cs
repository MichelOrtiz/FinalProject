using UnityEngine;
using System;
using System.Collections;

public class SomePhysics : MonoBehaviour
{
	[SerializeField] private Entity entity;
	[SerializeField] private Rigidbody2D rigidbody2d;
	private Vector2 position;

    public void StartKnockback(float knockbackDuration, float knockbackForce, Vector2 knockbackDir)
    {
        StartCoroutine(Knockback(knockbackDuration, knockbackForce, knockbackDir));
    }

    public IEnumerator Knockback(float knockbackDuration, float knockbackForce, Vector2 knockbackDir)
	{
		float knockbackTimer = 0;
		SetEnableEntity(entity, false);
		while (knockbackTimer < knockbackDuration)
		{
			knockbackTimer += Time.deltaTime;
			rigidbody2d.velocity = knockbackDir * knockbackForce;
			yield return null; // yield for a frame
		}
		SetEnableEntity(entity, true);
	}

    private void SetEnableEntity(Entity entity, bool enable)
    {
		if (entity != null)
		{
			if (entity == PlayerManager.instance)
			{
				PlayerManager.instance.inputs.enabled = enable;
			}
			else
			{
				entity.enabled = enable;
			}
		}
    }
}