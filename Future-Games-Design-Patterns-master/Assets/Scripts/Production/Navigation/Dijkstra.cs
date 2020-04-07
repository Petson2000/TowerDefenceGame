using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace AI
{
    public class Dijkstra : IPathFinder
    {
        private readonly HashSet<Vector2Int> accessablePositions;

        public Dijkstra(IEnumerable<Vector2Int> accessables)
        {
            accessablePositions = new HashSet<Vector2Int>(accessables);
        }

        public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
        {
            Dictionary<Vector2Int, Vector2Int?> ancestors = new Dictionary<Vector2Int, Vector2Int?>();
            ancestors.Add(start, default);

            Queue<Vector2Int> frontier = new Queue<Vector2Int>();
            frontier.Enqueue(start);

            while (frontier.Count > 0)
            {
                Vector2Int current = frontier.Dequeue();

                if (current == goal)
                {
                    break;
                }

                foreach (Vector2Int dir in DirectionTools.Dirs)
                {
                    Vector2Int next = current + dir;

                    if (accessablePositions.Contains(next) && ancestors.ContainsKey(next) == false)
                    {
                        ancestors[next] = current;
                        frontier.Enqueue(next);
                    }
                }
            }

            if (ancestors.ContainsKey(goal))
            {
                List<Vector2Int> path = new List<Vector2Int>();
                
                for (Vector2Int? run = goal; run.HasValue; run = ancestors[run.Value])
                {
                    path.Add(run.Value);
                }

                path.Reverse();
                return path;
            }

            return Enumerable.Empty<Vector2Int>();
        }
    }
}