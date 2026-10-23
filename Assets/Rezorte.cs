using UnityEngine;

public class Rezorte : MonoBehaviour
{
    public float fuerzaSalto = 15f; // Cambia este número en Unity para saltar más alto o más bajo

    private void OnTriggerEnter2D(Collider2D otro)
    {
        // Verifica si el objeto que tocó el resorte tiene la etiqueta "Player"
        if (otro.CompareTag("Player"))
        {
            Rigidbody2D rbJugador = otro.GetComponent<Rigidbody2D>();
            if (rbJugador != null)
            {
                // Mantiene la velocidad horizontal y aplica la fuerza de salto hacia arriba
                rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, fuerzaSalto);
            }
        }
    }
}