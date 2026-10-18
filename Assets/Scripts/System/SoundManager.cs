using UnityEngine;

namespace System
{
    public class SoundManager : MonoBehaviour
    {
        [Serializable]
        public class SoundLibrary
        {
            public AudioClip[] audioClips;
            public float volume;
        }
        
        public enum Sound
        {
            footstep,
            speech,
            correct,
            incorrect
        }
        
        public static SoundManager instance;
        
        [SerializeField] private SoundLibrary[] audioLibraries;
        [SerializeField] private AudioSource sourcePrefab;
        
        
        private void Awake()
        {
            instance = this;
        }

        public void PlaySound(Sound sound, Vector3 position)
        {
            SoundLibrary soundLibrary = audioLibraries[(int)sound];
            AudioClip clip = soundLibrary.audioClips[UnityEngine.Random.Range(0, soundLibrary.audioClips.Length)];
            
            AudioSource source = Instantiate(sourcePrefab, position, Quaternion.identity);
            source.clip = clip;
            source.volume = soundLibrary.volume;
            source.Play();
            
            Destroy(source.gameObject, clip.length);
        }
    }
}
