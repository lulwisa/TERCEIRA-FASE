using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Plataform : MonoBehaviour {

    public float fallingTime; // tempo que vai demorar para cair quando o player pisar
    private TargetJoint2D target; // instanciar o target joint para desativar ele depois
    private TilemapCollider2D tilemapCollider2D; // instanciar para remover os colisores depois

    void Start() {
        target = GetComponent<TargetJoint2D>();
        tilemapCollider2D = GetComponent<TilemapCollider2D>();
    }

    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") { // se colidir com o player
            Invoke("Falling", fallingTime); // chama a função depois de um tempo
        }
    }

    void Falling() { // coloca tudo que vai acontecer depois que o player subir na plataforma
        target.enabled = false; // desativar o target joint
        tilemapCollider2D.isTrigger = true; // isso para a plataforma cair e sair do mapa
    }

}
