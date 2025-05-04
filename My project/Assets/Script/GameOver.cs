using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameOver : MonoBehaviour {

    void Start() {
        
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) { // Botão esquerdo do mouse
            SoundManager.Instance.PlaySound2D("MouseClick");
        }
    }

    public void Restart() {
        string lastLevel = PlayerPrefs.GetString("Pre Historia", "Fase1");
        SceneManager.LoadScene(lastLevel);
    }

    public void Menu() {
        SceneManager.LoadScene("Menu");
    }

}
