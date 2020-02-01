using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PuzzleGraphTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PuzzleGraphRayOverlapTestVerticalCases()
        {
            var vec1 = new Vector2(0, 0);
            var vec2 = new Vector2(0, 3);
            var vec3 = new Vector2(0, 6);
            var vec4 = new Vector2(0, 9);

            var ARays = (new Segment2D(vec1, vec2), new Segment2D(vec3, vec4));
            var AResult = ARays.Item1.getRightAngleOverlap(ARays.Item2);
            Assert.IsFalse(AResult.HasValue);
            
            var BRays = (new Segment2D(vec1, vec3), new Segment2D(vec2, vec4));
            var BResult = BRays.Item1.getRightAngleOverlap(BRays.Item2);
            Assert.AreEqual(3, (BResult.Value.end - BResult.Value.begin).magnitude);

            var CRays = (new Segment2D(vec2, vec4), new Segment2D(vec3, vec1));
            var CResult = CRays.Item1.getRightAngleOverlap(CRays.Item2);
            Assert.AreEqual(3, (CResult.Value.end - CResult.Value.begin).magnitude);
            
            var DRays = (new Segment2D(vec1, vec4), new Segment2D(vec2, vec3));
            var DResult = DRays.Item1.getRightAngleOverlap(DRays.Item2);
            Assert.AreEqual(DResult.Value, new Segment2D(vec2, vec3));
            
            var ERays = (new Segment2D(vec2, vec3), new Segment2D(vec1, vec4));
            var EResult = ERays.Item1.getRightAngleOverlap(ERays.Item2);
            Assert.AreEqual(EResult.Value, new Segment2D(vec2, vec3));
            
            var FRays = (new Segment2D(vec2, vec3), new Segment2D(vec1, vec2));
            var FResult = FRays.Item1.getRightAngleOverlap(FRays.Item2);
            Assert.False(FResult.HasValue);
            
            var GRays = (new Segment2D(vec1, vec4), new Segment2D(vec3, vec4));
            var GResult = GRays.Item1.getRightAngleOverlap(GRays.Item2);
            Assert.AreEqual(GResult, new Segment2D(vec3, vec4));

        }
        
        [Test]
        public void PuzzleGraphRayOverlapTestHorizontalCases()
        {
            var vec1 = new Vector2( 0,0);
            var vec2 = new Vector2( 3,0);
            var vec3 = new Vector2( 6,0);
            var vec4 = new Vector2( 9,0);

            var ARays = (new Segment2D(vec1, vec2), new Segment2D(vec3, vec4));
            var AResult = ARays.Item1.getRightAngleOverlap(ARays.Item2);
            Assert.IsFalse(AResult.HasValue);
            
            var BRays = (new Segment2D(vec1, vec3), new Segment2D(vec2, vec4));
            var BResult = BRays.Item1.getRightAngleOverlap(BRays.Item2);
            Assert.AreEqual(3, (BResult.Value.end - BResult.Value.begin).magnitude);

            var CRays = (new Segment2D(vec2, vec4), new Segment2D(vec3, vec1));
            var CResult = CRays.Item1.getRightAngleOverlap(CRays.Item2);
            Assert.AreEqual(3, (CResult.Value.end - CResult.Value.begin).magnitude);
            
            var DRays = (new Segment2D(vec1, vec4), new Segment2D(vec2, vec3));
            var DResult = DRays.Item1.getRightAngleOverlap(DRays.Item2);
            Assert.AreEqual(DResult.Value, new Segment2D(vec2, vec3));
            
            var ERays = (new Segment2D(vec2, vec3), new Segment2D(vec1, vec4));
            var EResult = ERays.Item1.getRightAngleOverlap(ERays.Item2);
            Assert.AreEqual(EResult.Value, new Segment2D(vec2, vec3));
            
            var FRays = (new Segment2D(vec2, vec3), new Segment2D(vec1, vec2));
            var FResult = FRays.Item1.getRightAngleOverlap(FRays.Item2);
            Assert.False(FResult.HasValue);

        }


        /// <summary>
        /// test abandoned until I sort this out
        /// </summary>
        public void GetOuterSegmentsTest_LiteralCornerCase()
        {
            
            var vec1 = new Vector2(0,0);
            var vec2 = new Vector2(0,10);
            var vec3 = new Vector2(10,10);
            var vec4 = new Vector2(10,0);
            var vec5 = new Vector2(1,1);
            var vec6 = new Vector2(1,0);
            var vec7 = new Vector2(0,1);
            
            var rect1 = new Rect(vec1, vec5);
            var bounds = new Rect(vec1, vec3);

            var result = rect1.toEdgeList(bounds);
            // Assert.AreEqual(2, result.Count);
            // Assert.Contains(new Segment2D(vec7, vec5), result);

        }


        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PuzzleGraphTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
