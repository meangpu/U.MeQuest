using UnityEngine;

namespace Meangpu.Quest
{
    [RequireComponent(typeof(Collider))]
    public class QuestTriggerCollider : MonoBehaviour
    {
        [SerializeField] QuestInfoHolder _infoHolderScript;

        void OnMouseDown()
        {
            _infoHolderScript.OnGetTrigger();
        }
    }
}
