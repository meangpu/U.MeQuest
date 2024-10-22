using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Meangpu.Quest
{
    public class QuestLogScrollList : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] Transform _parent;
        [Header("SpawnPref")]
        [SerializeField] QuestLogButton _logPrefab;

        private Dictionary<SOQuestInfo, QuestLogButton> idToButtonMap = new();

        private void Start()
        {
            for (int i = 0, length = 3; i < length; i++)
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
            current_log.Initialize(quest.Info.DisplayName, selectAction);
            idToButtonMap[quest.Info] = current_log;
            return current_log;
        }


    }
}
