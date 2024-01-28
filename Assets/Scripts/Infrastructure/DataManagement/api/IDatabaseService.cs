using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public interface IDatabaseService
    {
        IEnumerable<T> Get<T>() where T : Object;
        void Load<T>(string path) where T : Object;
    }
}