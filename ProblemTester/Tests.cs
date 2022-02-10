
using AI_Project1;
using System.Diagnostics;
using Xunit;

namespace ProblemTester
{
   public  class ProblemTester
    {

        [Theory]
        [InlineData("3, 10, 15", "18")]
        [InlineData("2,3,5,19,121,852", "11443")]
        [InlineData("3,5", "4")]
        public void IsSolvable(string input, string goal)
        {
            var problem = Problem.Init(new string[] { input, goal });
            var solution = problem.Search();
            Assert.NotNull(solution);
        }


        [Theory]
        [InlineData("3, 6", "10")]
        [InlineData("2,4,10", "101")]
        [InlineData("2", "143")]
        public void NotSolvable(string input, string goal)
        {
            var problem = Problem.Init(new string[] { input, goal });
            var solution = problem.Search();
            Assert.Null(solution);
        }


        [Theory]
        [InlineData("3, 5, 10", "18",6)]
        [InlineData("2,3,5,19,121,852", "11443",36)]
        [InlineData("3,5,8,10", "207", 43)]
        [InlineData("3,5,9", "34", 11)]
        [InlineData("2,5,6,72", "143", 7)]
        [InlineData("3,5", "4",7)]
        [InlineData("3,5", "17",9)]
        [InlineData("3,5", "14",8)]
        [InlineData("2, 4, 5", "8",4)]
        [InlineData("1,94,100", "12",7)]
        [InlineData("1,97,100", "3",3)]
        public void HasFoundCorrentSteps(string input, string goal,float expectedCost)
        {
            var problem = Problem.Init(new string[] { input, goal });
            var solution = problem.Search();
            Assert.Equal(solution.Cost,expectedCost);
        }

        [Theory]
        [InlineData("3, 5, 10", "18", 10000)]
        [InlineData("2,3,5,19,121,852", "11443", 10000)]
        [InlineData("2,3,5,19,121,852", "21443", 10000)]
        [InlineData("3,5,8,10", "207", 10000)]
        public void ShouldNotTakeLongerThan(string input, string goal, long milliseconds)
        {
            var sw = Stopwatch.StartNew();
            sw.Start();
            var problem = Problem.Init(new string[] { input, goal });
            problem.Search();
            sw.Stop();

            Assert.True(sw.ElapsedMilliseconds < milliseconds);
        }
        [Theory]
        [InlineData("3, 5, 10", "18", 6)]
        [InlineData("2,3,5,19,121,852", "11443", 36)]
        [InlineData("3,5,8,10", "207", 43)]
        [InlineData("3,5,9", "34", 11)]
        [InlineData("2,5,6,72", "143", 7)]
        [InlineData("3,5", "4", 7)]
        [InlineData("3,5", "17", 9)]
        [InlineData("3,5", "14", 8)]
        [InlineData("1,94,100", "12", 7)]
        [InlineData("1,97,100", "3", 3)]
        [InlineData("3,70,110,4000", "4064", 44)]
     
        public void Is_Admissable(string input, string goal, float cost  )
        {
            var problem = Problem.Init(new string[] { input, goal.ToString() });
            var solution = problem.Search();

            float maxEstimation=0;
            while (solution!=null)
            {
                if (solution.Distance > maxEstimation)
                {
                    maxEstimation = solution.Distance;
                };
                solution = solution.Parent;
            }

            Assert.True(maxEstimation <= cost);
        }


    }
}
