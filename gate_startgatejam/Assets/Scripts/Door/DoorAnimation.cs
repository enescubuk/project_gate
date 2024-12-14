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
    
    private void Start()
    {
        CurrentState = DoorState.Idle;
    }
    private void OpenDoor()
    {
        StartCoroutine(RotateDoor(new Vector3(0, 90, 0)));
    }

    private void GoToMiddle()
    {
        StartCoroutine(RotateDoor(new Vector3(0, 0, 0)));
    }

    private void CloseDoor()
    {
        StartCoroutine(RotateDoor(new Vector3(0, -90, 0)));
    }

    public void MoveDoor()
    {
        switch (CurrentState)
        {
            case DoorState.Idle:
                OpenDoor();
                CurrentState = DoorState.Open;
                break;
            case DoorState.Open:
                GoToMiddle();
                CurrentState = DoorState.Idle;
                break;
        }
    }

    private IEnumerator RotateDoor(Vector3 targetRotation)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float duration = 1.0f; // Duration of the rotation in seconds
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }

}
