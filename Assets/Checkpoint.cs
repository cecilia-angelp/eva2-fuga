using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Checkpoint : MonoBehaviour
{
    void Awake()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = new Vector2(1f, 2f);
        col.offset = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player") || otro.GetComponent<Jugador>() != null || otro.name.Contains("character"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ActualizarCheckpoint(transform.position);
            }
        }
    }
}