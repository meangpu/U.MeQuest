using UnityEngine;

namespace Meangpu.Quest
{
    public class CurrentQuestDisplay : MonoBehaviour
    {
        [SerializeField] CurrentQuestInfoHolderUI questPrefab;

        void OnEnable()
        {
            QuestEvent.OnQuestStateChange += UpdateQuestUI;
            QuestEvent.OnStartQuest += AddQuestToUI;
        }

        void OnDisable()
        {
            QuestEvent.OnQuestStateChange -= UpdateQuestUI;
            QuestEvent.OnStartQuest -= AddQuestToUI;
        }

        private void AddQuestToUI(SOQuestInfo questInfo)
        {
            CurrentQuestInfoHolderUI nowUI = Instantiate(questPrefab, transform);
            nowUI.SetQuestUIData(questInfo);
        }

        private void UpdateQuestUI(Quest quest)
        {
            // QuestInfoHolderUI nowUI = Instantiate(questPrefab, transform);
            // nowUI.SetQuestUIData(quest.Info);
        }
    }
}
