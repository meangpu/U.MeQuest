using UnityEngine;
namespace Meangpu.Quest
{
    public class Quest
    {
        public SOQuestInfo Info;
        public QuestState State;
        private int _currentQuestStepIndex;

        public Quest(SOQuestInfo _info)
        {
            Info = _info;
            State = QuestState.REQUIREMENTS_NOT_MET;
            _currentQuestStepIndex = 0;
        }

        public void MoveToNextStep() => _currentQuestStepIndex++;
        public bool IsCurrentStepExists() => _currentQuestStepIndex < Info.QuestStepPrefabs.Length;

        public void InstantiateCurrentQuestStep(Transform parentTransform)
        {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null) Object.Instantiate(questStepPrefab, parentTransform);
        }

        private GameObject GetCurrentQuestStepPrefab()
        {
            GameObject questStepPrefab = null;
            if (IsCurrentStepExists()) questStepPrefab = Info.QuestStepPrefabs[_currentQuestStepIndex];
            else Debug.LogWarning($"QuestStepIsOutOfRange:{Info.Id}//{Info.name}");
            return questStepPrefab;
        }
    }
}
