using UnityEngine.Events;

namespace realima.asterioidz
{
    public interface IDestroyable
    {
        /// <summary>
        /// This function is triggered from the collision of two objects. Supposed to be both IDestroyables
        /// </summary>
        /// <param name="destroyer"> The other object that collided</param>
        /// <returns>Destruction value</returns>
        int DestroyInstance(IDestroyable destroyer = null);
    }
}