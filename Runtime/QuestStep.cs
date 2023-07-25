using UnityEngine;

namespace Meangpu.Quest
{
    public abstract class QuestStep : MonoBehaviour
    {
        bool _isFinish;
        string _questId;
        int _stepIndex;

        public void InitializeQuestStep(string questId, int stepIndex)
        {
            _questId = questId;
            _stepIndex = stepIndex;
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
        protected void ChangeState(string newState)
        {
            QuestEvent.QuestStepStateChange(_questId, _stepIndex, new QuestStepState(newState));
        }
    }
}
