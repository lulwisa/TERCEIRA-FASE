using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinossaur : MonoBehaviour {

    public float speed = 2f; // velocidade do dinossauro
    public float stap = 5f; // quantidade de passos que ele vai dar até voltar
    private Vector3 startPosition; // posição inicial
    private int direction = 1; // direção atual: 1 = direita, -1 = esquerda

    void Start() {
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
}
