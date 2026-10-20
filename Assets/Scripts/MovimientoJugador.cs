using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovimientoJugador : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 3f;

    [SerializeField] private InputActionReference accionMover;

    [Header("Salto")]
    public float fuerzoSalto = 5f; //fuerza aplicada para saltar
    [SerializeField] private InputActionReference accionSaltar;
    [SerializeField] private LayerMask groundMask;       // Selecciona la capa "Suelo" aquí
    //[SerializeField] private float longRaycast;


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
    private float longRaycast = 1.6f;



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
            Debug.LogError("Asigna Acción Jugador/Saltar en Acción Saltar.", this);
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

        //Lanzamos un rayo hacia abajo desde el centro del personaje.
        // transform.position: Origen del rayo.
        // Vector2.down: Dirección del rayo hacia abajo.
        // 1.1f: Longitud del rayo (ajústala según el tamaño de tu Sprite/Collider).
        // groundMask: Capa del suelo para ignorar al propio jugador.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longRaycast, groundMask);

        // Si el rayo impactó contra algo en esa capa, 'hit.collider' no será nulo
        isGrounded = hit.collider != null;

        // 2. DIBUJAR EL RAYO VISUAL EN EL EDITOR
        // Definimos el color: Verde si toca el suelo, Rojo si está en el aire
        Color colorDelRayo = isGrounded ? Color.green : Color.red;

        // Dibujamos la línea en la pestaña Scene
        // Parámetros: Origen, Vector de dirección (Dirección * Longitud), Color
        Debug.DrawRay(transform.position, Vector2.down * longRaycast, colorDelRayo);

        //Debug.Log("Está tocando el suelo (Raycast 2D): " + isGrounded);

        /*  La variable saltoPendiente actúa como una nota que queda escrita hasta que FixedUpdate la consume. 
         *  Si entre dos pasos físicos hay un fotograma que detecta la pulsación y otro que no la detecta, la nota sigue siendo verdadera.
         */
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
            fisica.AddForce(Vector2.up * fuerzoSalto, ForceMode2D.Impulse);
        }

        //Como el salto ya ha sido atendido, deja de estar pendiente.
        saltoPendiente = false;
    }
}
