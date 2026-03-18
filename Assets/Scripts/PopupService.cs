using System.Collections.Generic;

using UnityEngine;

namespace TestTask_OctoGames
{
    public class PopupService : MonoBehaviour
    {
        [SerializeField] private PopupView _popupView;

        private void Awake()
        {
            PopupButtonData popudData = new PopupButtonData("ButtonName", () => { Debug.Log("PopupButtonClickAction"); });
            _popupView.Show("PopapHeader", "PopupBodyText", new List<PopupButtonData>() { popudData });
        }
    }
}