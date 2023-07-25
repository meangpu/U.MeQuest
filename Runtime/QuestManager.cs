using UnityEngine;
using System.Collections.Generic;
using System;

namespace Meangpu.Quest
{
    public class QuestManager : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] bool _doLoadDataFromSave;
        private int _playerCurrentLevel;
        private Dictionary<string, Quest> _questMap;
        private Dictionary<string, Quest> CreateQuestMap()
        {
            SOQuestInfo[] allQuest = Resources.LoadAll<SOQuestInfo>("Quests");
            Dictionary<string, Quest> idToQuestMap = new();
            foreach (SOQuestInfo questInfo in allQuest)
            {
                if (idToQuestMap.ContainsKey(questInfo.Id)) Debug.Log($"Duplicate quest id found! {questInfo.Id}");

                idToQuestMap.Add(questInfo.Id, LoadQuest(questInfo));
            }
            return idToQuestMap;
        }

        private void Awake()
        {
            _questMap = CreateQuestMap();
            UpdateQuestThatPlayerCanStart();
        }

        void OnEnable()
        {
            QuestEvent.OnSetPlayerLevel += SetPlayerLevel;

            QuestEvent.OnStartQuest += StartQuest;
            QuestEvent.OnAdvanceQuest += AdvanceQuest;
            QuestEvent.OnFinishQuest += FinishQuest;

            QuestEvent.OnQuestStepStateChange += QuestStepStateChange;
        }
        void OnDisable()
        {
            QuestEvent.OnSetPlayerLevel -= SetPlayerLevel;

            QuestEvent.OnStartQuest -= StartQuest;
            QuestEvent.OnAdvanceQuest -= AdvanceQuest;
            QuestEvent.OnFinishQuest -= FinishQuest;

            QuestEvent.OnQuestStepStateChange -= QuestStepStateChange;
        }

        private void SetPlayerLevel(int newLevel)
        {
            _playerCurrentLevel = newLevel;
            UpdateQuestThatPlayerCanStart();
        }

        private void Start()
        {
            foreach (Quest quest in _questMap.Values)
            {
                if (quest.State == QuestState.IN_PROGRESS)
                {
                    quest.InstantiateCurrentQuestStep(transform);
                }
                QuestEvent.QuestStateChange(quest);
            }
        }

        private void ChangeQuestState(string id, QuestState newState)
        {
            Quest nowQuest = GetQuestByID(id);
            nowQuest.State = newState;
            QuestEvent.QuestStateChange(nowQuest);
        }

        private bool CheckPlayerRequirement(Quest quest)
        {
            bool isMeetRequirement = true;
            if (_playerCurrentLevel < quest.Info.LevelRequirement) isMeetRequirement = false;
            foreach (SOQuestInfo prerequisiteQuestInfo in quest.Info.QuestPrerequisites)
            {
                if (GetQuestByID(prerequisiteQuestInfo.Id).State != QuestState.FINISHED)
                {
                    isMeetRequirement = false;
                }
            }
            return isMeetRequirement;
        }

        private void StartQuest(string id)
        {
            Quest quest = GetQuestByID(id);
            quest.InstantiateCurrentQuestStep(transform);
            ChangeQuestState(quest.Info.Id, QuestState.IN_PROGRESS);
        }

        private void AdvanceQuest(string id)
        {
            Quest quest = GetQuestByID(id);
            quest.MoveToNextStep();
            if (quest.IsCurrentStepExists()) quest.InstantiateCurrentQuestStep(transform);
            else ChangeQuestState(quest.Info.Id, QuestState.CAN_FINISH);
        }

        private void FinishQuest(string id)
        {
            Quest quest = GetQuestByID(id);
            ClaimReward(quest);
            ChangeQuestState(quest.Info.Id, QuestState.FINISHED);
            UpdateQuestThatPlayerCanStart();
        }

        private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
        {
            Quest quest = GetQuestByID(id);
            quest.StoreQuestStepState(questStepState, stepIndex);
            ChangeQuestState(id, quest.State);
        }

        private void ClaimReward(Quest quest)
        {
            if (quest.Info.RewardPrefab.Length > 0)
            {
                foreach (QuestReward reward in quest.Info.RewardPrefab) reward.GetReward();
            }
            Debug.Log($"get reward from {quest.Info.Id}");
        }

        private Quest GetQuestByID(string id)
        {
            Quest quest = _questMap[id];
            if (quest == null) Debug.LogError($"no quest with {id} found");
            return quest;
        }

        void UpdateQuestThatPlayerCanStart()
        {
            foreach (Quest quest in _questMap.Values)
            {
                if (quest.State == QuestState.REQUIREMENTS_NOT_MET && CheckPlayerRequirement(quest))
                    ChangeQuestState(quest.Info.Id, QuestState.CAN_START);
            }
        }

        private void OnApplicationQuit()
        {
            foreach (Quest quest in _questMap.Values) SaveQuest(quest);
        }

        void SaveQuest(Quest quest)
        {
            try
            {
                QuestData questData = quest.GetQuestData();
                string serializedData = JsonUtility.ToJson(questData);
                PlayerPrefs.SetString(quest.Info.Id, serializedData);
                // Debug.Log($"{serializedData}");
            }
            catch (Exception e)
            {
                Debug.LogError("fail to save quest with id" + quest.Info.Id + ":" + e);
            }
        }

        private Quest LoadQuest(SOQuestInfo questInfo)
        {
            Quest quest = null;

            try
            {
                if (PlayerPrefs.HasKey(questInfo.Id) && _doLoadDataFromSave)
                {
                    string serializedData = PlayerPrefs.GetString(questInfo.Id);
                    QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                    quest = new Quest(questInfo, questData.State, questData.QuestStepIndex, questData.QuestStepStates);
                }
                else
                {
                    quest = new Quest(questInfo);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("fail to save quest with id" + quest.Info.Id + ":" + e);
            }
            return quest;
        }
    }
}