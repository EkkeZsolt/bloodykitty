using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // a Player Transformja
    public float smoothSpeed = 5f;  // milyen gyorsan kövesse
    public Vector3 offset;    // mennyivel legyen eltolva (pl. hogy ne legyen pont középen)

    void LateUpdate()
    {
        if (target == null)
            return;

        // célpozíció (a player + offset)
        Vector3 desiredPosition = target.position + offset;

        // sima mozgás (interpoláció)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}

