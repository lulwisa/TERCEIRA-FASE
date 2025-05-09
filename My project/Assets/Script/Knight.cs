using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {
    private Transform target; // variável para saber quem o inimigo vai perseguir
    public float speed; // velocidade do inimigo
    public float visionRadius; // radio de visão para o inimigo ver o player
    public GameObject attackPoint; // ponto de ataque que vai ser criado na Unity como filho do inimigo
    public float attackRadius; // raio para o ataque da espada
    public LayerMask Player; // saber a layer do player para atacar ele
    public float attackCooldown; // cooldown entre um ataque e outro
    private bool isAtacking = false; // saber se esta na animação de atacar ou não

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // targetar o player para ele seguir
    }

    void Update() {
        LookPlayer();

        if(this.target != null) { // se tiver um alvo
            float distance = Vector2.Distance(attackPoint.transform.position, target.position); // distancia entre o player e a espada do knight

            if(distance <= attackRadius) { // se tiver no alcance da espada
                Attack(); // ataca
            }
            else {
                FollowPlayer(); // segue o player
            }
        }
        else { // se não tiver um alvo
            StopMoving(); // para de se mover
        }
    }

    private void FollowPlayer() {
        Vector3 scale = transform.localScale; // pegar a posição
        if(target.position.x > transform.position.x) {
            scale.x = Mathf.Abs(scale.x);
        }
        else {
            scale.x = -Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime); // atualizar sua posição para seguir o player
    }

    private void StopMoving() { // função para ele ficar parado quando não ver o player
        // fica parado, não faz nada
    }

    private void OnDrawGizmos() { // apenas representação visual desse raio na unity para testar
        Gizmos.DrawWireSphere(this.transform.position, this.visionRadius);
        Gizmos.DrawWireSphere(attackPoint.transform.position, this.attackRadius);
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

    private void Attack() {
        if(isAtacking) {
            return; // se estiver atacando retorna
        }
        isAtacking = true; // seta para true

        Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRadius, Player);
        foreach (Collider2D playerGameObject in player) {
            PlayerHealth.Instance.TakeDamage();
        }
        Invoke("EndAttack", attackCooldown);
    }

    private void EndAttack() {
        isAtacking = false;
        // apenas setar como falso a animação de atacar
    }

}
