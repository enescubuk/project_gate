using UnityEngine;

public class WindowTeleporter : MonoBehaviour
{
     public Transform player;
    public Transform reciver;
    private bool playerIsOverlapping = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBody")
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f)
            {
                print("yes");
                float rotationDiff = Quaternion.Angle(transform.rotation, reciver.rotation);
                //rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = reciver.position + positionOffset;


            }
            else
            {
                print(dotProduct);
            }
        }
    }
}
