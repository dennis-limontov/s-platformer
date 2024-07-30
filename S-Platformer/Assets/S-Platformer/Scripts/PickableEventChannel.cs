using System;
using UnityEngine;

namespace SPlatformer
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PickableEventChannel")]
    public class PickableEventChannel : ScriptableObject
    {
        public Action<Pickable> OnPick;
    }
}