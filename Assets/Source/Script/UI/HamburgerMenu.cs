using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
public class HamburgerMenu : MonoBehaviour
{
    public GameObject hamMenuPanel;
    public CanvasGroup bgMenu;
    [SerializeField] private InputActionReference pauseAction;
    void OnEnable() {
        pauseAction.action.performed += ctx => ToggleMenu();
    }
    void OnDisable() {
        pauseAction.action.performed -= ctx => ToggleMenu();
    }

    private void ToggleMenu() {
        if (!hamMenuPanel.activeSelf)
            HamburgerAnimation();
        else
            HamburgerAnimationExit();
    }
    public void HamburgerAnimation()
    {
        StartCoroutine(OnHamburgerClick());
    }

    private IEnumerator OnHamburgerClick()
    {
        hamMenuPanel.SetActive(true);
        Image ham = hamMenuPanel.GetComponent<Image>();
        ham.DOFade(0.5f, 0.5f);
        yield return new WaitForSeconds(.3f);
        bgMenu.DOFade(1, 0.5f);
        EventManager.instance.WhenPausedGame(true);
    }
    public void HamburgerAnimationExit()
    {
        StartCoroutine(OnHamburgerExit());
    }
    private IEnumerator OnHamburgerExit()
    {
        bgMenu.DOFade(0, 0.5f);
        yield return new WaitForSeconds(.3f);
        Image ham = hamMenuPanel.GetComponent<Image>();
        ham.DOFade(0, 0.5f);
        yield return new WaitForSeconds(.5f);
        hamMenuPanel.SetActive(false);
        EventManager.instance.WhenPausedGame(false);
    }
}
