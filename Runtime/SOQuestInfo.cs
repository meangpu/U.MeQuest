using UnityEngine;

namespace Meangpu.Quest
{
    [CreateAssetMenu(fileName = "SOQuestInfo", menuName = "MeQuest/SOQuestInfo", order = 1)]
    public class SOQuestInfo : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
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

        private void OnValidate()
        {
#if UNITY_EDITOR
            Id = name;
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}
