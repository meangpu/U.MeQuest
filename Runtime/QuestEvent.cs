using System;

namespace Meangpu.Quest
{
    public static class QuestEvent
    {
        public static event Action<int> OnSetPlayerLevel;
        public static void SetPlayerLevel(int playerLevel) => OnSetPlayerLevel?.Invoke(playerLevel);

        public static event Action<SOQuestInfo> OnStartQuest;
        public static void StartQuest(SOQuestInfo id) => OnStartQuest?.Invoke(id);

        public static event Action<SOQuestInfo> OnAdvanceQuest;
        public static void AdvanceQuest(SOQuestInfo id) => OnAdvanceQuest?.Invoke(id);

        public static event Action<SOQuestInfo> OnFinishQuest;
        public static void FinishQuest(SOQuestInfo id) => OnFinishQuest?.Invoke(id);

        public static event Action<Quest> OnQuestStateChange;
        public static void QuestStateChange(Quest quest) => OnQuestStateChange?.Invoke(quest);

        public static event Action<SOQuestInfo, int, QuestStepState> OnQuestStepStateChange;

        public static void QuestStepStateChange(SOQuestInfo id, int stepIndex, QuestStepState questStepState)
        {
            OnQuestStepStateChange?.Invoke(id, stepIndex, questStepState);
        }
    }
}