using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Meangpu.Quest
{
    public class QuestLogButton : MonoBehaviour, ISelectHandler
    {
        public Button Button { get; private set; }
        TMP_Text _text;
        UnityAction onSelectAction;

        public void Initialize(string displayName, UnityAction selectAction)
        {
            Button = GetComponent<Button>();
            _text = GetComponentInChildren<TMP_Text>();
            _text.SetText(displayName);
            onSelectAction = selectAction;
        }

        public void OnSelect(BaseEventData eventData)
        {
            onSelectAction();
        }
    }
}
