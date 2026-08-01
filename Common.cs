using UnityEngine;

namespace CinematicTitleScreen
{
    internal static class Common
    {
        public const string MenuSceneName = "Menu_Title";
        public const float DefaultDelay = 5;
    }

    public struct SceneInfo
    {
        public string Name = "";
        public Animation[] Animations = [];
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
        public Vector3[] Positions = [];
        public float StartDelay = Common.DefaultDelay;
        public float EndDelay = Common.DefaultDelay;
        public float Speed = 1;
        public bool AllowReverse = false;
        public bool UseLight = true;

        public Animation()
        {
        }

        public Animation(Vector3[] positions, float delay = Common.DefaultDelay, float endDelay = -1, float speed = 1, bool allowReverse = false, bool useLight = true)
        {
            Positions = positions;
            StartDelay = delay;

            if (endDelay < 0) EndDelay = delay;
            else EndDelay = endDelay;
            
            Speed = speed;
            AllowReverse = allowReverse;
            UseLight = useLight;
        }

        public static Animation SingleFrame(Vector2 position, float delay = Common.DefaultDelay, bool useLight = true)
        {
            return new Animation
            {
                Positions = [position],
                StartDelay = delay,
                EndDelay = 0,
                Speed = 1,
                UseLight = useLight
            };
        }
    }
}
