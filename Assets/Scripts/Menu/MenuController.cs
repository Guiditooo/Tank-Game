using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using TMPro;

namespace ZapGames.TankGame
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup startingPanel;
        [SerializeField] private float showSpeed = 1;
        [SerializeField] private float hideSpeed = 1;

        private CanvasGroup actualPanel;

        private void Awake()
        {
            actualPanel = startingPanel;

            foreach (CanvasGroup canvasGroup in GetComponentsInChildren<CanvasGroup>())
            {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }

            actualPanel.alpha = 1;
            actualPanel.blocksRaycasts = true;
            actualPanel.interactable = true;

        }

        private void Start()
        {
            Time.timeScale = 1;
        }

        public void StartPanel(CanvasGroup newPanel)
        {
            StartCoroutine(PanelChange(newPanel));
        }

        IEnumerator MakeItVisible(CanvasGroup panel)
        {
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * showSpeed;
                panel.alpha = t;
                yield return null;
            }
            panel.alpha = 1;
            panel.blocksRaycasts = true;
            panel.interactable = true;
        }
        IEnumerator MakeItInvisible(CanvasGroup panel)
        {
            panel.blocksRaycasts = false;
            panel.interactable = false;
            float t = 1;
            while (t > 0)
            {
                t -= Time.deltaTime * hideSpeed;
                panel.alpha = t;
                yield return null;
            }
            panel.alpha = 0;
        }

        IEnumerator PanelChange(CanvasGroup panel)
        {
            yield return StartCoroutine(MakeItInvisible(actualPanel));
            StartCoroutine(MakeItVisible(panel));
            actualPanel = panel;
        }
        public void CloseGame()
        {
            Debug.Log("SALIENGO DEL JUEGO");
            Application.Quit();
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Gameplay");
        }

        public void ApplyChanges()
        {

        }

    }
}