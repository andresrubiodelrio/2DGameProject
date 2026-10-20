using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlEnemigo : MonoBehaviour
{
    public float velocidad = 2f;       // Unidades del mundo por segundo
    public Vector2 posicionFin;

    [SerializeField] private float tolerancia = 0.01f;

    private Rigidbody2D fisica;
    private Vector2 posicionInicio;
    private bool moviendoAFin = true;


    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        posicionInicio = fisica.position;
    }

    private void FixedUpdate()
    {
        if (velocidad <= 0f) return;

        Vector2 posicionObjetivo = moviendoAFin ? posicionFin : posicionInicio;

        if (Vector2.Distance(fisica.position, posicionObjetivo) <= tolerancia)
        {
            moviendoAFin = !moviendoAFin;
            posicionObjetivo = moviendoAFin ? posicionFin : posicionInicio;
        }

        Vector2 siguiente = Vector2.MoveTowards(
            fisica.position,
            posicionObjetivo,
            velocidad * Time.fixedDeltaTime
        );


        fisica.MovePosition(siguiente);
    }
}
