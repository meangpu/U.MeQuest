namespace Meangpu.Quest
{
    [System.Serializable]
    public class QuestStepState
    {
        public string State;
        public string Status;

        public QuestStepState(string state, string status)
        {
            State = state;
            Status = status;
        }

        public QuestStepState()
        {
            State = "";
            Status = "";
        }
    }
}