using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public AudioClip sonidoBoton;

    public void EmpezarJuego()
    {
        if (sonidoBoton != null)
        {
            AudioSource.PlayClipAtPoint(sonidoBoton, Vector3.zero);
        }
        SceneManager.LoadScene("SampleScene");
    }
}