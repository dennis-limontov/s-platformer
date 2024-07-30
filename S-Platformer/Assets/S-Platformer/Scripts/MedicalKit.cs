using UnityEngine;

namespace SPlatformer
{
    public class MedicalKit : Pickable
    {
        [field: SerializeField]
        public int Increment { get; private set; }
    }
}