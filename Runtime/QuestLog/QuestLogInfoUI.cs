using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.Quest
{
    public class QuestLogInfoUI : MonoBehaviour
    {
        [SerializeField] Transform _parent;
        [SerializeField] QuestLogScrollList _scrollList;
        [SerializeField] TMP_Text _questName;
        [SerializeField] TMP_Text _questStatus;

        [SerializeField] TMP_Text _rewardGold;
        [SerializeField] TMP_Text _rewardExp;
        [SerializeField] TMP_Text _rewardQuest;

        [SerializeField] TMP_Text _requirementLevel;
        [SerializeField] TMP_Text _requirementQuest;

        Button _firstSelectedButton;

        void OnEnable()
        {
            QuestEvent.OnQuestStateChange += QuestStateChange;
        }
        void OnDisable()
        {
            QuestEvent.OnQuestStateChange -= QuestStateChange;
        }

        void QuestStateChange(Quest quest)
        {
            QuestLogButton log = _scrollList.CreateButtonIfNotExist(quest, () => { SetQuestInfo(quest); });
            if (_firstSelectedButton == null)
            {
                _firstSelectedButton = log.Button;
                _firstSelectedButton.Select();
            }
        }

        void SetQuestInfo(Quest quest)
        {
            _questName.SetText(quest.Info.DisplayName);

            _questName.SetText(quest.Info.DisplayName);


            _requirementLevel.SetText(quest.Info.LevelRequirement.ToString());
            _requirementQuest.SetText("");
            foreach (SOQuestInfo requirement in quest.Info.QuestPrerequisites)
            {
                _requirementQuest.text += requirement.DisplayName + "\n";
            }
        }

    }
}
