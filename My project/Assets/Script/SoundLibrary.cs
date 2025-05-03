using UnityEngine;

// basicamente vai conter todos os sound effects do jogo

[System.Serializable]
public struct SoundEffect {
    public string groupID; // id do nosso sound effect
    public AudioClip[] clips; // clip
}

public class SoundLibrary : MonoBehaviour {
    
    public SoundEffect[] soundEffects; // array de sound effects visivel la no inspector

    public AudioClip GetClipFromName(string name) { // pega o nome la no inspector
        foreach (var soundEffect in soundEffects) { // percorre toda a lista de sounds
            if (soundEffect.groupID == name) { // caso ache
                return soundEffect.clips[Random.Range(0, soundEffect.clips.Length)]; // retorna o sound effect
            }
        }
        return null;
    }

}