using UnityEngine;

namespace Meangpu.Quest
{
    [CreateAssetMenu(fileName = "SOQuestInfo", menuName = "MeQuest/SOQuestInfo", order = 1)]
    public class SOQuestInfo : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [Header("General")]
        public string DisplayName;

        [Header("Requirement")]
        public int LevelRequirement;
        public SOQuestInfo QuestPrerequisites;

        [Header("Steps")]
        public GameObject[] QuestStepPrefabs;

        [Header("Rewards")]
        public GameObject[] RewardPrefab; // make class with interface IGetReward
        public int GoldReward;
        public int ExpReward;

        private void OnValidate()
        {
#if UNITY_EDITOR
            Id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}
