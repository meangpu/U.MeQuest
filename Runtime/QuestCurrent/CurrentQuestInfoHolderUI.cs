using UnityEngine;
using TMPro;

namespace Meangpu.Quest
{
    public class CurrentQuestInfoHolderUI : MonoBehaviour
    {
        public SOQuestInfo Info { get; private set; }

        [SerializeField] TMP_Text _questName;
        [SerializeField] TMP_Text _questDes;

        void OnEnable()
        {
            QuestEvent.OnFinishQuest += RemoveQuest;
        }

        void OnDisable()
        {
            QuestEvent.OnFinishQuest -= RemoveQuest;
        }

        private void RemoveQuest(SOQuestInfo info)
        {
            if (info.Equals(Info)) Destroy(gameObject);
        }

        public void SetQuestUIData(SOQuestInfo questInfo)
        {
            Info = questInfo;
            _questName.SetText(questInfo.DisplayName);
            _questDes.SetText(questInfo.Description);
        }
    }
}