using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Es fundamental para cambiar de escena

public class PantallaJuego : MonoBehaviour
{
    // Escena de destino.
    private const string NombreEscenaMenu = "Menu inicial";

    /// <summary>
    /// Esta función pública cargará la escena del menú inicial.
    /// Debe ser asignada al evento OnClick() del botón "Ingresar".
    /// </summary>
    public void CargarMenuInicial()
    {
        // Usa SceneManager para cargar la escena de destino.
        SceneManager.LoadScene(NombreEscenaMenu);

        // Opcional: Para una carga más rápida si no hay mucha información
        // SceneManager.LoadScene("Menu inicial");
    }
}
public class PantallaJuego : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
