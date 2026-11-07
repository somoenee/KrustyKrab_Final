using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BobMovement : MonoBehaviour
{
    [Header("Configuración de movimiento")]
    public float velocidad = 5f;
    public float gravedad = -9.81f;
    public float velocidadRotacion = 10f;

    [Header("Animaciones")]
    public Animator animador;

    private CharacterController controlador;
    private float velocidadVertical;
    private float velocidadMovimiento;

    void Start()
    {
        controlador = GetComponent<CharacterController>();

        if (animador == null)
            animador = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GestionarMovimiento();
        GestionarGravedad();
        ActualizarAnimacion();
    }

    void GestionarMovimiento()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Compatibilidad con WASD y ZQSD
        if (Input.GetKey(KeyCode.Q)) horizontal = -1;
        if (Input.GetKey(KeyCode.D)) horizontal = 1;
        if (Input.GetKey(KeyCode.Z)) vertical = 1;
        if (Input.GetKey(KeyCode.S)) vertical = -1;

        // Movimiento relativo a la orientación del personaje
        Vector3 direccion = transform.right * horizontal
                          + transform.forward * vertical;

        direccion.y = 0;
        direccion.Normalize();

        velocidadMovimiento = new Vector2(horizontal, vertical).magnitude;

        // Mover el personaje sin girarlo con las teclas
        controlador.Move(direccion * velocidad * Time.deltaTime);
    }

    void GestionarGravedad()
    {
        if (controlador.isGrounded && velocidadVertical < 0)
            velocidadVertical = -2f;

        velocidadVertical += gravedad * Time.deltaTime;
        controlador.Move(Vector3.up * velocidadVertical * Time.deltaTime);
    }

    void ActualizarAnimacion()
    {
        if (animador != null)
            animador.SetFloat("Speed", velocidadMovimiento);
    }
}
