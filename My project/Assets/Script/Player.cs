using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 5f; // velocidade personagem, public para poder mudar la no unity
    private Rigidbody2D rb2d; // Define o corpo rigido 2D que representa a raquete
    private Animator animar; // pegar a parte de animação
    public float jump = 10f; // é a força do pulo do personagem
    public bool isJumping; // saber se ele esta pulando ou nao
    public bool doubleJump; // saber se ele esta dando um pulo duplo ou nao
    public bool inStairs; // saber se o personagem esta em cima da escada
    public float speedClimb = 4f; // velocidade para escalar a escada


    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        animar = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        Mover();
        Pular();
        Escalar();
    }

    // aqui ele vai movimentar a partir do eixo X (no unity ja esta pre setado que essa movimentacao eh pelas teclas A, D ->, <-)
    void Mover() {
        Vector3 movement = new(Input.GetAxis("Horizontal"), 0f, 0f); // recebe as posicoes x, y e z -> nesse caso so a pos X vai mudar (jogo de plataforma)
        transform.position += movement * Time.deltaTime * speed;

        if (Input.GetAxis("Horizontal") > 0f) { // quando o personagem vai para a direita
            animar.SetBool("walk", true); // setar como true o parametro criado para andar
            transform.eulerAngles = new Vector3(0f, 0f, 0f); // mantem igual
        }
 
        if (Input.GetAxis("Horizontal") < 0f) { // quando o personagem vai para a esquerda
            animar.SetBool("walk", true); // setar como true o parametro criado para andar
            transform.eulerAngles = new Vector3(0f, 180f, 0f); // vira o personagem 180 graus para olhar para a esquerda
        }

        if (Input.GetAxis("Horizontal") == 0f) { // quando o personagem ta parado
            animar.SetBool("walk", false); // setar como false o parametro criado para andar
        }
    }

    void Pular() {
        if (Input.GetButtonDown("Jump")) { // ja eh pre setado como space
            if (isJumping == false) {
                rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse); // so muda o eixo Y (eixo X = 0f)
                doubleJump = true;
                animar.SetBool("jump", true); // setar como true o parametro criado para pular
            }
            else {
                if (doubleJump == true) {
                    rb2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
                    doubleJump = false; // assim nao pode pudar varias vezes
                }
            }
        }
    }

    // metodo para detectar sempre que o personagem tocar em alguma coisa
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) { // 6 é o layer que eu criei para Ground
            isJumping = false;
            animar.SetBool("jump", false); // setar como false o parametro criado para pular

        }
    }

    // metodo para detectar sempre que o personagem deixar de tocar em alguma coisa
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) { // 6 é o layer que eu criei para Ground
            isJumping = true;
        }
    }


    void Escalar() {
        if (inStairs == true) {
            Vector3 movement = new(0f, Input.GetAxis("Vertical"), 0f); // o getAxis vertical ja esta pre setado as teclas W, D, seta para cima e para baixo, entao ja faz tudo sozinho
            transform.position += movement * Time.deltaTime * speedClimb;
        }
    }


}