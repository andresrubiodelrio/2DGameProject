using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugadorInputSystemNew : MonoBehaviour
{
    public InputAction MoveAction; //define una acción de entrada
    public float velocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveAction.Enable(); //La acción debe estar habilitada para procesar controles

        //QualitySettings.vSyncCount = 0;

        //Application.targetFrameRate = 10; //10 fps
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 move = MoveAction.ReadValue<Vector2>();

        Debug.Log(move);

        //La posición del objeto de juego PlayerCharacter se actualizará ahora en función de su posición anterior,
        //la dirección indicada por la tecla pulsada y un valor de incremento de movimiento de 0,01f .
        //Vector2 position = (Vector2)transform.position + move * 0.01f;
        Vector2 position = (Vector2)transform.position + move * velocity * Time.deltaTime;

        /*
         * • Sin Time.deltaTime: Tu código dice "mueve al jugador 3 metros cada vez que se dibuje un fotograma". 
         * Si un ordenador corre a 60 FPS, el jugador se moverá 3 * 60 = 180 metros en un segundo. 
         * Pero si otro ordenador corre a 120 FPS, ¡se moverá 3 * 120 = 360 metros en el mismo segundo! El juego va al doble de velocidad.
         * 
         * Con Time.deltaTime: Convertimos el movimiento en "mueve al jugador 3 metros por segundo".
         * 
         * Si estás usando componentes físicos como un Rigidbody y le cambias su velocidad directamente (rb.linearVelocity), 
         * no necesitas multiplicarlo, ya que el motor de físicas de Unity ya gestiona el tiempo de forma automática en el método FixedUpdate().
         * */


        transform.position = position;
    }
}
