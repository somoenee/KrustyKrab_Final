
using UnityEngine;

public class BobCamera : MonoBehaviour
{
    [Header("Configuración de cámara")]
    public Transform jugador;
    public float sensibilidad = 200f;

    private float rotacionVertical = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float ratonX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float ratonY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        // Girar el personaje y la cámara horizontalmente
        jugador.Rotate(Vector3.up * ratonX);

        // Mirar arriba y abajo
        rotacionVertical -= ratonY;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -80f, 80f);

        transform.localRotation = Quaternion.Euler(rotacionVertical, 0f, 0f);

        // Liberar el ratón con Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Bloquear el ratón con clic
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

