using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;



namespace Tests
{
    public static class TestExtensions
    {
        public static float volume(this Vector2 aVector)
        {
            return aVector.x * aVector.y;
        }
    }
    
    public class TestRectSplitting
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestBasicRectSplitVertical()
        {
            // Use the Assert class to test condition
            var testRect = new Rect(0,0,10,10);
            var results = PuzzleSplitter.SplitRect(testRect, RectTransform.Axis.Vertical, 5);

            Assert.AreEqual( 5, results.Item1.width);
            Assert.AreEqual(5, results.Item2.width );
            Assert.AreEqual(5,  results.Item2.x);
            var ActualMagnitude = results.Item1.size.volume() + results.Item2.size.volume(); 
            Assert.AreEqual(testRect.size.volume(), ActualMagnitude);
        }
        
        [Test]
        public void TestBasicRectSplitHorizontal()
        {
            // Use the Assert class to test condition
            var testRect = new Rect(0,0,10,10);
            var results = PuzzleSplitter.SplitRect(testRect, RectTransform.Axis.Horizontal, 5);

            Assert.AreEqual(results.Item1.height, 5);
            Assert.AreEqual(results.Item2.height, 5);
            Assert.AreEqual(results.Item2.x, 0);
            Assert.AreEqual(results.Item2.y, 5);
            var ActualMagnitude = results.Item1.size.volume() + results.Item2.size.volume(); 
            Assert.AreEqual(testRect.size.volume(), ActualMagnitude);
        }
        
        [Test]
        public void TestSplitRectRandom()
        {
            // Use the Assert class to test condition
            var testRect = new Rect(0,0,10,10);
            var results = PuzzleSplitter.SplitRect(testRect, 3);
            Assert.True(results.HasValue);
            Assert.Greater(results.Value.Item1.size.magnitude, 9);
            Assert.Greater(results.Value.Item2.size.magnitude,9);
            var ActualMagnitude = results.Value.Item1.size.volume() + results.Value.Item2.size.volume(); 
            Assert.AreEqual(testRect.size.volume(), ActualMagnitude);
        }
        
        [Test]
        public void TestSplitRectRandomList()
        {
            // Use the Assert class to test condition
            var testRect = new Rect(0,0,10,10);
            var results = PuzzleSplitter.SplitRect(testRect, 2, 10);
            
            Assert.Greater(results.Count, 5);
            var totalMagnitude = results.Sum(x => x.size.volume());
            Assert.AreEqual(testRect.size.volume(), totalMagnitude);
        }


        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestRectSplittingWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
