using System;
using Xunit;
using MathNet.Numerics.LinearAlgebra;
namespace StepRedraw.Math.Tests
{
    public class LineTest
    {
        [Theory]
        [InlineData(new[] { 1.0D, 3 }, new[] { 6.0D, 3 }, new[] { 0.0D, -1, 3 })] //vertical
        [InlineData(new[] { 1.0D, 1 }, new[] { 1.0D, 6 }, new[] { 1.0D, 0, -1 })]  //horisontal
        [InlineData(new[] { 1.0D, 1 }, new[] { 5.0D, 5 }, new[] { 1.0D, -1, 0 })]  //diagonal
        [InlineData(new[] { 1.0D, 2 }, new[] { 4.0D, 6 }, new[] { -4.0D, 3, -2 })]  //any

        public void CreateTest(double[] a, double[] b, double[] expected_raw)
        {
            var A = CreateVector.Dense(a);
            var B = CreateVector.Dense(b);
            var expect = Line.Create(CreateVector.Dense(expected_raw));

            var result1 = Line.Create(A, B);
            var result2 = Line.Create(B, A);

            Assert.Equal(result1, result2, new Line.LineEqualityComparer());
            Assert.Equal(expect, result1, new Line.LineEqualityComparer());
        }

        [Theory]
        [InlineData(new[] { 1.0D, 3 }, new[] { 6.0D, 3 }, new[] { 4.0D, 1 }, new[] { 1.0D, 0, -4 })] //vertical
        [InlineData(new[] { 1.0D, 1 }, new[] { 1.0D, 6 }, new[] { 3.0D, 3 }, new[] { 0.0D, 1, -3 })]  //horisontal
        [InlineData(new[] { 1.0D, 1 }, new[] { 5.0D, 5 }, new[] { 5.0D, 1 }, new[] { 1.0D, 1, -6 })]  //diagonal
        [InlineData(new[] { 1.0D, 2 }, new[] { 4.0D, 6 }, new[] { 5.0D, -1 }, new[] { 3.0D, 4, -11 })]  //any
        public void PerpendicularThroughThePointTest(double[] a, double[] b, double[] c, double[] expected_raw)
        {
            var expected = Line.Create(CreateVector.Dense(expected_raw));

            var A = CreateVector.Dense(a);
            var B = CreateVector.Dense(b);
            var C = CreateVector.Dense(c);

            var line_A_B = Line.Create(A, B);
            var perp_C = line_A_B.PerpendicularThroughThePoint(C);
            Assert.Equal(expected, perp_C, new Line.LineEqualityComparer());
            var mf1 = perp_C.PerpendicularThroughThePoint(A);
            

            Assert.Equal(perp_C.PerpendicularThroughThePoint(A), perp_C.PerpendicularThroughThePoint(B) , new Line.LineEqualityComparer());


        }

        [Theory]
        [InlineData(new[] { 1.0D, 3 }, new[] { 6.0D, 3 }, new[] { 4.0D, 1 }, new[] { 4.0D, 3 })] //vertical
        [InlineData(new[] { 1.0D, 1 }, new[] { 1.0D, 6 }, new[] { 3.0D, 3 }, new[] { 1.0D, 3 })]  //horisontal
        [InlineData(new[] { 1.0D, 1 }, new[] { 5.0D, 5 }, new[] { 5.0D, 1 }, new[] { 3.0D, 3 })]  //diagonal
        [InlineData(new[] { 1.0D, 2 }, new[] { 4.0D, 6 }, new[] { 5.0D, -1 }, new[] { 1.0D, 2 })]  //any
        public void IntersectTest(double[] a, double[] b, double[] c, double[] expected_raw)
        {
            var expected = CreateVector.Dense(expected_raw);

            var A = CreateVector.Dense(a);
            var B = CreateVector.Dense(b);
            var C = CreateVector.Dense(c);

            var line_A_B = Line.Create(A, B);
            var perp_C = line_A_B.PerpendicularThroughThePoint(C);
            var intersect_D = line_A_B.Intersect(perp_C);
            Assert.Equal(expected, intersect_D, new Line.VectorEqualityComparer());
        }
        [Fact(DisplayName = "My Test fail")]
        public void Mf()
        {
            Assert.True(false);
        }

    }
}
