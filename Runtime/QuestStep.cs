using UnityEngine;

namespace Meangpu.Quest
{
    public abstract class QuestStep : MonoBehaviour
    {
        bool _isFinish = false;
        protected void FinishQuestStep()
        {
            if (!_isFinish)
            {
                _isFinish = true;
                // TODO advance quest
                Destroy(gameObject);
            }
        }
    }
}
