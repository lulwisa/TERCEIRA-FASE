using System.Collections;
using UnityEngine;

// basicamente funcao para poder acessar as musicas de qualquer parte do jogo

public class MusicManager : MonoBehaviour {

    public static MusicManager Instance;

    [SerializeField]
    private MusicLibrary musicLibrary;
    [SerializeField]
    private AudioSource musicSource;
    

    private void Awake() {
        if (Instance != null) { // se a musica ja estiver selecionada
            Destroy(gameObject); // destroy para nao duplicar
        }
        else { // se nao estiver selecionada
            Instance = this; // ativa a musica
            DontDestroyOnLoad(gameObject); // ter certeza que nao vai ser destruida na troca de cenas
        }
    }

    // funcao para tocar a musica
    public void PlayMusic(string trackName, float fadeDuration = 0.5f) {
        StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetClipFromName(trackName), fadeDuration));
    }

    // funcao para parar de tocar a musica
    public void StopMusic() {
        if (musicSource.isPlaying) {
        musicSource.Stop();
        }
    }



    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f) {
        float percent = 0; // a musica basicamente vai indo de 0 a 1
        while (percent < 1) {
            percent += Time.deltaTime * 1 / fadeDuration; // o fade é a transição suave no volume da música ao trocá-la

            musicSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;
        }

        musicSource.clip = nextTrack; // quando tocar esse fade troca a musica
        musicSource.Play();

        percent = 0;
        while (percent < 1) {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;
        }
    }
 

}