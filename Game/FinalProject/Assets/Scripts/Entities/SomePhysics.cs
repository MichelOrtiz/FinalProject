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

	public void Shake(float amount, float duration)
	{
		//InvokeRepeating("BeginShake", 0, 0.01f);
		//Invoke("StopShake", length);
		Vector2 transformPos = 	rigidbody2d.transform.position;

		StartCoroutine(BeginShake(amount, duration, transformPos));
	}

	private IEnumerator BeginShake(float amount, float duration, Vector2 transformPos)
	{
		if (amount > 0)
		{
			float curTime = 0;

			while (curTime < duration)
			{
				curTime += Time.deltaTime;
				float offsetX = UnityEngine.Random.value * amount * 2 - amount;
				float offsetY = UnityEngine.Random.value * amount * 2 - amount;
				
				transformPos.x += offsetX;
				transformPos.y += offsetY;

				rigidbody2d.transform.position = transformPos; 
				yield return null;
			}
		}
	}

	/*private void BeginShake(float amount)
	{
		if (amount > 0)
		{
			Vector2 transformPos = 	rigidbody2d.transform.position;
			float offsetX = UnityEngine.Random.value * amount * 2 - amount;
			float offsetY = UnityEngine.Random.value * amount * 2 - amount;
			
			transformPos.x += offsetX;
			transformPos.y += offsetY;

			rigidbody2d.transform.position = transformPos; 
		}
	}*/

	private void StopShake()
	{
		CancelInvoke("BeginShake");
		rigidbody2d.transform.localPosition = Vector3.zero;
	}
}