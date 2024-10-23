using System.Collections.Generic;
using UnityEngine;

namespace Meangpu.Quest
{
    public class CurrentQuestDisplay : MonoBehaviour
    {
        [SerializeField] CurrentQuestInfoHolderUI questPrefab;
        List<CurrentQuestInfoHolderUI> _questList = new();


        void OnEnable()
        {
            QuestEvent.OnQuestStateChange += UpdateQuestUI;
            QuestEvent.OnStartQuest += AddQuestToUI;
            QuestEvent.OnFinishQuest += RemoveQuest;
        }

        void OnDisable()
        {
            QuestEvent.OnQuestStateChange -= UpdateQuestUI;
            QuestEvent.OnStartQuest -= AddQuestToUI;
            QuestEvent.OnFinishQuest -= RemoveQuest;
        }

        private void RemoveQuest(SOQuestInfo info)
        {
            if (_questList.Count == 0) return;
            foreach (CurrentQuestInfoHolderUI currentQuest in _questList)
            {
                if (!currentQuest.Info.Equals(info)) return;
                _questList.Remove(currentQuest);
                Destroy(currentQuest.gameObject);
                return;
            }
        }

        private void AddQuestToUI(SOQuestInfo questInfo)
        {
            CurrentQuestInfoHolderUI nowUI = Instantiate(questPrefab, transform);
            nowUI.SetQuestUIData(questInfo);
            _questList.Add(nowUI);

        }
        private void UpdateQuestUI(Quest quest)
        {
            foreach (CurrentQuestInfoHolderUI currentQuest in _questList)
            {
                if (!currentQuest.Info.Equals(quest.Info)) return;
                currentQuest.UpdateQuestDes(quest);
            }
        }
    }
}
