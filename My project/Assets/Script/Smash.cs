using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour {

    public float speedDown; // velocidade pra descer
    public float speedUp; // velocidade para subir
    public float smashDistance; // distância total que ele vai percorrer
    public float timePaused; // tempo que o esmagador fica parado quando bater no chão
    private bool isPaused = false; // saber se ele esta parado ou não quando bate
    private bool isSmashing = true; // saber se ele esta esmagando ou voltando para o topo; true = movimento de esmagar
    private float startY; // posição inicial Y do meu esmagador
    private float bottomLimit; // limite inferior
    private Animator anim; // fazer as animações

    void Start() {
        anim = GetComponent<Animator>(); // setar animação

        startY = transform.position.y; // guardar a posição inicial
        bottomLimit = startY - smashDistance; // guardar o limite inferior

        anim.SetBool("up", false);
    }

    void Update() {
        if(isPaused) { // se estiver pausado
            return; // não move o esmagador
        }

        float time = Time.deltaTime; // pegar o tempo desde o último frame
        Vector3 position = transform.position; // pegar a posição toda atual

        if(isSmashing) { // se estiver no movimento de esmagar
            anim.SetBool("up", false);
            position.y -= speedDown * time; // vai descendo o esmagador
            if(position.y <= bottomLimit) { // se chegar no limite inferior
                position.y = bottomLimit; // ele para ali
                isSmashing = false; // e começa a subir
            }
        }
        else {
            position.y += speedUp * time; // vai subindo o esmagador
            anim.SetBool("up", true);
            if(position.y >= startY) { // se alcançar a posição inicial do esmagador
                position.y = startY; // ele para ali
                isSmashing = true; // e volta a esmagar
            }
        }
        transform.position = position; // aplica a nova posição
    }

    void OnCollisionEnter2D(Collision2D collision) { // detectar colisao com o chão
        if(isSmashing && collision.gameObject.layer == 6) { // 6 é a layer para chão (ground)
            StartPause();
            isSmashing = false; // começa a subir
        }
        if(collision.gameObject.tag == "Player") { // se colidir com o player
            StartPause();
            isSmashing = false; // volta a subir
        }
    }

    private void StartPause() {
        anim.SetTrigger("hit"); // setar animação de quando esmaga
        isPaused = true; // pausa o esmagador
        Invoke("EndPause", timePaused); // chama a função de despausar depois de um determinado tempo
    }

    private void EndPause() {
        isPaused = false; // despausa o esmagador
    }

}
