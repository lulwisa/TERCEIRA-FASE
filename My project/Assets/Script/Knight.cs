using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {
    private Transform target; // variável para saber quem o inimigo vai perseguir
    public float speed; // velocidade do inimigo
    public float visionRadius; // radio de visão para o inimigo ver o player

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // targetar o player para ele seguir
    }

    void Update() {
        LookPlayer();
        if(this.target != null) { // se tiver um alvo
            FollowPlayer(); // segue o player
        }
        else { // se não tiver um alvo
            StopMoving(); // para de se mover
        }
    }

    private void FollowPlayer() {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime); // atualizar sua posição para seguir o player
    }

    private void StopMoving() { // função para ele ficar parado quando não ver o player
        // fica parado, não faz nada
    }

    private void OnDrawGizmos() { // apenas representação visual desse raio na unity para testar
        Gizmos.DrawWireSphere(this.transform.position, this.visionRadius);
    }

    private void LookPlayer() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, visionRadius); // pega todos os colisores presentes na cena -> player, ground etc
        target = null; // seta como null

        foreach (var hit in hits) { // percorre todos os colisores
            if (hit.CompareTag("Player")) { // se um dos colisores for player
                target = hit.transform; // seta o targer para o player
                break; // não precisa mais percorrer a lista
            }
        }
    }


}
