using UnityEngine;
using System.Collections.Generic;

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

        private void Awake()
        {
            _questMap = CreateQuestMap();
            Quest nowQuest = GetQuestByID("CollectCoin");
            Debug.Log(nowQuest.Info.DisplayName);
            Debug.Log(nowQuest.Info.ExpReward);
        }

        private Quest GetQuestByID(string id)
        {
            Quest quest = _questMap[id];
            if (quest == null) Debug.LogError($"no quest with {id} found");
            return quest;
        }
    }
}