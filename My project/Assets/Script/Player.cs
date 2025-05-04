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
    public int lifes = 3;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Move();
        Jump();
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

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) { // 6 é o layer que eu criei para Ground
            isJumping = false;
        }
        if(collision.gameObject.tag == "Lava"){ // se ele cai na lava
            //Destroy(GameObject.FindGameObjectWithTag("Player"));
            PlayerPrefs.SetString("Pre Historia", SceneManager.GetActiveScene().name); // pega o nome da cena atual e coloca na variavel Pre Historia
            SceneManager.LoadScene("GameOver");
        }
    }

    // metodo para detectar sempre que o personagem deixar de tocar em alguma coisa
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) { // 6 é o layer que eu criei para Ground
            isJumping = true;
        }
    }


}
