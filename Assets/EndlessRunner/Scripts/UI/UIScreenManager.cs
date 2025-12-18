using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EndlessRunner
{
    public class UIScreenManager : MonoBehaviour
    {
        internal List<UIScreensInstanceModel> _uiScreenInstanceModels = new List<UIScreensInstanceModel>();
        private UiScreen _currentScreen;
        public List<UiScreens> _uiScreens;
        public static UIScreenManager Instance;

        private void Awake()
        {
            Instance = this;
            Time.timeScale = 1.0f;
        }

        private void Start()
        {
            _uiScreenInstanceModels = _uiScreens.Select(uiScreen => new UIScreensInstanceModel
            {
                _screenId = uiScreen._myScreenName,
                _screenInstance = uiScreen
            }).ToList();

            ShowScreen(UiScreen.Loading, UiScreenShowBehaviour.HidePrevious);
        }

        public void ShowScreen(UiScreen screenId, UiScreenShowBehaviour behaviour = UiScreenShowBehaviour.HidePrevious)
        {
            GameObject uiScreenInstance = GetUiScreenFromList(screenId);

            if (uiScreenInstance != null)
            {
                uiScreenInstance.SetActive(true);
                _currentScreen = screenId;

                if (behaviour == UiScreenShowBehaviour.HidePrevious && GetTotalActiveUIScreens() > 1)
                {
                    List<UIScreensInstanceModel> activeScreens = GetAllActiveUiScreenReferenceExculdingItself(screenId);
                    activeScreens.ForEach(screen => screen._screenInstance.gameObject.SetActive(false));
                }
            }
            else
            {
                Debug.LogWarning($"Trying to use panelId = {screenId}, but this is not found in the ObjectPool");
            }
        }

        public void HideAllUIScreens()
        {
            if (AnyScreenShowing())
            {
                List<UIScreensInstanceModel> activeScreens = GetAllActiveUiScreenReferenceExculdingItself();
                activeScreens.ForEach(screen => screen._screenInstance.gameObject.SetActive(false));
            }
        }

        public void HideUIScreens(UiScreen screenId)
        {
            GameObject uiScreenInstance = GetUiScreenFromList(screenId);

            if (uiScreenInstance != null)
            {
                uiScreenInstance.SetActive(false);
            }
        }

        private bool AnyScreenShowing()
        {
            return GetTotalActiveUIScreens() > 0;
        }

        private int GetTotalActiveUIScreens()
        {
            return _uiScreenInstanceModels.FindAll(e => e._screenInstance.gameObject.activeInHierarchy).Count;
        }

        private List<UIScreensInstanceModel> GetAllActiveUiScreenReferenceExculdingItself(UiScreen screenID = UiScreen.None)
        {
            return _uiScreenInstanceModels.FindAll(e => e._screenInstance.gameObject.activeInHierarchy && e._screenId != screenID);
        }

        private GameObject GetUiScreenFromList(UiScreen objectName)
        {
            var instance = _uiScreens.FirstOrDefault(obj => obj._myScreenName == objectName);

            if (instance != null)
            {
                return instance.gameObject;
            }
            else
            {
                Debug.LogWarning("Object pool doesn't have a prefab for the object with name " + objectName);
                return null;
            }
        }
    }
}