using UnityEngine;

// vai basicamente conter todas as musicas que iremos usar no decorrer do jogo
// lembrando que as musicas devem ser adicionadas la no inspector do AudioManager

[System.Serializable] // ver o inspector
public struct MusicTrack {
    public string trackName; // nome da musica
    public AudioClip clip; // clip
}
public class MusicLibrary : MonoBehaviour {

    public MusicTrack[] tracks; // lista com todas as musicas

    public AudioClip GetClipFromName(string trackName) { // vai digitar no inspector a musica que deseja
        foreach (var track in tracks) { // percorre toda a lista
            if (track.trackName == trackName) { // se achar
                return track.clip; // retorna o clip
            }
        }
        return null;
    }

}