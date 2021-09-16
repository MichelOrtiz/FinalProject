using UnityEngine;
using System.Collections.Generic;
using MEC;

namespace FinalProject.Assets.Scripts
{
    [RequireComponent(typeof(Canvas))]
    public class OffscreenIndicators : MonoBehaviour
    {
        public Camera activeCamera;

        public List<Indicator> targetIndicators;
        public GameObject defaultIndicator;
        private Transform _transform;

        public float checkTime = 0.1f;
        public Vector2 offset;

        void Start()
        {
            _transform = transform;

            InstantiateIndicators();
            Timing.RunCoroutine(UpdateIndicators().CancelWith(gameObject));
        }

        public void AddTargetIndicator(GameObject targetIndicator)
        {
            targetIndicators.Add(new Indicator()
            {
                target = targetIndicator.transform, 
            });
            Debug.Log(targetIndicator);
            InstantiateIndicators();
        }

        public void InstantiateIndicators()
        {
            foreach (var targetIndicator in targetIndicators)
            {
                if (targetIndicator.indicatorUI == null)
                {
                    targetIndicator.indicatorUI = Instantiate(defaultIndicator).transform;
                    targetIndicator.indicatorUI.SetParent(_transform);
                }

                var rectTransform = targetIndicator.indicatorUI.GetComponent<RectTransform>();
                if (rectTransform == null)
                {
                    rectTransform = targetIndicator.indicatorUI.gameObject.AddComponent<RectTransform>();
                }
                targetIndicator.rectTransform = rectTransform;
            }
        }

        private void UpdatePosition(Indicator targetIndicator)
        {
            if (targetIndicator.target == null)
            {
                return;
            }
            var rect = targetIndicator.rectTransform.rect;
            var indicatorPos = activeCamera.WorldToScreenPoint(targetIndicator.target.position);

            var newPos = new Vector3(indicatorPos.x, indicatorPos.y);

            indicatorPos.x = Mathf.Clamp(indicatorPos.x, rect.width / 2, rect.width - rect.width / 2) + offset.x;
            indicatorPos.y = Mathf.Clamp(indicatorPos.y, rect.width / 2, rect.width - rect.width / 2) + offset.y;

            targetIndicator.indicatorUI.up = (newPos - indicatorPos).normalized;
            targetIndicator.indicatorUI.position = indicatorPos;
        }

        private IEnumerator<float> UpdateIndicators()
        {
            while(true)
            {
                foreach (var targetIndicator in targetIndicators)
                {
                    UpdatePosition(targetIndicator);
                }
                yield return Timing.WaitForSeconds(checkTime);
            }
        }


    }

    [System.Serializable]
    public class Indicator
    {
        public Transform target;
        public Transform indicatorUI;
        [HideInInspector] public RectTransform rectTransform;
    }
}