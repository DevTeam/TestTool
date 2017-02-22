namespace DevTeam.TestEngine
{
    using System;

    internal class Disposable : IDisposable
    {
        private readonly Action _disposableAction;

        public Disposable(Action disposableAction)
        {
            if (disposableAction == null) throw new ArgumentNullException(nameof(disposableAction));
            _disposableAction = disposableAction;
        }

        public void Dispose()
        {
            _disposableAction();
        }

        public override string ToString()
        {
            return $"{nameof(Disposable)}";
        }
    }
}
