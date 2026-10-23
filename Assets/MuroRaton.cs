using UnityEngine;

public class MuroRaton : MonoBehaviour
{
    public ComportamientoRaton elRaton; // Casillero para vincular a la rata

    // Usamos OnCollisionEnter2D porque el muro ahora es sólido
    private void OnCollisionEnter2D(Collision2D choque)
    {
        // Detectar si impacta la estrella (Bala)
        if (choque.gameObject.CompareTag("Bala"))
        {
            // Despertar al ratón
            if (elRaton != null)
            {
                elRaton.despierto = true;
            }

            Destroy(gameObject); // Destruir el muro
            Destroy(choque.gameObject); // Destruir la estrella
        }
    }
}