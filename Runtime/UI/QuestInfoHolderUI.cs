using UnityEngine;
using TMPro;

namespace Meangpu.Quest
{
    public class QuestInfoHolderUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _questName;
        [SerializeField] TMP_Text _questDes;

        public void SetQuestUIData(SOQuestInfo questInfo)
        {
            _questName.SetText(questInfo.DisplayName);
            _questDes.SetText(questInfo.Description);
        }
    }
}