using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class PuzzleEdge : Tuple<PuzzleVertex, PuzzleVertex>
{
    public bool contains(PuzzleVertex item)
    {
        return this.Item1 == item || this.Item2 == item;
    }
    
    public PuzzleEdge(PuzzleVertex item1, PuzzleVertex item2):base(item1, item2)
    {
    }
}

public class PuzzleVertex
{
    
    
    public Rect Position
    {
        get;
        private set;
    }
    
    public List<PuzzleEdge> connectedEdges = new List<PuzzleEdge>();

    public PuzzleVertex(Rect aRect)
    {
        
    }

}

public struct Segment2D
{
    public Vector2 begin;
    public Vector2 end;

    public Segment2D(Vector2 begin, Vector2 end)
    {
        this.begin = begin;
        this.end = end;
    }
}




public static class PuzzleGraphExtensions
{
    public enum VectorSelection
    {
        origin,
        direction
    }

    public static bool raysAreDisjoint(Segment2D testRay, Segment2D ray1, Segment2D ray2)
    {
        return (testRay.begin == ray1.end && testRay.end == ray2.begin) ||
               (testRay.begin == ray2.end && testRay.end == ray1.begin) ||
               (testRay.begin == ray2.end && testRay.end == ray1.begin) ||
               (testRay.begin == ray2.end && testRay.end == ray1.begin);
    }
    
    public static Segment2D? getRightAngleOverlap(this Segment2D sourceRay, Segment2D testRay)
    {
        var parallelOnX = sourceRay.begin.x == testRay.begin.x && 
                          sourceRay.end.x == testRay.end.x;
        var parallelOnY = sourceRay.begin.y == testRay.begin.y && 
                          sourceRay.end.y == testRay.end.y;
        if (!parallelOnX && !parallelOnY) return null;

        var tList = new List<Vector2>()
        {
            sourceRay.begin,
            sourceRay.end,
            testRay.begin,
            testRay.end
        };

        var sorted = parallelOnX
            ? tList.OrderBy(x => x.y)
            : tList.OrderBy(x => x.x);
        
        var output = sorted.ToList();
        var result = new Segment2D(output[1], output[2]);
        if (raysAreDisjoint(result, sourceRay, testRay)) return null;
        return result;
    }

    public static List<Segment2D> toEdgeList(this Rect aRect, Rect? bounds = null)
    {
        List<Segment2D> boundSegments = new List<Segment2D>();
        if (bounds is Rect boundValue)
        {
            boundSegments = toEdgeList(boundValue, new List<Segment2D>());
        }

        return toEdgeList(aRect, boundSegments);
    }
    
    private static List<Segment2D> toEdgeList(Rect aRect, List<Segment2D> boundSegments)
    {
        var result = new List<Segment2D>();
        var cornerList = new List<Vector2>()
        {
            new Vector2(aRect.xMin, aRect.yMin),
            new Vector2(aRect.xMin, aRect.yMax),
            new Vector2(aRect.xMax, aRect.yMax),
            new Vector2(aRect.xMax, aRect.yMin),
        };
        
        for (int i = 0; i < 4; i++)
        {
            var tRay = new Segment2D(cornerList[i], cornerList[(i+1)%4]);
            if(boundSegments.Any(x => tRay.getRightAngleOverlap(x).HasValue)) continue;
            result.Add(tRay);
        }

        return result;
    }
    
    public static PuzzleGraph toPuzzleGraph(this List<Rect> rectList)
    {
        throw new NotImplementedException();
    }
}


public class PuzzleGraph : MonoBehaviour
{

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
