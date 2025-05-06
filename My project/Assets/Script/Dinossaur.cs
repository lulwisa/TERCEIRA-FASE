using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinossaur : MonoBehaviour {

    public float speed = 2f; // velocidade do dinossauro
    public float stap = 5f; // quantidade de passos que ele vai dar até voltar
    private float leftLimit; // x mínimo
    private float rightLimit; // x máximo
    private int direction = 1; // direção atual: 1 = direita, -1 = esquerda
    private Rigidbody2D rb2d; // me permite manipular qualquer variavel no rigidbody la do inspector
    private Animator anim; // para fazer a animação de morrer
    public Transform headPoint; // objeto que fica em cima para saber se o personagem bateu na cabeça do dinossauro
    public BoxCollider2D boxCollider2D; // para desabilitar depois que morrer
    private bool isDead = false; // saber se ja morrer e nao colidir duas vezes

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        float startX = transform.position.x; // pega a posicao inicial em X
        leftLimit  = startX; // seta o limite da esquerda como a posição inicial
        rightLimit = startX + stap; // seta o limite da direita como a posição final
    }

    void Update() {
        Move();
    }

    void Move() {
        float newX = transform.position.x + direction * speed * Time.deltaTime; // calcula o X

        // se ultrapassar o limite da direita, limita e inverte
        if (direction == 1 && newX >= rightLimit) {
            ReverseDirection();
            newX = rightLimit;
        }
        // se ultrapassar o limite da esquerda, limita e inverte
        else if (direction == -1 && newX <= leftLimit) {
            ReverseDirection();
            newX = leftLimit;
        }
        transform.position = new Vector3(newX, transform.position.y, transform.position.z); // atualiza a posição
    }

    private void ReverseDirection() { // inverter a direção do dino
        direction *= -1; // inverte direção
        Vector3 scale = transform.localScale; // variavel para a escala
        scale.x *= -1; // multiplica por -1 para fazer com que o dinossauro vire a cabeça
        transform.localScale = scale; // atualiza o valor da escala
    }

    void OnCollisionEnter2D(Collision2D collision) { // detectar colisao com o player
        if(isDead) {
            return;
        }
        if(collision.gameObject.tag == "Player") { // se acertar o player
            ReverseDirection();
            PlayerHealth.Instance.TakeDamage(); // recebe dano
        }   
    }

    public void Die() {
        if(isDead) {
            return;
        }
        isDead = true;
        speed = 0; // fazer ele parar de andar
        anim.SetTrigger("death"); // setar animação de morrer
        boxCollider2D.enabled = false; // desabilitar colisão
        rb2d.bodyType = RigidbodyType2D.Kinematic; // setar como kinematic para o dino não cair do mapa
        Destroy(gameObject, 0.25f); // destruir dino depois de 0.25sec
    }

}