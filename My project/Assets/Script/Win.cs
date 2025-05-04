using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    void Start() {
        
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) { // Botão esquerdo do mouse
            SoundManager.Instance.PlaySound2D("MouseClick");
        }
    }

    public void Victory() {
        SceneManager.LoadScene("Menu");
    }
}
