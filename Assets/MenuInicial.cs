using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuInicial : MonoBehaviour
{
    public void CambiarAMision()
    {
        SceneManager.LoadScene("3.Misión");

    }

    public void CambiarAInstrucciones()
    {
        SceneManager.LoadScene("4.Instrucciones");

    }
    public void CambiarAManejadores()
    {
        SceneManager.LoadScene("5.Manejadores");

    }
    public void CambiarACampoDeJuego()
    {
        SceneManager.LoadScene("6.Campo de juego");

    }
    public void CambiarAPantallaJuego()
    {
        SceneManager.LoadScene("1.Pantalla Juego");

    }

}
