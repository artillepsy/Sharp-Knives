using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class AbstractCanvasManager : MonoBehaviour
    {
        [SerializeField] protected float changeDelayInSeconds = 0.5f;
        private CanvasType _type;
        protected List<IOnCanvasChange> _subs;
        protected void NotifyAll(CanvasType newType)
        {
            _type = newType;
            _subs.ForEach(sub => sub.OnCanvasChange(_type, changeDelayInSeconds));
        }
    }
}