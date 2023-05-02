using TMPro;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using Zenject.Signals;
using Tutor.GameTutorials;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Tutor
{
    public class TutorialSceneContainer : MonoBehaviour
    {
        [SerializeField] private Button buttonCloseTextDialog;
        [SerializeField] private Button buttonApplicationReload;
        [SerializeField] private TextMeshProUGUI textDialog;

        [Inject] private SignalBus _signalBus;
        [Inject] private UITutorial _uiTutorial;


        private void OnEnable()
        {
            _uiTutorial.Construct();
        }


        private void OnDisable()
        {
            _uiTutorial.Deconstruct();
        }


        private void Start()
        {
            buttonCloseTextDialog.onClick.AddListener(() => _signalBus.Fire(new CloseTextDialog()));
            buttonApplicationReload.onClick.AddListener(() => SceneManager.LoadScene(0));
            buttonApplicationReload.gameObject.SetActive(false);

            _uiTutorial.InitializeTutorial(this);
            _uiTutorial.RunAsync(0).Forget();
        }

        public void ShowDialogText(string textKey) => textDialog.text = textKey;
        public void ActivateButtonQuit() => buttonApplicationReload.gameObject.SetActive(true);
    }
}