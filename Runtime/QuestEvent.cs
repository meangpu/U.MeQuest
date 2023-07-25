using System;

namespace Meangpu.Quest
{
    public static class QuestEvent
    {
        public static event Action<string> OnStartQuest;
        public static void StartQuest(string id) => OnStartQuest?.Invoke(id);

        public static event Action<string> OnAdvanceQuest;
        public static void AdvanceQuest(string id) => OnAdvanceQuest?.Invoke(id);

        public static event Action<string> OnFinishQuest;
        public static void FinishQuest(string id) => OnFinishQuest?.Invoke(id);

        public static event Action<Quest> OnQuestStateChange;
        public static void QuestStateChange(Quest quest) => OnQuestStateChange?.Invoke(quest);

        public static event Action<string, int, QuestStepState> OnQuestStepStateChange;
        public static void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
        {
            OnQuestStepStateChange?.Invoke(id, stepIndex, questStepState);
        }
    }
}