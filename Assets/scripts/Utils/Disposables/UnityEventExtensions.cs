using System;
using UnityEngine.Events;

namespace Assets.scripts.Utils.Disposables
{
    public static class UnityEventExtensions
    {
        public static IDisposable Subscribe(this UnityEvent unityEvent, UnityAction call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposable(() => unityEvent.RemoveAllListeners());
        }
        
        public static IDisposable Subscribe<TType>(this UnityEvent<TType> unityEvent, UnityAction<TType> call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposable(() => unityEvent.RemoveAllListeners());
        }
    }
}
