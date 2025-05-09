using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHealth : MonoBehaviour {

    public static KnightHealth Instance; // instanciar para poder acessar todos os métodos públicos de qualquer outra classe
    private Animator animator; // pode fazer as animações
    public int vidas = 2; // quantidade de vidas do knight
    public float dieDuration; // duração da animação de morrer para o inimigo sumir depois

    private void Awake() { // garantir que so vai ter uma instancia
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        animator = GetComponent<Animator>();   
    }

    void Update() {
        
    }

    public void TakeDamage() {
        vidas--;

        if(vidas >= 1) { // quando so tiver recebido um ataque
            animator.SetTrigger("hurt"); // animcao de tomar dano
        }
        else { // quando for receber o segundo ataque
            animator.SetTrigger("death"); // animacao de morrer
            Invoke("Die", dieDuration); // sumir do mapa depois de um X tempo
        }

    }

    private void Die() {
        Destroy(gameObject); // destruir o inimigo depois de morrer
    }

}
