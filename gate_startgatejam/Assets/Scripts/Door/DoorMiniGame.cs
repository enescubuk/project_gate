using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class DoorMiniGame : MonoBehaviour
{
    private GameObject[] pins = new GameObject[3];
    private int counterPin = 0;
    private bool isPressing = false;
    [SerializeField] private float speed;
    private int _botSide;
    private int _topSide;
    [SerializeField] private int correctAreaGap;
    [SerializeField] private int correctPinGap;
    private bool correctZone = false;
    [SerializeField] private UnityEvent onGameOver;
    private void OnEnable()
    {
        SetPins();
        CalculateRandomGap();
    }

    private void SetPins()
    {
        pins = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount && i < pins.Length; i++)
        {
            pins[i] = transform.GetChild(i).gameObject;
        }
    }

    private void CalculateRandomGap()
    {
        _botSide = UnityEngine.Random.Range(0, 55-correctAreaGap);
        _topSide = _botSide + correctAreaGap;
    }

    private void Update()
    {
        myInput();
    }

    private void myInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressing = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPressing = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && correctZone)
        {
            Vector2 _pinPos = pins[counterPin].GetComponent<RectTransform>().anchoredPosition;
            _pinPos.y = 22.5f;
            StartCoroutine(SmoothMove(pins[counterPin].GetComponent<RectTransform>(), _pinPos, 0.1f));
            counterPin++;
            if (counterPin >= pins.Length)
            {
                Debug.Log("Game Over");
                Invoke("GameOver", 1f);
            }
            else
            {
                CalculateRandomGap();
            }
        }
    }

    private void GameOver()
    {
        onGameOver.Invoke();
    }

    private System.Collections.IEnumerator SmoothMove(RectTransform rectTransform, Vector2 targetPos, float time)
    {
        Vector2 currentPos = rectTransform.anchoredPosition;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            rectTransform.anchoredPosition = Vector2.Lerp(currentPos, targetPos, t);
            yield return null;
        }

        RectTransform child0 = rectTransform.GetChild(0).GetComponent<RectTransform>();
        RectTransform child1 = rectTransform.GetChild(1).GetComponent<RectTransform>();

        Vector2 child0CurrentPos = child0.anchoredPosition;
        Vector2 child1CurrentPos = child1.anchoredPosition;

        Vector2 child0TargetPos = child0CurrentPos + new Vector2(0, correctPinGap); // +20 ekle
        Vector2 child1TargetPos = child1CurrentPos + new Vector2(0, -correctPinGap); // -20 ekle

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / time;
            child0.anchoredPosition = Vector2.Lerp(child0CurrentPos, child0TargetPos, t);
            child1.anchoredPosition = Vector2.Lerp(child1CurrentPos, child1TargetPos, t);
            yield return null;
        }
    }


    private void FixedUpdate()
    {
        if (isPressing)
        {
            MovingPin();
        }
        else
        {
            FallingDown();
        }
        if (counterPin < pins.Length)
        {
            CheckCorrectPosition();
        }
    }

    private void MovingPin()
    {
        if (counterPin < pins.Length)
        {
            pins[counterPin].transform.Translate(Vector3.up * Time.deltaTime * speed);
            ClampPinPosition(pins[counterPin]);
        }
    }

    private void FallingDown()
    {
        if (counterPin < pins.Length)
        {
            pins[counterPin].transform.Translate(Vector3.down * Time.deltaTime * speed / 2);
            ClampPinPosition(pins[counterPin]);
        }
    }

    private void ClampPinPosition(GameObject pin)
    {
        RectTransform rectTransform = pin.GetComponent<RectTransform>();
        Vector3 position = rectTransform.anchoredPosition;
        position.y = Mathf.Clamp(position.y, 0, 55);
        rectTransform.anchoredPosition = position;
    }

    private void CheckCorrectPosition()
    {
        RectTransform rectTransform = pins[counterPin].GetComponent<RectTransform>();
        if (rectTransform.anchoredPosition.y > _botSide && rectTransform.anchoredPosition.y < _topSide)
        {
            for (int i = 0; i < pins[counterPin].transform.childCount; i++)
            {
                pins[counterPin].transform.GetChild(i).GetComponent<Image>().color = Color.green;
                correctZone = true;
            }
        }
        else
        {
            for (int i = 0; i < pins[counterPin].transform.childCount; i++)
            {
                pins[counterPin].transform.GetChild(i).GetComponent<Image>().color = Color.white;
                correctZone = false;
            }
        }
    }
}
