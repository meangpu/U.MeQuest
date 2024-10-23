using UnityEngine;

namespace Meangpu.Quest
{
    [CreateAssetMenu(fileName = "SOQuestInfo", menuName = "MeQuest/SOQuestInfo", order = 1)]
    public class SOQuestInfo : ScriptableObject
    {
        [Header("General")]
        public string DisplayName;
        [TextArea]
        public string Description;

        [Header("Requirement")]
        public int LevelRequirement;

        public SOQuestInfo[] QuestPrerequisites;

        [Header("Steps")]
        public QuestStep[] QuestStepPrefabs;

        [Header("Rewards")]
        public QuestReward[] RewardPrefab;

    }
}
