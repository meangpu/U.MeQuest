using UnityEngine;
using System.Collections.Generic;
using System;

namespace Meangpu.Quest
{
    public class QuestManager : MonoBehaviour
    {
        private int _playerCurrentLevel;
        private Dictionary<string, Quest> _questMap;
        private Dictionary<string, Quest> CreateQuestMap()
        {
            SOQuestInfo[] allQuest = Resources.LoadAll<SOQuestInfo>("Quests");
            Dictionary<string, Quest> idToQuestMap = new();
            foreach (SOQuestInfo questInfo in allQuest)
            {
                if (idToQuestMap.ContainsKey(questInfo.Id)) Debug.Log($"Duplicate quest id found! {questInfo.Id}");
                idToQuestMap.Add(questInfo.Id, new Quest(questInfo));
            }
            return idToQuestMap;
        }

        private void Awake() => _questMap = CreateQuestMap();

        void OnEnable()
        {
            QuestEvent.OnStartQuest += StartQuest;
            QuestEvent.OnAdvanceQuest += AdvanceQuest;
            QuestEvent.OnFinishQuest += FinishQuest;
        }
        void OnDisable()
        {
            QuestEvent.OnStartQuest -= StartQuest;
            QuestEvent.OnAdvanceQuest -= AdvanceQuest;
            QuestEvent.OnFinishQuest -= FinishQuest;
        }

        private void Start()
        {
            foreach (Quest quest in _questMap.Values) QuestEvent.QuestStateChange(quest);
        }

        private void ChangeQuestState(string id, QuestState newState)
        {
            Quest nowQuest = GetQuestByID(id);
            nowQuest.State = newState;
            QuestEvent.QuestStateChange(nowQuest);
        }

        private bool CheckPlayerRequirement(Quest quest)
        {
            bool isMeetRequirement = true;
            if (_playerCurrentLevel < quest.Info.LevelRequirement) isMeetRequirement = false;
            foreach (SOQuestInfo prerequisiteQuestInfo in quest.Info.QuestPrerequisites)
            {
                if (GetQuestByID(prerequisiteQuestInfo.Id).State != QuestState.FINISHED)
                {
                    isMeetRequirement = false;
                }
            }
            return isMeetRequirement;
        }

        private void StartQuest(string id)
        {
            Quest quest = GetQuestByID(id);
            quest.InstantiateCurrentQuestStep(transform);
            ChangeQuestState(quest.Info.Id, QuestState.IN_PROGRESS);
        }

        private void AdvanceQuest(string id)
        {
            Quest quest = GetQuestByID(id);
            quest.MoveToNextStep();
            if (quest.IsCurrentStepExists()) quest.InstantiateCurrentQuestStep(transform);
            else ChangeQuestState(quest.Info.Id, QuestState.CAN_FINISH);
        }

        private void FinishQuest(string id)
        {
            Quest quest = GetQuestByID(id);
            ClaimReward(quest);
            ChangeQuestState(quest.Info.Id, QuestState.FINISHED);
        }

        private void ClaimReward(Quest quest)
        {
            Debug.Log($"get reward from {quest.Info.Id}");
        }

        private Quest GetQuestByID(string id)
        {
            Quest quest = _questMap[id];
            if (quest == null) Debug.LogError($"no quest with {id} found");
            return quest;
        }

        // todo delete 
        private void Update()
        {
            foreach (Quest quest in _questMap.Values)
            {
                if (quest.State == QuestState.REQUIREMENTS_NOT_MET && CheckPlayerRequirement(quest))
                    ChangeQuestState(quest.Info.Id, QuestState.CAN_START);
            }
        }
    }
}