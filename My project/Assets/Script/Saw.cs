using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {

    public float speed; // velocidade da serra
    public float moveTime; // dizer o tempo que ele vai andar em cada direção
    private bool rightDirection = true; // direção da serra, que começa indo para a direita
    private float timer; // apenas um timer para saber o tempo de jogo

    void Start() {
        
    }

    void Update() {
        if(rightDirection) { // se verdadeiro
            transform.Translate(Vector2.right * speed * Time.deltaTime); // a serra vai para a direita
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else {
            transform.Translate(Vector2.left * speed * Time.deltaTime); // a serra vai para a esquerda
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        timer += Time.deltaTime; // pega o deltaTime que é o tempo de jogo atual
        if(timer >= moveTime) { // se o tempo atual passar o limite de tempo que a serra anda
            rightDirection = !rightDirection; // troca de direção
            timer = 0f; // zera o timer
        }
        
    }
}
