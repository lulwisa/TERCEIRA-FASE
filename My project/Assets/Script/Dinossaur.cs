using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinossaur : MonoBehaviour {

    public float speed = 2f; // velocidade do dinossauro
    public float stap = 5f; // quantidade de passos que ele vai dar até voltar
    private Vector3 startPosition; // posição inicial
    private int direction = 1; // direção atual: 1 = direita, -1 = esquerda
    private Rigidbody2D rb2d; // me permite manipular qualquer variavel no rigidbody la do inspector
    private Animator anim; // para fazer a animação de morrer
    public Transform headPoint; // objeto que fica em cima para saber se o personagem bateu na cabeça do dinossauro
    public BoxCollider2D boxCollider2D; // para desabilitar depois que morrer

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPosition = transform.position; // pega a posição inicial
    }

    void Update() {
        Move();
    }

    void Move() {
        transform.position += Vector3.right * direction * speed * Time.deltaTime; // move o dinossauro na direção atual
        float distanceFromStart = transform.position.x - startPosition.x; // calcula a distancia total percorrida

        if (Mathf.Abs(distanceFromStart) >= stap - 0.01f) { // se a distancia percorrida for maior que a distancia setada
            direction *= -1; // inverte direção

            Vector3 scale = transform.localScale; // variavel para a escala
            scale.x *= -1; // multiplica por -1 para fazer com que o dinossauro vire a cabeça
            transform.localScale = scale; // atualiza o valor da escala

            startPosition = transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D col) { // detectar colisao com o player
        if (col.gameObject.tag == "Player") {
            float height = col.contacts[0].point.y - headPoint.position.y; // saber se ta batendo na cabeca mesmo

            if (height > 0) { // se estiver batendo na cabeca
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse); // para dar um pulinho depois de bater
                speed = 0; // fazer ele parar de andar
                anim.SetTrigger("death"); // setar animação de morrer
                boxCollider2D.enabled = false; // desabilitar colisão
                rb2d.bodyType = RigidbodyType2D.Kinematic; // setar como kinematic para o dino não cair do mapa
                Destroy(gameObject, 0.25f); // destruir dino depois de 0.25sec
            }
        }
    }

}
