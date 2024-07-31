using System;
using UnityEngine;

namespace SPlatformer
{
    public static class EventHub
    {
        public static Action<Transform> OnCheckPointCrossed;
        public static Action<int> OnFallingHappened;
        public static Action OnFinishReached;
        public static Action OnGameRestarted;
        public static Action<int, int> OnHealthChanged;
        public static Action<int, int> OnKeyChanged;
        public static Action OnKeyCollected;
    }
}