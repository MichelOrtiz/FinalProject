using UnityEngine;
using System.Collections.Generic;
public class FollowerObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private int maxFollowers;
    private List<float> angles;
    private List<FollowerObject> followers;

    void Awake()
    {
        angles = new List<float>();
        followers = new List<FollowerObject>();
        
        float angleBtw = 360 / maxFollowers;
        float prevAngle = 0;
        for (int i = 0; i < maxFollowers; i++)
        {
            angles.Add(prevAngle);
            prevAngle += angleBtw;
        }
    }

    void Update()
    {
        if (followers != null)
        {
            int index = 0;
            foreach (var follower in followers)
            {
                if (follower != null)
                {
                    follower.transform.position = target.transform.position + MathUtils.GetVectorFromAngle(follower.angle) * follower.distance;
                    index++;
                }
                else
                {
                    followers.Remove(follower);
                    break;
                }
            }
        }
    }

    public void AddFollower(FollowerObject follower)
    {
        if (followers.Count < angles.Count)
        {
            float angle = 0;
            if (followers.Count > 0)
            {
                float lastAngle = followers[followers.Count - 1].angle;
                angle = angles[angles.IndexOf(lastAngle) + 1];
            }

            follower.angle = angle;
            follower.target = target;
            
            followers.Add(Instantiate(follower, target.transform.position + MathUtils.GetVectorFromAngle(angle) * follower.distance, follower.transform.rotation));
        }
    }

    public void DestroyAllFollowers()
    {
        //ScenesManagers.GetObjectsOfType<FollowerObject>().FindAll(f => f.target == target);
        int index = 0;
        while(followers.Count > 0)
        {
            if (followers[index] != null)
            {
                Destroy(followers[index].gameObject);
                followers.Remove(followers[index]);
            }
        }
    }
}