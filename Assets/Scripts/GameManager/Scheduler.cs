using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShootEmUp
{
    public class Scheduler : IService
    {
        private readonly Dictionary<DateTime, Action> _scheduledActions;
        private readonly List<DateTime> _actionsToRemove;
        private readonly CancellationTokenSource _tickCts;

        public event Action<Action> TimeElapsed;

        public Scheduler()
        {
            _scheduledActions = new Dictionary<DateTime, Action>();
            _actionsToRemove = new List<DateTime>();
            _tickCts = new CancellationTokenSource();
        }

        public bool TrySchedule(DateTime at, Action action)
        {
            if (at < DateTime.Now || _scheduledActions.ContainsKey(at))
                return false;

            _scheduledActions.Add(at, action);

            return true;
        }

        public bool TryAbort(DateTime timeKey)
        {
            if (_scheduledActions.ContainsKey(timeKey))
                return _scheduledActions.Remove(timeKey);

            return false;
        }

        private async Task TickAsync()
        {
            while (!_tickCts.IsCancellationRequested)
            {
                foreach (var scheduledAction in _scheduledActions)
                {
                    if (scheduledAction.Key > DateTime.Now)
                    {
                        TimeElapsed?.Invoke(scheduledAction.Value);
                        _actionsToRemove.Add(scheduledAction.Key);
                    }
                }

                foreach (DateTime timeKey in _actionsToRemove)
                {
                    _scheduledActions.Remove(timeKey);
                }

                await Task.Yield();
            }
        }
    }
}