using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VInspector;

namespace Meangpu.Quest
{
    public class QuestLogInfoUI : MonoBehaviour
    {
        [SerializeField] GameObject _parentQuestInfo;

        [SerializeField] QuestLogScrollList _scrollList;
        [Header("Text")]
        [SerializeField] TMP_Text _questName;
        [SerializeField] TMP_Text _questStatus;

        [SerializeField] TMP_Text _rewardGold;
        [SerializeField] TMP_Text _rewardExp;
        [SerializeField] TMP_Text _rewardQuest;

        [SerializeField] TMP_Text _requirementLevel;
        [SerializeField] TMP_Text _requirementQuest;

        Quest _lastQuest;
        Button _lastSelectedButton;

        void OnEnable()
        {
            QuestEvent.OnQuestStateChange += QuestStateChange;
            QuestEvent.OnQuestStateChange += UpdateDescription;
        }
        void OnDisable()
        {
            QuestEvent.OnQuestStateChange -= QuestStateChange;
            QuestEvent.OnQuestStateChange -= UpdateDescription;
        }


        [Button]
        public void ToggleQuestLog()
        {
            if (_parentQuestInfo.activeSelf) HideUI();
            else ShowUI();
        }

        private void HideUI()
        {
            _parentQuestInfo.SetActive(false);
            EventSystem.current.currentSelectedGameObject.TryGetComponent(out _lastSelectedButton);
            EventSystem.current.SetSelectedGameObject(null);
        }

        private void ShowUI()
        {
            _parentQuestInfo.SetActive(true);

            if (_lastSelectedButton != null)
            {
                _lastSelectedButton.Select();
            }
        }

        void QuestStateChange(Quest quest)
        {
            QuestLogButton logEntry = _scrollList.CreateButtonIfNotExist(quest, () => { SetQuestInfo(quest); });
            if (_lastSelectedButton == null)
            {
                _lastSelectedButton = logEntry.Button;
            }
            logEntry.SetTextColorByState(quest.State);
        }

        void UpdateDescription(Quest quest)
        {
            if (_lastQuest == null || !_lastQuest.Equals(quest)) return;
            _questStatus.SetText(quest.GetFullStatusText());
        }

        void SetQuestInfo(Quest quest)
        {
            _questName.SetText(quest.Info.DisplayName);
            _questStatus.SetText(quest.GetFullStatusText());
            _requirementLevel.SetText(quest.Info.LevelRequirement.ToString());
            _requirementQuest.SetText("");
            foreach (SOQuestInfo requirement in quest.Info.QuestPrerequisites)
            {
                _requirementQuest.text += $"{requirement.DisplayName}\n";
            }
            _lastQuest = quest;
        }

    }
}
