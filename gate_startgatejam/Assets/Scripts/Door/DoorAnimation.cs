using System.Collections;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public enum DoorState
    {
        Open,
        Idle,
    }

    public DoorState CurrentState { get; private set; }
    private bool isAnimating = false; // Kapının animasyon halinde olup olmadığını kontrol eder.

    private void Start()
    {
        CurrentState = DoorState.Idle;
    }

    private void OpenDoor()
    {
        if (isAnimating) return; // Animasyon sırasında yeni girişleri engelle
        StartCoroutine(RotateDoor(new Vector3(0, -90, 0), DoorState.Open));
        SoundManager.Instance.PlaySFX(SoundManager.Instance.doorOpenClip);
    }

    private void CloseDoor()
    {
        if (isAnimating) return; // Animasyon sırasında yeni girişleri engelle
        StartCoroutine(RotateDoor(new Vector3(0, 0, 0), DoorState.Idle));
        SoundManager.Instance.PlaySFX(SoundManager.Instance.doorCloseClip);
    }

    public void MoveDoor()
    {
        if (isAnimating) return; // Animasyon devam ederken tetiklemeyi engelle

        switch (CurrentState)
        {
            case DoorState.Idle:
                OpenDoor();
                break;
            case DoorState.Open:
                CloseDoor();
                break;
        }
    }

    private IEnumerator RotateDoor(Vector3 targetRotation, DoorState endState)
    {
        isAnimating = true; // Animasyonun başladığını işaretle

        // Kapının dönüşünü tanımla
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float duration = 1.0f; // Animasyon süresi
        float elapsed = 0.0f;

        // Sürüklenme sesi çal
        SoundManager.Instance.PlaySFX(SoundManager.Instance.doorDragClip);

        // Kapının yumuşak bir şekilde dönmesi
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation; // Döndürme işlemini tamamlama
        CurrentState = endState; // Kapının yeni durumunu ayarla
        isAnimating = false; // Animasyonun bittiğini işaretle
    }
}
