using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            Jugador scriptJugador = otro.GetComponent<Jugador>();
            if (scriptJugador != null)
            {
                scriptJugador.RecibirDano();
            }
        }
    }
}