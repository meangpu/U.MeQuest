using UnityEngine;
using TMPro;

namespace Meangpu.Quest
{
    public class CurrentQuestInfoHolderUI : MonoBehaviour
    {
        public SOQuestInfo Info { get; private set; }

        [SerializeField] TMP_Text _questName;
        [SerializeField] TMP_Text _questDes;

        public void SetQuestUIData(SOQuestInfo questInfo)
        {
            Info = questInfo;
            _questName.SetText(questInfo.DisplayName);
            _questDes.SetText(questInfo.Description);
        }

        public void UpdateQuestDes(Quest quest)
        {
            if (quest.Info != Info) return;
            _questDes.SetText(quest.GetFullStatusText());
        }
    }
}