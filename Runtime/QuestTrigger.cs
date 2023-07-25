using UnityEngine;

namespace Meangpu.Quest
{
    [RequireComponent(typeof(Collider))]
    public class QuestTrigger : MonoBehaviour
    {
        [SerializeField] QuestInfoHolder _infoHolderScript;

        void OnMouseDown()
        {
            _infoHolderScript.OnGetTrigger();
            Debug.Log(_infoHolderScript.QuestData.Id);
        }
    }
}
