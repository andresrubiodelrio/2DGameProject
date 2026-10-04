using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 3f;

    [SerializeField] private InputActionReference accionMover;

    private Rigidbody2D fisica;
    private Vector2 entradaMovimiento;

    
    
    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (accionMover == null || accionMover.action == null)
        {
            Debug.LogError(
                "Asigna Jugador/Mover en Accion Mover.", this);
            enabled = false;
            return;
        }

        accionMover.action.Enable();
    }

    private void OnDisable()
    {
        if (accionMover != null && accionMover.action != null)
        {
            accionMover.action.Disable();
        }

        entradaMovimiento = Vector2.zero;

        if (fisica != null)
        {
            fisica.linearVelocity =
                new Vector2(0f, fisica.linearVelocity.y);
        }
    }

    private void Update()
    {
        entradaMovimiento = accionMover.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        fisica.linearVelocity = new Vector2(
            entradaMovimiento.x * velocidad,
            fisica.linearVelocity.y
        );
    }
}
