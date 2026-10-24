using UnityEngine;

public class Triggers : MonoBehaviour
{

    [SerializeField] private string targetTag = "Player";
    [SerializeField] private Transform objectToMove;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private bool shakeCameraWhileMoving = true;
    [SerializeField] private float maxShakeMagnitude = 0.3f;
    [SerializeField] private float maxShakeDistance = 10f;

    private bool isTriggered = false;
    private bool isMoving = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(targetTag) && !isTriggered)
        {
            isTriggered = true;
            isMoving = true;

            if(shakeCameraWhileMoving && CameraShake.Instance != null)
            {
                CameraShake.Instance.SetShaking(true, 0f);
            }
        }
    }

    private void Update()
    {
        if(isTriggered && isMoving && objectToMove != null && targetPosition != null) 
        {
            objectToMove.position = Vector2.MoveTowards(objectToMove.position, targetPosition.position,
            moveSpeed * Time.deltaTime);     
            
            if(shakeCameraWhileMoving && playerTransform != null && CameraShake.Instance != null)
            {
                float currentDistance = Vector2.Distance(objectToMove.position, playerTransform.position);

                float intensity = 1f - Mathf.Clamp01(currentDistance / maxShakeDistance);

                float calculatedMagnitude = intensity * maxShakeMagnitude;

                CameraShake.Instance.UpdateMagnitude(calculatedMagnitude);
            }
            if(Vector2.Distance(objectToMove.position, targetPosition.position) < 0.01f)
            {
                isMoving = false;

                if(shakeCameraWhileMoving && CameraShake.Instance != null)
                {
                    CameraShake.Instance.SetShaking(false);
                }
            }
        }

    }
}
