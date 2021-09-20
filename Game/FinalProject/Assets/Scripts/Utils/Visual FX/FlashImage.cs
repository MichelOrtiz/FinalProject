using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;


[RequireComponent(typeof(Image))]
public class FlashImage : MonoBehaviour
{
    [Header("General")]
    [SerializeField] bool loopOnEnable = false;
    [Range(0, 1)] [SerializeField] float startingAlpha = 0;
    [SerializeField] float secondsForOneFlash = 2f;
    public float SecondsForOneFlash
    {
        get { return secondsForOneFlash; }
        private set
        {
            if(value < 0)
            {
                value = 0;
            }
            secondsForOneFlash = value;
        }
    }
    private const float MIN_VALUE = 0.00001f;

    [Range(0, 1)]
    [SerializeField] float _minAlpha = 0f;
    public float MinAlpha
    {
        get { return _minAlpha; }
        private set
        {
            _minAlpha = Mathf.Clamp(value, 0, 1);
        }
    }
    [Range(0, 1)]
    [SerializeField] float _maxAlpha = 1f;
    public float MaxAlpha
    {
        get { return _maxAlpha; }
        private set
        {
            _maxAlpha = Mathf.Clamp(value, 0, 1);
        }
    }


    Coroutine flashRoutine = null;
    Image flashImage;

    // events
    public delegate void FlashStop();
    public event FlashStop FlashStopHandler;
    protected virtual void OnStop()
    {
        FlashStopHandler?.Invoke();
    }
    public delegate void CycleStart();
    public event CycleStart CycleStartHandler;
    protected virtual void OnCycleStart()
    {
        CycleStartHandler?.Invoke();
    }
    public delegate void CycleComplete();
    public event CycleComplete CycleCompleteHandler;
    protected virtual void OnCycleComplete()
    {
        CycleCompleteHandler?.Invoke();
    }
    public delegate void FlashInComplete();
    public event FlashInComplete FlashInCompleteHandler;
    protected virtual void OnFlashInComplete()
    {
        FlashInCompleteHandler?.Invoke();
    }

    /*public event Action OnStop = delegate{ };
    public event Action OnCycleStart = delegate { };
    public event Action OnCycleComplete = delegate { };
    public event Action OnFlashInComplete = delegate { };*/

    #region Initialization
    private void Awake()
    {
        flashImage = GetComponent<Image>();
        // initial state
        SetAlphaToDefault();
    }

    private void OnEnable()
    {
        if (loopOnEnable)
        {
            StartFlashLoop();
        }
    }

    private void OnDisable()
    {
        if(loopOnEnable)
        {
            StopFlashLoop();
        }
    }
    #endregion

    #region Public Functions
    public void Flash()
    {
        StopFlashRoutine();
        flashRoutine = StartCoroutine(FlashOnce(SecondsForOneFlash, MinAlpha, MaxAlpha));
    }

    public void Flash(float secondsForOneFlash, float minAlpha, float maxAlpha)
    {
        Debug.Log("Flash called");
        SetNewFlashValues(secondsForOneFlash, minAlpha, maxAlpha);
        StopFlashRoutine();
        flashRoutine = StartCoroutine(FlashOnce(SecondsForOneFlash, MinAlpha, MaxAlpha));
    }

    public void StartFlashLoop()
    {
        StopFlashRoutine();
        flashRoutine = StartCoroutine(FlashLoop(SecondsForOneFlash, MinAlpha, MaxAlpha));
    }
    public void StartFlashLoop(float secondsForOneFlash, float minAlpha, float maxAlpha)
    {
        SetNewFlashValues(secondsForOneFlash, minAlpha, maxAlpha);
        StopFlashRoutine();
        flashRoutine = StartCoroutine(FlashLoop(secondsForOneFlash, minAlpha, maxAlpha));
    }

    public void StopFlashRoutine()
    {
        if(flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
    }

    public void StopFlashLoop()
    {
        //flashImage.enabled = false;
        StopFlashRoutine();
        SetAlphaToDefault();

        //OnStop?.Invoke();
        OnStop();
    }
    #endregion

    #region Private Functions
    IEnumerator FlashOnce(float secondsForOneFlash, float minAlpha, float maxAlpha)
    {
            
        // half the flash time should be on flash in, the other half for flash out
        float flashInDuration = secondsForOneFlash / 2;
        float flashOutDuration = secondsForOneFlash / 2;

        //OnCycleStart?.Invoke();
        OnCycleStart();
        // flash in
        for (float t = 0f; t <= flashInDuration; t += Time.deltaTime)
        {
            Color newColor = flashImage.color;
            newColor.a = Mathf.Lerp(minAlpha, maxAlpha, t / flashInDuration);
            flashImage.color = newColor;
            yield return null;
        }
        //OnFlashInComplete?.Invoke();
        OnFlashInComplete();
        // flash out
        for (float t = 0f; t <= flashOutDuration; t += Time.deltaTime)
        {
            Color newColor = flashImage.color;
            newColor.a = Mathf.Lerp(maxAlpha, minAlpha, t / flashOutDuration);
            flashImage.color = newColor;
            yield return null;
        }

        //OnCycleComplete?.Invoke();
        OnCycleComplete();
    }

    IEnumerator FlashLoop(float secondsForOneFlash, float minAlpha, float maxAlpha)
    {
        // half the flash time should be on flash in, the other half for flash out
        float flashInDuration = secondsForOneFlash / 2;
        float flashOutDuration = secondsForOneFlash / 2;
        // start the flash cycle
        while (true)
        {
            OnCycleStart();
            // flash in
            for (float t = 0f; t <= flashInDuration; t += Time.deltaTime)
            {
                Color newColor = flashImage.color;
                newColor.a = Mathf.Lerp(minAlpha, maxAlpha, t / flashInDuration);
                flashImage.color = newColor;
                yield return null;
            }
            OnFlashInComplete();
            // flash out
            for (float t = 0f; t <= flashOutDuration; t += Time.deltaTime)
            {
                Color newColor = flashImage.color;
                newColor.a = Mathf.Lerp(maxAlpha, minAlpha, t / flashOutDuration);
                flashImage.color = newColor;
                yield return null;
            }
                
            OnCycleComplete();
        }
    }

    private void SetAlphaToDefault()
    {
        Color newColor = flashImage.color;
        newColor.a = startingAlpha;
        flashImage.color = newColor;
    }

    private void SetNewFlashValues(float secondsForOneFlash, float minAlpha, float maxAlpha)
    {
        if (secondsForOneFlash <= 0) secondsForOneFlash = MIN_VALUE;
        SecondsForOneFlash = secondsForOneFlash;
        MinAlpha = minAlpha;
        MaxAlpha = maxAlpha;
    }
    #endregion
}