using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform John;

    // La cámara sigue a John solo en el eje X
    private void Update()
    {
        if (John == null) return;

        Vector3 position = transform.position;
        position.x = John.position.x;
        transform.position = position;
    }
}
