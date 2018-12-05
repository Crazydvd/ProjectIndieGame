using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    private List<GameObject> _numbers = new List<GameObject>();
    private bool _started = false;
    private int _currentIndex;

    private float _timer = 1;

    // Use this for initialization
    void Start()
    {
        Transform countdown = transform.Find("Countdown");
        _numbers.Add(countdown.Find("Rumble").gameObject);
        _numbers.Add(countdown.Find("1").gameObject);
        _numbers.Add(countdown.Find("2").gameObject);
        _numbers.Add(countdown.Find("3").gameObject);

        _currentIndex = _numbers.Count - 1;
    }

    private void Update()
    {
        if (_started)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                _timer = 1;
                _numbers[_currentIndex].SetActive(false);

                _currentIndex--;

                if (_currentIndex == -1)
                {
                    _timer = 1;
                    _currentIndex = _numbers.Count - 1;
                    _started = false;
                    return;
                }

                _numbers[_currentIndex].SetActive(true);
            }
        }
    }

    public void StartTimer()
    {
        _started = true;
        _numbers[_currentIndex].SetActive(true);
    }
}