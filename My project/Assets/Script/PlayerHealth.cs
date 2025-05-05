using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public int vidas = 3;
    public Image[] iconesVidas; // arrays de imagens de vidas

    private void Awake() { // garantir que so vai ter uma instancia
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void TakeDamage() {
        vidas--;

        if (vidas >= 0 && vidas < iconesVidas.Length && iconesVidas[vidas] != null) {
            iconesVidas[vidas].enabled = false;
        }
        if (vidas <= 0) {
            Die();
        }
    }

    void Die() {
        PlayerPrefs.SetString("Fase", SceneManager.GetActiveScene().name); // pega o nome da cena atual e coloca na variavel Fase
        SceneManager.LoadScene("GameOver");
    }

}
