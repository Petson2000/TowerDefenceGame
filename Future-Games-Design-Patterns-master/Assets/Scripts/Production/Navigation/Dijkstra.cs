using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Tools;

namespace AI
{
    public class Dijkstra : IPathFinder
    {
        public List<Vector2Int> accessable;

        //Ancestor is previous position x and y. no need to check these again for a path.

        public Dijkstra(List<Vector2Int> accessableTiles)
        {
            accessable = accessableTiles;
        }

        public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
        {
            Vector2Int currentNode = start;

            Queue<Vector2Int> frontier = new Queue<Vector2Int>(new[] {currentNode});

            Dictionary<Vector2Int, Vector2Int> ancestors = new Dictionary<Vector2Int, Vector2Int>()
                {{currentNode, default}};

            frontier.Enqueue(currentNode);

            while (frontier.Count > 0)
            {
                currentNode = frontier.Dequeue();

                if (currentNode == goal)
                {
                    break;
                }

                foreach (Vector2Int vec2Int in DirectionTools.Dirs)
                {
                    Vector2Int next = currentNode + vec2Int;
                    if (accessable.Contains(next))
                    {
                        if (!ancestors.ContainsKey(next))
                        {
                            frontier.Enqueue(next);
                            ancestors.Add(next, currentNode);
                        }
                    }
                }
            }

            if (!ancestors.ContainsKey(goal))
            {
                //Path is not possible
                return null;
            }

            List<Vector2Int> path = new List<Vector2Int>();

            if (ancestors.ContainsKey(goal)) //Found a path
            {
                //Create for loop that goes backwards,

                for (int i = ancestors.Count - 1; i >= 0; i--)
                {
                    path.Add(currentNode);
                    currentNode = ancestors.ElementAt(i).Key;
                }
            }

            path.Reverse();
            return path;
        }
    }
}