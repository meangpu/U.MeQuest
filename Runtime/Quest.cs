using UnityEngine;
namespace Meangpu.Quest
{
    public class Quest
    {
        public SOQuestInfo Info;
        public QuestState State;
        private int _currentQuestStepIndex;
        private QuestStepState[] _questStepStates;

        public Quest(SOQuestInfo _info)
        {
            Info = _info;
            State = QuestState.REQUIREMENTS_NOT_MET;
            _currentQuestStepIndex = 0;
            _questStepStates = new QuestStepState[Info.QuestStepPrefabs.Length];
            for (var i = 0; i < _questStepStates.Length; i++)
            {
                _questStepStates[i] = new QuestStepState();
            }
        }

        public void MoveToNextStep() => _currentQuestStepIndex++;
        public bool IsCurrentStepExists() => _currentQuestStepIndex < Info.QuestStepPrefabs.Length;

        public void InstantiateCurrentQuestStep(Transform parentTransform)
        {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null)
            {
                QuestStep questStep = Object.Instantiate(questStepPrefab, parentTransform).GetComponent<QuestStep>();
                questStep.InitializeQuestStep(Info.Id, _currentQuestStepIndex);
            }
        }

        private GameObject GetCurrentQuestStepPrefab()
        {
            GameObject questStepPrefab = null;
            if (IsCurrentStepExists()) questStepPrefab = Info.QuestStepPrefabs[_currentQuestStepIndex];
            else Debug.LogWarning($"QuestStepIsOutOfRange:{Info.Id}//{Info.name}");
            return questStepPrefab;
        }

        public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
        {
            if (stepIndex < _questStepStates.Length)
            {
                _questStepStates[stepIndex].State = questStepState.State;
            }
            else
            {
                Debug.LogWarning($"index of quest step is out of range{Info.Id}");
            }
        }

        public QuestData GetQuestData()
        {
            return new QuestData(State, _currentQuestStepIndex, _questStepStates);
        }
    }
}
