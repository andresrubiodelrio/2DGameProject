using UnityEngine;
using UnityEngine.InputSystem;

public class ControlJugadorInputSystemNew : MonoBehaviour
{
    public InputAction MoveAction; //define una acción de entrada

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
        //La primera instrucción utiliza ReadValue para obtener el valor actual de la función MoveAction ,
        //en lugar de determinar si se está presionando una tecla o no, como hacía el código anterior.
        //El tipo de valor que se lee está contenido entre corchetes angulares(<>);
        //en este caso, es un valor Vector2 porque la entrada controla el movimiento en dos ejes.
        Vector2 move = MoveAction.ReadValue<Vector2>();

        Debug.Log(move);

        //La posición del objeto de juego PlayerCharacter se actualizará ahora en función de su posición anterior,
        //la dirección indicada por la tecla pulsada y un valor de incremento de movimiento de 0,01f .
        //Vector2 position = (Vector2)transform.position + move * 0.01f;
        Vector2 position = (Vector2)transform.position + move * 3.0f * Time.deltaTime;


        transform.position = position;
    }
}
