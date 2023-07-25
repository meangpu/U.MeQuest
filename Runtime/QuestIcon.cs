using UnityEngine;

namespace Meangpu.Quest
{
    public class QuestIcon : MonoBehaviour
    {
        [Header("icon")]
        [SerializeField] GameObject _requirementNotMetIcon;
        [SerializeField] GameObject _canStartIcon;
        [SerializeField] GameObject _requirementNotFinishIcon;
        [SerializeField] GameObject _canFinishIcon;
        public void SetState(QuestState newState, bool isStartPoint, bool isFinishPoint)
        {
            _requirementNotMetIcon.SetActive(false);
            _canStartIcon.SetActive(false);
            _requirementNotFinishIcon.SetActive(false);
            _canFinishIcon.SetActive(false);

            switch (newState)
            {
                case QuestState.REQUIREMENTS_NOT_MET:
                    if (isStartPoint) _requirementNotMetIcon.SetActive(true);
                    break;
                case QuestState.CAN_START:
                    if (isStartPoint) _canStartIcon.SetActive(true);
                    break;
                case QuestState.IN_PROGRESS:
                    if (isFinishPoint) _requirementNotFinishIcon.SetActive(true);
                    break;
                case QuestState.CAN_FINISH:
                    if (isFinishPoint) _canFinishIcon.SetActive(true);
                    break;
                case QuestState.FINISHED:
                    break;
                default:
                    break;
            }
        }
    }
}