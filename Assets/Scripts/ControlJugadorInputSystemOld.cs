using UnityEngine;

//Indicamos que este componente requiere un componente Rigidbody2D para funcionar correctamente.
[RequireComponent(typeof(Rigidbody2D))]
public class ControlJugador : MonoBehaviour
{
    public float velocidad;              

    private Rigidbody2D fisica;
    private float entradaX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //entradaX = Input.GetAxis("Horizontal"); //Valor entre -1 y 1
        ////Movemos al jugador a derecha/izquierda a una determinada velocidad en función de la pulsación de cursor (izq/dcha) o de las teclas a/d.
        ////Calcula el componente horizontal de la velocidad; con entradaX = 1 y velocidad = 3, resulta 3 unidades/s.
        ////Además, Conserva la velocidad vertical que ya tiene el cuerpo, incluida la caída por gravedad.
        //fisica.linearVelocity = new Vector2(
        //    entradaX * velocidad,
        //    fisica.linearVelocity.y
        //);
    }

    /*
     *  Update se ejecuta una vez por fotograma (su frecuencia varía según los FPS del dispositivo), 
     *  Si el juego va a 60 FPS, se ejecuta 60 veces por segundo; si baja a 30 FPS, se ejecuta 30 veces.
     *  Si lo usamos para control de movimiento  Necesitas multiplicar los movimientos por Time.deltaTime para que la velocidad sea uniforme en cualquier computadora.
     *  Se suele usar para leer la entrada del teclado/mouse (Input), mover elementos mediante Transform (sin físicas) o cambiar interfaces de usuario.
     *  
     *  */

    //Método que se suele usar cuando se quieren hacer movimientos de físicas.
    //Se usa este método en lugar del Update porque en el caso de usar Update puede provocar pequeños saltos en el movimiento lineal. Con FixedUpdate se evita.
    /*
     * FixedUpdate se ejecuta en intervalos de tiempo constantes y fijos (independientemente de los fotogramas por segundo).  
     * (por defecto, cada 0.02 segundos o 50 veces por segundo)
     * Independiente de los FPS: No se ve afectado por las bajadas o subidas de rendimiento gráfico.
     * Cuándo usarlo: Exclusivamente para cálculos de físicas, aplicar fuerzas o modificar componentes Rigidbody
     * */
    private void FixedUpdate()
    {
        //Ver Project Setting -> InputManager -> opción Horizontal.
        entradaX = Input.GetAxis("Horizontal"); //Valor entre -1 y 1
        //Movemos al jugador a derecha/izquierda a una determinada velocidad en función de la pulsación de cursor (izq/dcha) o de las teclas a/d.
        //Calcula el componente horizontal de la velocidad; con entradaX = 1 y velocidad = 3, resulta 3 unidades/s.
        //Además, Conserva la velocidad vertical que ya tiene el cuerpo, incluida la caída por gravedad.
        fisica.linearVelocity = new Vector2(
            entradaX * velocidad,
            fisica.linearVelocity.y
        );

    }


    //void Update()
    //{
    //    /*
    //     *Aquí tienes una explicación de esta línea de código:


    //    La variable es de tipo Vector2 . El tipo Vector2 puede almacenar dos valores numéricos, por lo que es ideal para coordenadas 2D.
    //    `transform.position` le indica al ordenador que almacene los valores X e Y de la propiedad `Position` del componente `Transform` . Puedes usar el punto (o operador punto) para acceder a los datos de otro objeto o componente. Esta instrucción almacena la posición actual del objeto de juego.
    //    El punto y coma (;) le indica al ordenador que la instrucción ha finalizado. Si no lo incluyes, aparecerá un mensaje de error en la ventana de la consola del editor de Unity y el código no funcionará. 
    //     */
    //    Vector2 position = transform.position;

    //    /*
    //     *Esta instrucción establece una nueva posición horizontal (eje x) para el GameObject.
    //    La nueva posición para la coordenada es la posición actual ( posición.x ) más 0.1. f
    //    indica un número con una posición decimal (llamado número de punto flotante ). 
    //     */
    //    position.x = position.x + 0.1f;

    //    /*
    //     *Esta instrucción establece la propiedad Posición en el componente Transformar utilizando su variable de posición.
    //        Anteriormente, las actualizaciones que realizabas se almacenaban en tu variable, pero no se aplicaban al GameObject. Esta instrucción hará que el personaje del jugador se mueva. 
    //     */
    //    transform.position = position;
    //}
}
