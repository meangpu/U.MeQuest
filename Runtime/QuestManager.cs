using UnityEngine;
using System.Collections.Generic;
using System;

namespace Meangpu.Quest
{
    public class QuestManager : MonoBehaviour
    {
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

        private void StartQuest(string obj)
        {
            throw new NotImplementedException();
        }

        private void AdvanceQuest(string obj)
        {
            throw new NotImplementedException();
        }

        private void FinishQuest(string obj)
        {
            throw new NotImplementedException();
        }

        private Quest GetQuestByID(string id)
        {
            Quest quest = _questMap[id];
            if (quest == null) Debug.LogError($"no quest with {id} found");
            return quest;
        }
    }
}