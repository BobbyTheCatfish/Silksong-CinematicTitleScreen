using UnityEngine;

namespace CinematicTitleScreen
{
    internal static class Common
    {
        public const string MenuSceneName = "Menu_Title";
        public const float DefaultDelay = 25;
    }

    public struct SceneInfo
    {
        public string Name = "";
        public Animation[] Animations = [];
        public float Delay = 25;
        public float Scale = 1;
        public string[]? EnablePaths = [];
        public string[]? DisablePaths = [];
        public string[]? PlayerFlags = [];
        public PersistentItemData<bool>[]? PersistantBools = [];
        public bool AddHero = false;
        public bool RandomOrder = true;

        public SceneInfo()
        {
        }
    }

    public struct Animation
    {
        public Vector3[] Positions;
        public float StartDelay;
        public float EndDelay;
        public float Speed;
        public bool AllowReverse;
        public Color AmbientColor;

        public Animation()
        {
            Positions = [];
            StartDelay = 25;
            EndDelay = 25;
            Speed = 1;
            AllowReverse = false;
            AmbientColor = new Color(0, 0, 0, 0);
        }

        public Animation(Vector3[] positions, float delay = 25, float endDelay = -1, float speed = 1, bool allowReverse = false)
        {
            Positions = positions;
            StartDelay = delay;

            if (endDelay < 0) EndDelay = delay;
            else EndDelay = endDelay;
            
            Speed = speed;
            AllowReverse = allowReverse;
        }

        public static Animation SingleFrame(Vector2 position, float delay = Common.DefaultDelay, Color? ambientColor = null)
        {
            return new Animation
            {
                Positions = [position],
                StartDelay = delay,
                EndDelay = 0,
                Speed = 1,
                AmbientColor = ambientColor ?? new Color(0, 0, 0, 0)
            };
        }
    }
}
