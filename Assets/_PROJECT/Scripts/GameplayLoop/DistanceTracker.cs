using Cysharp.Threading.Tasks;
using SanyaBeerExtension;
using System;
using System.Threading;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] private Transform _startpoint;
    [SerializeField] private Transform _nubic;

    [SerializeField] private float _delay = 0.25f;

    public event Action<int> OnTracked;
    public int DistantionTracked { get; private set; } = 0;

    private CancellationTokenSource _tokenSource;

    private void OnDestroy()
    {
        _tokenSource?.Cancel();
        _tokenSource?.Dispose();
    }

    public async UniTaskVoid StartTrackAsync()
    {
        _tokenSource = new CancellationTokenSource();

        while (_tokenSource.IsCancellationRequested == false)
        {
            DistantionTracked = (int)Mathf.Max(0, _nubic.position.x - _startpoint.position.x);
            OnTracked?.Invoke(DistantionTracked);

            await UniTask.Delay(_delay.ToDelayMillisecond(), cancellationToken: _tokenSource.Token);
        }
    }

    public void StopTrack()
    {
        _tokenSource?.Cancel();
    }
}