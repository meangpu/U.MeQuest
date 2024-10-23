using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Meangpu.Quest
{
    public class QuestLogButton : MonoBehaviour, ISelectHandler
    {
        public Button Button { get; private set; }
        TMP_Text _text;
        UnityAction onSelectAction;
        public Quest QuestData { get; private set; }

        [SerializeField] Color _colReqNotMet;
        [SerializeField] Color _colCanStart;
        [SerializeField] Color _colInProgress;
        [SerializeField] Color _colCanFinish;
        [SerializeField] Color _colFInished;

        public void Initialize(Quest quest, string displayName, UnityAction selectAction)
        {
            QuestData = quest;
            Button = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();
            _text.SetText(displayName);
            onSelectAction = selectAction;
        }

        public void OnSelect(BaseEventData eventData)
        {
            onSelectAction();
        }

        public void SetTextColorByState(QuestState state)
        {
            switch (state)
            {
                case QuestState.REQUIREMENTS_NOT_MET:
                    _text.color = _colReqNotMet;
                    break;
                case QuestState.CAN_START:
                    _text.color = _colCanStart;
                    break;
                case QuestState.IN_PROGRESS:
                    _text.color = _colInProgress;
                    break;
                case QuestState.CAN_FINISH:
                    _text.color = _colCanFinish;
                    break;
                case QuestState.FINISHED:
                    _text.color = _colFInished;
                    break;
                default:
                    Debug.LogWarning($"Quest state error No {state}");
                    break;
            }

        }
    }
}
