using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEditor.Build;
using UnityEngine;
using Random = UnityEngine.Random;


public static class ExtensionManager
{
    public static  Vector3 ToVector3( this Vector2 vec)
    {
        return new Vector3(vec.x, vec.y, 0);
    }

    public static RectTransform toRectTransform(this Rect sourceRect)
    {
        GameObject go = new GameObject("MyGO", typeof(RectTransform));
        var t = (RectTransform) go.transform;
        t.SetPositionAndRotation(sourceRect.position.ToVector3(), new Quaternion());
        t.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sourceRect.height);
        t.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sourceRect.width);
        return t;
    }

    public static T getRandomChoice<T>(this List<T> collection)
    {
        var selection = Random.value * collection.Count;
        return collection[(int) Math.Floor(selection)];
    }
}

public class PuzzleSplitter : MonoBehaviour
{

    public static Texture2D LoadPNG(string filePath) {
 
        Texture2D tex = null;
        byte[] fileData;
 
        if (File.Exists(filePath))     {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    public static List<Rect> getPuzzlePieceBounds(Vector2 bounds, short pieceCount)
    {
        var result = new List<Vector2>();

        var sourceRect = new Rect(new Vector2(), bounds);

        var splitAxis = Random.value < 0.5 ? RectTransform.Axis.Horizontal : RectTransform.Axis.Vertical;
        
        

        return null;
    }

    public static (Rect, Rect)? SplitRect(Rect sourceRect, float minSize)
    {
        var splitAxis = Random.value < 0.5 ? RectTransform.Axis.Horizontal : RectTransform.Axis.Vertical;
        var sourceValue = splitAxis == RectTransform.Axis.Horizontal ? sourceRect.height : sourceRect.width;
        var reservedSpace = (2 * minSize);
        if (reservedSpace > sourceValue) return null;
        var randomTolerance = sourceValue - reservedSpace;
        var adjustment = Random.value * randomTolerance;
        var positionValue = minSize + adjustment;
        return SplitRect(sourceRect, splitAxis, positionValue);
    }
    
    public static List<Rect> SplitRect(Rect sourceRect, float minSize, ushort maxItems=2, ushort sliceAttempts = 15)
    {
        var result = new List<Rect>()
        {
            sourceRect
        };
        var sliceCounter = 0;
        while (result.Count < maxItems && sliceCounter < sliceAttempts)
        {
            var selectedRect = result.getRandomChoice();
            result.Remove(selectedRect);

            var splitResults = SplitRect(selectedRect, minSize);
            sliceCounter++;
            if (splitResults.HasValue)
            {
                result.Add(splitResults.Value.Item1);
                result.Add(splitResults.Value.Item2);
            }
            else
            {
                result.Add(selectedRect);
            }
        }

        return result;
    }


    //if vertical
    public static (Rect, Rect) SplitRect(Rect sourceRect, RectTransform.Axis axis, float position)
    {
        if (axis == RectTransform.Axis.Vertical)
        {
            var rect1Size = new Vector2(position, sourceRect.height);
            var rect1 = new Rect(sourceRect.position, rect1Size);
            var rect2Size = sourceRect.size - rect1Size + new Vector2(0, sourceRect.height);
            var rect2Position = new Vector2(rect1Size.x, sourceRect.y);
            var rect2 = new Rect(rect2Position, rect2Size);
            return (rect1, rect2);
        }
        else
        {
            var rect1Size = new Vector2(sourceRect.width, position);
            var rect1 = new Rect(sourceRect.position, rect1Size);
            var rect2Size = sourceRect.size - rect1Size + new Vector2( sourceRect.width,0 );
            var rect2Position = new Vector2(sourceRect.x, rect1Size.y);
            var rect2 = new Rect(rect2Position, rect2Size);
            return (rect1, rect2);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
