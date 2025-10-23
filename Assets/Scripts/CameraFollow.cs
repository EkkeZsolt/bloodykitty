using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // a Player Transformja
    public float smoothSpeed = 5f;  // milyen gyorsan k�vesse
    public Vector3 offset;    // mennyivel legyen eltolva (pl. hogy ne legyen pont k�z�pen)

    void LateUpdate()
    {
        if (target == null)
            return;

        // c�lpoz�ci� (a player + offset)
        Vector3 desiredPosition = target.position + offset;

        // sima mozg�s (interpol�ci�)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}

