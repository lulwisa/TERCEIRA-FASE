using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public AudioMixer audioMixer; // instancia de audiomixer do unity
    public Slider musicSlider; // referencia ao slider de música
    public Slider soundSlider; // referencia ao slider de sound

    void Start() {
        LoadVolume();
        MusicManager.Instance.PlayMusic("MainMenu"); // toca a musica do menu
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) { // Botão esquerdo do mouse
            SoundManager.Instance.PlaySound2D("MouseClick");
        }
    }

    public void Play() { // quando clicar no botao de Play:
        SceneManager.LoadScene("Pre Historia"); // troca de cena
        MusicManager.Instance.StopMusic(); // para de tocar a musica
    }

    public void Quit() { // quando clicar no botao de Quit:
        Application.Quit();
    }

    // atualiza o volume da música
    public void UpdateMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume); // seta o valor lá nos parâmetros do audio mixer
    }

    // // atualiza o volume do sound effect
    public void UpdateSoundVolume(float volume) {
        audioMixer.SetFloat("SoundVolume", volume); // seta o valor lá nos parâmetros do audio mixer
    }

    // salvar o volume do jogo, sempre que reiniciado se manterá o mesmo de antes
    public void SaveVolume() {
        audioMixer.GetFloat("MusicVolume", out float musicVolume); // armazena o volume atual em uma variável
        PlayerPrefs.SetFloat("MusicVolume", musicVolume); // salva o valor dessa variável nas preferências do usuário

        audioMixer.GetFloat("SoundVolume", out float soundVolume);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
    }

    // carrega nos sliders os volumes já salvos na unity do usuário
    public void LoadVolume() {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
    }

}