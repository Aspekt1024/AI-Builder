using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectButton : MonoBehaviour, IButton {

    protected Image flashImage;
    private Coroutine animationRoutine;

    private GameObject wallObject;
    private LevelCreator levelCreator;

    private enum States
    {
        None, Held, Pressed, Released, Clicked
    }
    private States state;

    private void Start()
    {
        levelCreator = FindObjectOfType<LevelCreator>();
        flashImage = GetComponent<Image>();
        wallObject = (GameObject)Resources.Load("Prefabs/Walls/Wall_0");
    }
    
    private void Update()
    {
        switch (state)
        {
            case States.None:
                break;
            case States.Pressed:
                state = States.Held;
                if (animationRoutine != null) StopCoroutine(animationRoutine);
                animationRoutine = StartCoroutine(ShowFlash());
                break;
            case States.Released:
                state = States.None;
                if (animationRoutine != null) StopCoroutine(animationRoutine);
                animationRoutine = StartCoroutine(FadeFlash());
                break;
            case States.Held:
                break;
            case States.Clicked:
                if (animationRoutine != null) StopCoroutine(animationRoutine);
                animationRoutine = StartCoroutine(FadeFlash());
                state = States.None;
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (state == States.Held || state == States.Clicked) return;
        state = States.Pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (state == States.Held)
        {
            state = States.Released;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        state = States.Clicked;
        Clicked();
    }

    public virtual void Clicked()
    {
        PlaceableObject obj = Instantiate(wallObject).GetComponent<PlaceableObject>();
        levelCreator.SetCurrentObject(obj);
    }

    private IEnumerator ShowFlash()
    {
        float targetAlpha = 0.45f;
        while (flashImage.color.a > targetAlpha)
        {
            float alpha = Mathf.Lerp(flashImage.color.a, targetAlpha, Time.deltaTime * 10f);
            flashImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeFlash()
    {
        while (flashImage.color.a < 1f)
        {
            float alpha = Mathf.Lerp(flashImage.color.a, 1f, Time.deltaTime * 5f);
            flashImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
        flashImage.color = Color.clear;
    }
}
