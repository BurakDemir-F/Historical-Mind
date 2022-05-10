using System.Collections.Generic;
using UnityEngine;

namespace Algorithms
{
    public class NeighborData
    {
        private readonly List<bool> _neighborStatuses;

        public NeighborData(List<bool> neighborStatuses)
        {
            _neighborStatuses = neighborStatuses;
        }

        public void SetNeighborStatus(int side)
        {
            if(side >= _neighborStatuses.Count || side < 0)
            {
                Debug.Log("Something going wrong here");
                return;
            }

            _neighborStatuses[side] = true;
        }

        public List<bool> GetNeighborStatuses()
        {
            return _neighborStatuses;
        }
    }
}