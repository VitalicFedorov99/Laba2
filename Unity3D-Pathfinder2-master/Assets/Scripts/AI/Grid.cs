using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject nodeModel;      //   Шаблон узла сетки
    [SerializeField] private Terrain landscape;
    [SerializeField] private float gridDelta = 20;
   
    [SerializeField] private GlobalPathFinder globalPathFinder;

    [SerializeField] private Color[] colorsDotRegion;

    private int updateAtFrame = 0;

    private PNode[,] grid = null;

    private int id=0;

    private void CheckWalkableNodes()
    {
        foreach(PNode node in grid)
        {
            node.walkable = true;
            
            node.walkable = !Physics.CheckSphere(node.worldPosition, 1) && (node.worldPosition.y >= 10);
            if(node.worldPosition.y<10)
            {
                node.walkable=false;
                Debug.Log("ниже нужного");
            }

            if (node.walkable)
                node.Illuminate();
            else
                node.Fade();
        }
    }

    private void NodeInGlobalRegion()
    {
        GlobalRegion[] region=globalPathFinder.GetGlobalRegion();
        foreach(PNode node in grid)
        {
            for(int i=0;i<region.Length;i++)
            {
               bool isBelongs=false;
               isBelongs=region[i].CheckDotInRegion(node.worldPosition);
               if(isBelongs==true && node.body.transform.position.y>10)
               {
                   node.body.GetComponent<PNodeInGlobalRegion>().globalRegion=region[i];
                   node.body.GetComponent<Renderer>().material.color=colorsDotRegion[i];
                   region[i]._nodeRegion.Add(node.body);
               }
            }
        }
    }

    private List<Vector2Int> GetNeighbours(Vector2Int current)
    {	
        List<Vector2Int> nodes = new List<Vector2Int>();

        for (int x = current.x - 1; x <= current.x + 1; ++x)
            for (int y = current.y - 1; y <= current.y + 1; ++y)
                if(x >= 0 && x < grid.GetLength(0) && y>=0 && y < grid.GetLength(1) && (x != current.x || y != current.y) && grid[x,y].walkable)
                nodes.Add(new Vector2Int(x, y));
        return nodes;
    }

   

    // Метод вызывается однократно при создании экземпляра класса
   /* void Start()
    {
        Vector3 terrainSize = landscape.terrainData.bounds.size;
        int sizeX = (int)(terrainSize.x / gridDelta);
        int sizeZ = (int)(terrainSize.z / gridDelta);

        grid = new PNode[sizeX, sizeZ];
        for(int x = 0; x<sizeX; ++x)
            for(int z = 0; z<sizeZ; ++z)
            {
                Vector3 position = new Vector3(x * gridDelta, 0, z * gridDelta);
                position.y = landscape.SampleHeight(position)+1;
                grid[x, z] = new PNode(nodeModel, true, position);
                //  Каждый узел массива знает своё место
                grid[x, z].gridIndex = new Vector2Int(x, z);
                grid[x, z].Fade();
            }
        CheckWalkableNodes();
        NodeInGlobalRegion();
    }
    */

    public void Setup()
    {
         Vector3 terrainSize = landscape.terrainData.bounds.size;
        int sizeX = (int)(terrainSize.x / gridDelta);
        int sizeZ = (int)(terrainSize.z / gridDelta);

        grid = new PNode[sizeX, sizeZ];
        for(int x = 0; x<sizeX; ++x)
            for(int z = 0; z<sizeZ; ++z)
            {
                Vector3 position = new Vector3(x * gridDelta, 0, z * gridDelta);
                position.y = landscape.SampleHeight(position)+1;
                grid[x, z] = new PNode(nodeModel, true, position,id,gameObject);
                //  Каждый узел массива знает своё место
                grid[x, z].gridIndex = new Vector2Int(x, z);
                grid[x, z].Fade();
                id++;
                
            }
        CheckWalkableNodes();
        NodeInGlobalRegion();
    }

    // Метод вызывается каждый кадр
    void Update()
    {
        if (Time.frameCount < updateAtFrame) return;
        updateAtFrame = Time.frameCount + 5000;
        //Debug.Log("Pathfinding started");

        //CalculatePath(new Vector2Int(0, 0), new Vector2Int(grid.GetLength(0) - 3, grid.GetLength(1) - 3));
    }
}
