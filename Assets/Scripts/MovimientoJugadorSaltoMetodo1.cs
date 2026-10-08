using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugadorSaltoMetodo1 : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 3f;

    [SerializeField] private InputActionReference accionMover;

    [Header("Salto")]
    public float impulso = 5f; //fuerza aplicada para saltar
    [SerializeField] private InputActionReference accionSaltar;

    [Header("Chequeo de Suelo")]
    [SerializeField] private Transform groundCheck;     // El objeto vacío en los pies
    [SerializeField] private float groundDistance = 0.2f; // Radio de la esfera
    [SerializeField] private LayerMask groundMask;       // Selecciona la capa "Suelo" aquí


    /*
     *        Tabla comparativa
            Característica	                    [SerializeField] private	                public
            ¿Visible en el Inspector?	                Sí	                                Sí
            ¿Modificable por otros scripts?	        No (Solo este script)   	            Sí (Cualquier script puede leer/escribir)
            Principio de Diseño	                Mantiene el encapsulamiento	                Rompe el encapsulamiento si solo se quería ver en el editor
            Seguridad del Código	        Alta (Evita errores accidentales externos)	    Baja (Cualquier clase puede corromper el dato) 
     
            [SerializeField] private int maxHealth = 100; // Configurable en el Inspector

            // Propiedad pública de solo lectura para el resto de scripts
            public int MaxHealth => maxHealth; 
     
     */

    private Rigidbody2D fisica;
    private Vector2 entradaMovimiento;
    private Boolean saltoPendiente;
    private bool isGrounded;




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

        if (accionSaltar==null || accionSaltar.action == null)
        {
            Debug.LogError("Acción Saltar.", this);
            enabled = false;
            return;
        }

        accionSaltar.action.Enable();
    }

    private void OnDisable()
    {
        if (accionMover != null && accionMover.action != null)
        {
            accionMover.action.Disable();
        }

        entradaMovimiento = Vector2.zero;

        if(accionSaltar != null && accionSaltar.action != null)
        {
            accionSaltar.action.Disable();  
        }



        if (fisica != null)
        {
            fisica.linearVelocity =
                new Vector2(0f, fisica.linearVelocity.y);
        }
    }

    private void Update()
    {
        entradaMovimiento = accionMover.action.ReadValue<Vector2>();

        // Lanzamos la esfera invisible. Si colisiona con la capa seleccionada, devuelve true.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);

        /*  La variable saltoPendiente actúa como una nota que queda escrita hasta que FixedUpdate la consume. 
         *  Si entre dos pasos físicos hay un fotograma que detecta la pulsación y otro que no la detecta, la nota sigue siendo verdadera.
         */
        Debug.Log("Está tocando el suelo: " + isGrounded);

        if (accionSaltar.action.WasPressedThisFrame() && isGrounded)
        {
            saltoPendiente = true;
        }
    }

    private void FixedUpdate()
    {
        fisica.linearVelocity = new Vector2(
            entradaMovimiento.x * velocidad,
            fisica.linearVelocity.y
        );

        // Experimento temporal: todavía permite saltar en el aire.
        //Vector2.up es (0,1)
        if (saltoPendiente)
        {
            fisica.AddForce(Vector2.up * impulso, ForceMode2D.Impulse);
        }

        //Como el salto ya ha sido atendido, deja de estar pendiente.
        saltoPendiente = false;
    }

    // Opcional: Dibuja la esfera en el editor de Unity para poder calibrar el tamaño visualmente
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(groundCheck.position, groundDistance);
        }
    }
}
