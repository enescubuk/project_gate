using UnityEngine;
using UnityEngine.Events;

public class DoorTrigger : MonoBehaviour
{
    private bool isItOpen = false;
    private bool isPlayerNearby;
    [SerializeField] private GameObject openDoorMiniGamePanel;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            Debug.Log("Player is nearby");
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            Debug.Log("Player is not nearby");
            isPlayerNearby = false;
        }
    }
    
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!isItOpen)
            {
                Debug.Log("Player pressed E");
                OpenDoorMiniGame();
            }
            else
            {
                Debug.Log("Door is already open");
                MoveDoor();
            }
        }
    }

    private void OpenDoorMiniGame()
    {
        Debug.Log("Open door mini game");
        openDoorMiniGamePanel.SetActive(true);
    }

    public void CloseDoorMiniGame()
    {
        Debug.Log("Close door mini game");
        openDoorMiniGamePanel.SetActive(false);
        isItOpen = true;
        MoveDoor();
    }

    private void MoveDoor()
    {
        GetComponentInParent<DoorAnimation>().MoveDoor();
    }
}
