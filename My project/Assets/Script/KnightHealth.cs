using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHealth : MonoBehaviour {

    public static KnightHealth Instance; // instanciar para poder acessar todos os métodos públicos de qualquer outra classe
    public int vidas = 2; // quantidade de vidas do knight

    private void Awake() { // garantir que so vai ter uma instancia
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        
    }

    void Update() {
        
    }

    public void TakeDamage() {
        vidas--;
    }

}
