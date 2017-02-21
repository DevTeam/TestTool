namespace DevTeam.TestEngine
{
    using System;
    using Contracts;

    internal class EventAggergator: IEventAggergator
    {
        public IDisposable Subscribe<T>(IObserver<T> observer)
        {
            return null;
        }

        public void Publish<T>(T value)
        {
        }
    }
}
