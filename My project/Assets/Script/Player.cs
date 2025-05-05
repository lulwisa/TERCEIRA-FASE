using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    public float speed = 5f; // velocidade personagem, public para poder mudar la no unity    public float JumpForce; // velocidade do pulo
    private Rigidbody2D rb2d; // me permite manipular qualquer variavel no rigidbody la do inspector
    public bool isJumping; // saber se ele esta pulando ou nao
    public bool doubleJump; // saber se ele esta dando um pulo duplo ou nao
    public float jump = 10f; // é a força do pulo do personagem
    public bool inStairs; // saber se o personagem esta em cima da escada
    public float speedClimb = 4f; // velocidade para escalar a escada
    public int lifes = 3; // quantidade de vidas

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Move();
        Jump();
        Climb();
    }

    void Move() {
        // O GetAxis ja detecta a movimentacao e teclas, ta pronta na Unity
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); // recebe apenas movimentacao lateral (x) - y e z ficam em 0
        transform.position += movement * Time.deltaTime * speed; // adiciona velocidade
    }

    void Jump() {
        if (Input.GetButtonDown("Jump")) { // ja eh pre setado como space
            if (isJumping == false) {
                rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse); // so muda o eixo Y (eixo X = 0f)
                doubleJump = true;
            }
            else {
                if (doubleJump == true) {
                    rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                    doubleJump = false; // assim nao pode pudar varias vezes
                }
            }
        }
    }

    void Climb() {
        if (inStairs) {
            Vector3 movement = new Vector3(0f, Input.GetAxis("Vertical"), 0f); // recebe apenas movimentacao lateral (x) - y e z ficam em 0
            transform.position += movement * Time.deltaTime * speedClimb; // adiciona velocidade
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) { // 6 é o layer que eu criei para Ground
            isJumping = false;
        }
        if(collision.gameObject.tag == "Lava"){ // se ele cair na lava
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse); // para dar um pulinho depois de bater
            PlayerHealth.Instance.TakeDamage();
        }
        if(collision.gameObject.tag == "Espinho") { // se ele cair nos espinhos
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse); // para dar um pulinho depois de bater
            PlayerHealth.Instance.TakeDamage();
        }
    }

    // metodo para detectar sempre que o personagem deixar de tocar em alguma coisa
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) { // 6 é o layer que eu criei para Ground
            isJumping = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) { // trigger eh quando o personagem pode passar por cima no objeto (se fosse uma bola nao teria trigger por exemplo)
        if(collider.gameObject.layer == 7) { // 7 é o layer criado para Escadas
            inStairs = true; // variavel na escada passa a ser verdadeiro
            rb2d.gravityScale = 0f; // deixo a gravidade como 0 para ele nao ficar descendo sozinho
        }
    }

    void OnTriggerExit2D(Collider2D collider) { // trigger eh quando o personagem pode passar por cima no objeto (se fosse uma bola nao teria trigger por exemplo)
        if(collider.gameObject.layer == 7) { // 7 é o layer criado para Escadas
            inStairs = false; // variavel na escada passa a ser falso
            rb2d.gravityScale = 3f; // deixo a gravidade como 3, que é a padrão do jogo
        }
    }


}
