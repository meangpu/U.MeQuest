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

        public Quest(SOQuestInfo questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates)
        {
            Info = questInfo;
            State = questState;
            _currentQuestStepIndex = currentQuestStepIndex;
            _questStepStates = questStepStates;
            if (questStepStates.Length != Info.QuestStepPrefabs.Length) Debug.LogWarning("Data is out of sync");
        }

        public void MoveToNextStep() => _currentQuestStepIndex++;
        public bool IsCurrentStepExists() => _currentQuestStepIndex < Info.QuestStepPrefabs.Length;

        public void InstantiateCurrentQuestStep(Transform parentTransform)
        {
            QuestStep questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null)
            {
                QuestStep questStep = Object.Instantiate(questStepPrefab, parentTransform);
                questStep.InitializeQuestStep(Info, _currentQuestStepIndex, _questStepStates[_currentQuestStepIndex].State);
            }
        }

        private QuestStep GetCurrentQuestStepPrefab()
        {
            QuestStep questStepPrefab = null;
            if (IsCurrentStepExists()) questStepPrefab = Info.QuestStepPrefabs[_currentQuestStepIndex];
            else Debug.LogWarning($"QuestStepIsOutOfRange:{Info.Id}//{Info.name}");
            return questStepPrefab;
        }

        public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
        {
            if (stepIndex < _questStepStates.Length)
            {
                _questStepStates[stepIndex].State = questStepState.State;
                _questStepStates[stepIndex].Status = questStepState.Status;
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

        public string GetFullStatusText()
        {
            string fullStatus = "";
            if (State == QuestState.REQUIREMENTS_NOT_MET)
            {
                fullStatus = "Requirements not met";
            }
            else if (State == QuestState.CAN_START)
            {
                fullStatus = "Can start";
            }
            else
            {
                for (int i = 0; i < _currentQuestStepIndex; i++)
                {
                    fullStatus += $"<s>{_questStepStates[i].Status}</s>\n";
                }
                if (IsCurrentStepExists())
                {
                    fullStatus += $"{_questStepStates[_currentQuestStepIndex].Status}\n";
                }
                if (State == QuestState.CAN_FINISH)
                {
                    fullStatus += $"Quest is ready to be turned in\n";
                }
                else if (State == QuestState.FINISHED)
                {
                    fullStatus += $"Quest completed!\n";
                }

            }

            return fullStatus;
        }
    }
}
