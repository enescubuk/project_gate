using UnityEngine;

public class SpriteBilbordSccript : MonoBehaviour
{
    public GameObject character;
    void Update()
    {
        transform.LookAt(character.transform);
    }
}
