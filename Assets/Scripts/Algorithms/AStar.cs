using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Maze;
using UnityEngine;
using Utilities;

namespace Algorithms
{
    public class PathMarker
    {
        public MapLocation location;
        public float G;
        public float H;
        public float F;
        public PathMarker parent;

        public PathMarker(MapLocation l, float g, float h, float f, PathMarker p)
        {
            location = l;
            G = g;
            H = h;
            F = f;
            parent = p;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return location.Equals(((PathMarker) obj).location);
            }

        }

        public override int GetHashCode()
        {
            return 0;
        }
    }

    public class AStar
    {
        public MazeBase maze;

        List<PathMarker> open = new List<PathMarker>();
        List<PathMarker> closed = new List<PathMarker>();


        PathMarker goalNode;
        PathMarker startNode;

        PathMarker lastPos;
        bool done = false;
        public bool IsCompletedSuccessfully { get; private set; }

        // void RemoveAllMarkers()
        // {
        //     GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");
        //     foreach (GameObject m in markers)
        //         Destroy(m);
        // }

        public AStar(MazeBase maze, PathMarker goalNode, PathMarker startNode)
        {
            this.maze = maze;
            this.goalNode = goalNode;
            this.startNode = startNode;
        }

        void BeginSearch()
        {
            done = false;
            //RemoveAllMarkers();

            List<MapLocation> locations = new List<MapLocation>();
            for (int z = 1; z < maze.depth - 1; z++)
            for (int x = 1; x < maze.width - 1; x++)
            {
                if (maze.map[x, z] != 1)
                    locations.Add(new MapLocation(x, z));
            }

            locations.Shuffle();

            // Vector3 startLocation = new Vector3(locations[0].x * maze.scale, 0, locations[0].z * maze.scale);
            // startNode = new PathMarker(new MapLocation(locations[0].x, locations[0].z), 0, 0, 0,
            //                                     Instantiate(start, startLocation, Quaternion.identity), null);


            // Vector3 goalLocation = new Vector3(locations[1].x * maze.scale, 0, locations[1].z * maze.scale);
            // goalNode = new PathMarker(new MapLocation(locations[1].x, locations[1].z), 0, 0, 0,
            //                                     Instantiate(end, goalLocation, Quaternion.identity), null);

            open.Clear();
            closed.Clear();
            open.Add(startNode);
            lastPos = startNode;
        }

        void Search(PathMarker thisNode)
        {
            if (thisNode.Equals(goalNode))
            {
                IsCompletedSuccessfully = true;
                done = true; 
                return;
            } //goal has been found

            foreach (MapLocation dir in maze.directions)
            {
                MapLocation neighbour = dir + thisNode.location;
                if (maze.map[neighbour.x, neighbour.z] == 1) continue;
                if (neighbour.x < 1 || neighbour.x >= maze.width || neighbour.z < 1 || neighbour.z >= maze.depth) continue;
                if (IsClosed(neighbour)) continue;

                float G = Vector2.Distance(thisNode.location.ToVector(), neighbour.ToVector()) + thisNode.G;
                float H = Vector2.Distance(neighbour.ToVector(), goalNode.location.ToVector());
                float F = G + H;

                // GameObject pathBlock = Instantiate(pathP, new Vector3(neighbour.x * maze.scale, 0, neighbour.z * maze.scale),
                //                                             Quaternion.identity);

                // TextMesh[] values = pathBlock.GetComponentsInChildren<TextMesh>();
                // values[0].text = "G: " + G.ToString("0.00");
                // values[1].text = "H: " + H.ToString("0.00");
                // values[2].text = "F: " + F.ToString("0.00");

                if (!UpdateMarker(neighbour, G, H, F, thisNode))
                    open.Add(new PathMarker(neighbour, G, H, F, thisNode));
            }

            open = open.OrderBy(p => p.F).ToList<PathMarker>();
            PathMarker pm = (PathMarker) open.ElementAt(0);
            closed.Add(pm);

            open.RemoveAt(0);
            //pm.marker.GetComponent<Renderer>().material = closedMaterial;

            lastPos = pm;
        }

        bool UpdateMarker(MapLocation pos, float g, float h, float f, PathMarker prt)
        {
            foreach (PathMarker p in open)
            {
                if (p.location.Equals(pos))
                {
                    p.G = g;
                    p.H = h;
                    p.F = f;
                    p.parent = prt;
                    return true;
                }
            }
            return false;
        }

        bool IsClosed(MapLocation marker)
        {
            foreach (PathMarker p in closed)
            {
                if (p.location.Equals(marker)) return true;
            }
            return false;
        }

        public void PerformAStar()
        {
            BeginSearch();
            while (!done)
                Search(lastPos);
            //maze.InitialiseMap();
            MarkPath();
            //maze.DrawMap();
        }

        void MarkPath()
        {
            // RemoveAllMarkers();
            PathMarker begin = lastPos;

            while (!startNode.Equals(begin) && begin != null)
            {
                //Instantiate(pathP, new Vector3(begin.location.x * maze.scale, 0, begin.location.z * maze.scale),
                //Quaternion.identity);
                maze.map[begin.location.x, begin.location.z] = 0;
                begin = begin.parent;
            }

            //Instantiate(pathP, new Vector3(startNode.location.x * maze.scale, 0, startNode.location.z * maze.scale),
            //Quaternion.identity);
        }

        void GetPath()
        {
            // RemoveAllMarkers();
            PathMarker begin = lastPos;

            while (!startNode.Equals(begin) && begin != null)
            {
                // Instantiate(pathP, new Vector3(begin.location.x * maze.scale, 0, begin.location.z * maze.scale),
                //     Quaternion.identity);
                begin = begin.parent;
            }

            // Instantiate(pathP, new Vector3(startNode.location.x * maze.scale, 0, startNode.location.z * maze.scale),
            //         Quaternion.identity);
        }

        public List<Vector2Int> GetPathCells()
        {
            var pathCells = new List<Vector2Int>();
            foreach (var marker in closed)
            {
                pathCells.Add(new Vector2Int(marker.location.x,marker.location.z));
            }

            return pathCells;
        }
    }
}