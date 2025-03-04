using UnityEngine;

namespace World
{
    public class CutsceneManager
    {
        public enum Cutscenes
        {
            StartGame,
            ReadyGame,
            Caught,
            Win
        }
        public static bool IsInCutscene { get; private set; } = false;
        public delegate void PlayCutsceneDel(Cutscenes cutscene, Transform location);
        public delegate void EndCutsceneDel(Cutscenes cutscene);

        public static PlayCutsceneDel OnPlayCutscene;
        public static EndCutsceneDel OnEndCutscene;

        public static void StartCutscene(Cutscenes cutscene, Transform location)
        {
            // Start cutscene

            IsInCutscene = true;
            OnPlayCutscene?.Invoke(cutscene, location);
        }
        
        public static void EndCutscene(Cutscenes cutscene)
        {
            // End cutscene
    
            IsInCutscene = false;
            OnEndCutscene?.Invoke(cutscene);
        }
    }
}