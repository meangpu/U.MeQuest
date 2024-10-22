using UnityEngine;

namespace Meangpu.Quest
{
    public class QuestInfoDisplay : MonoBehaviour
    {
        [SerializeField] QuestInfoHolderUI questPrefab;

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
            QuestInfoHolderUI nowUI = Instantiate(questPrefab, transform);
            nowUI.SetQuestUIData(questInfo);
        }

        private void UpdateQuestUI(Quest quest)
        {
            // QuestInfoHolderUI nowUI = Instantiate(questPrefab, transform);
            // nowUI.SetQuestUIData(quest.Info);
        }
    }
}
