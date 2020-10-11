using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{

    public Transform seeker, target;
    Grid grid;

    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        FindPath(seeker.position, target.position);
        seeker.GetComponent<KakamenaLabyrinth>().lockTarget = false;
        if (grid.path!= null && grid.path.Count != 0)
        {
            seeker.GetComponent<KakamenaLabyrinth>().lockTarget = true;
            float MinDistance = target.GetComponent<TopDownController>().GetSpotlightRadius() / 19;

            float distance = Vector2.Distance(seeker.position, target.transform.position);
            if (distance > MinDistance + .05f)
            {
                seeker.transform.position = Vector2.MoveTowards(seeker.transform.position, grid.path[0].worldPosition, .025f);
            }
            else if (distance < MinDistance - .05f)
            {
                Vector2 director = new Vector2(target.transform.position.x - seeker.position.x, target.transform.position.y - seeker.position.y);
                director.Normalize();
                seeker.Translate(director * 3 * Time.deltaTime);
            }
        }
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        grid.path = null;
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);
        int limit = 300;
        while (openSet.Count > 0 && limit > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
            limit--;
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}