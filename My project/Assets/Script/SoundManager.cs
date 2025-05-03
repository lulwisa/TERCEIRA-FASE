using UnityEngine;

// basicamente vai tocar os nossos sound effects

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance; // instancia para pode acessar essa classe em qualquer outra classe

    // boa prática de código: deixa privado para outras classes não poderem acessar mas permite mudar no inspect
    [SerializeField] // permite manipular a variável no inspector
    private SoundLibrary soundLibrary;
    [SerializeField]
    private AudioSource sound2DSource;

    private void Awake() { // verifica se não tem mais de um mesmo sound effect tocando
        if (Instance != null) { // se já tiver
            Destroy(gameObject); // destrói para não ter duplicatas
        }
        else { // se não tiver
            Instance = this; // instancia o sound 
            DontDestroyOnLoad(gameObject); // não destrói em troca de cenas
        }
    }

    // toca música em 3d
    public void PlaySound3D(AudioClip clip, Vector3 pos) {
        if (clip != null) { // verifica se o clip existe
            AudioSource.PlayClipAtPoint(clip, pos); // o som spawna naquele ponto, ou seja, quanto mais longe estiver mais baixo fica
        }
    }

    // toca música em 3d
    public void PlaySound3D(string soundName, Vector3 pos) {
        PlaySound3D(soundLibrary.GetClipFromName(soundName), pos);
    }

    // toca música em 2d
    public void PlaySound2D(string soundName) {
        sound2DSource.PlayOneShot(soundLibrary.GetClipFromName(soundName));
    }

}