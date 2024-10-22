using UnityEngine;

namespace Meangpu.Quest
{
    public class QuestInfoHolder : MonoBehaviour
    {
        [Header("Quest")]
        [SerializeField] SOQuestInfo _questData;
        public SOQuestInfo QuestData => _questData;

        private QuestState _currentQuestState;
        private QuestIcon _questIconScript;

        [Header("Config")]
        [SerializeField] bool _isStartPoint;
        [SerializeField] bool _isFinishPoint;

        private void Awake()
        {
            _questIconScript = GetComponentInChildren<QuestIcon>();
        }

        void OnEnable()
        {
            QuestEvent.OnQuestStateChange += OnQuestStateChange;
        }
        void OnDisable()
        {
            QuestEvent.OnQuestStateChange -= OnQuestStateChange;
        }

        private void OnQuestStateChange(Quest quest)
        {
            if (quest.Info.Equals(_questData))
            {
                _currentQuestState = quest.State;
                _questIconScript.SetState(_currentQuestState, _isStartPoint, _isFinishPoint);
            }
        }

        public void OnGetTrigger()
        {
            if (_currentQuestState.Equals(QuestState.CAN_START) && _isStartPoint) QuestEvent.StartQuest(_questData);
            else if (_currentQuestState.Equals(QuestState.CAN_FINISH) && _isFinishPoint) QuestEvent.FinishQuest(_questData);
        }
    }
}