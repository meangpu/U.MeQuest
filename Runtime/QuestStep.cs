using UnityEngine;

namespace Meangpu.Quest
{
    public abstract class QuestStep : MonoBehaviour
    {
        bool _isFinish;
        string _questId;

        public void InitializeQuestStep(string questId)
        {
            _questId = questId;
        }

        protected void FinishQuestStep()
        {
            if (!_isFinish)
            {
                _isFinish = true;
                QuestEvent.AdvanceQuest(_questId);
                Destroy(gameObject);
            }
        }
    }
}
