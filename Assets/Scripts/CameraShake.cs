using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPos;
    private bool isShaking = false;
    private float currentMagnitude = 0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        if(isShaking)
        {
            float x = originalPos.x + Random.Range(-1f, 1f) * currentMagnitude;
            float y = originalPos.y + Random.Range(-1f, 1f) * currentMagnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
        }
    }

    public void SetShaking(bool shake, float magnitude = 0f)
    {
        isShaking = shake;
        currentMagnitude = magnitude;

        if(!isShaking)
        {
            transform.localPosition = originalPos;
        }
    }

    public void UpdateMagnitude(float newMagnitude)
    {
        currentMagnitude = newMagnitude;
    }
}
