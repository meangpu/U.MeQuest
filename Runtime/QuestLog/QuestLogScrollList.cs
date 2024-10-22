using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VInspector;

namespace Meangpu.Quest
{
    public class QuestLogScrollList : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] Transform _parent;
        [Header("SpawnPref")]
        [SerializeField] QuestLogButton _logPrefab;

        [Header("Rect Trans")]
        [SerializeField] RectTransform _scrollRectTrans;
        [SerializeField] RectTransform _contentRectTrans;

        private Dictionary<SOQuestInfo, QuestLogButton> idToButtonMap = new();

        [Button]
        public void CreateTestQuestData(int count = 20)
        {
            for (int i = 0, length = count; i < length; i++)
            {
                SOQuestInfo soTest = ScriptableObject.CreateInstance<SOQuestInfo>();
                soTest.name = $"wow{i}";
                soTest.DisplayName = $"wow{i}";
                soTest.QuestStepPrefabs = new QuestStep[i];
                Quest quest = new(soTest);
                QuestLogButton log = CreateButtonIfNotExist(quest, () => { Debug.Log($"{soTest.DisplayName}"); });
                if (i == 0)
                {
                    log.Button.Select();
                }
            }
        }

        public QuestLogButton CreateButtonIfNotExist(Quest quest, UnityAction selectAction)
        {
            QuestLogButton currentLog = null;
            if (!idToButtonMap.ContainsKey(quest.Info))
            {
                currentLog = InstantiateQuestLogButton(quest, selectAction);
            }
            else
            {
                currentLog = idToButtonMap[quest.Info];
            }
            return currentLog;
        }

        private QuestLogButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction)
        {
            QuestLogButton current_log = Instantiate(_logPrefab, _parent);
            RectTransform buttonRect = current_log.GetComponent<RectTransform>();

            current_log.Initialize(quest.Info.DisplayName, () =>
            {
                selectAction();
                UpdateScrolling(buttonRect);
            });

            idToButtonMap[quest.Info] = current_log;

            return current_log;
        }

        private void UpdateScrolling(RectTransform btnRectTrans)
        {
            // when using arrow keyboard to select quest
            float btnYMin = Mathf.Abs(btnRectTrans.anchoredPosition.y);
            float btnYMax = btnYMin + btnRectTrans.rect.height;

            float contentYMin = _contentRectTrans.anchoredPosition.y;
            float contentYMax = contentYMin + _scrollRectTrans.rect.height;

            if (btnYMax > contentYMax)
            {
                _contentRectTrans.anchoredPosition = new(_contentRectTrans.anchoredPosition.x, btnYMax - _scrollRectTrans.rect.height);
            }
            else if (btnYMin < contentYMin)
            {
                _contentRectTrans.anchoredPosition = new(_contentRectTrans.anchoredPosition.x, btnYMin);
            }

        }
    }
}
