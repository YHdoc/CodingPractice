using System;
using System.Collections.Generic;
using CodingPractice.Core.Interfaces;
using CodingPractice.Core.Models;

namespace CodingPractice.Core.Base
{
    /// <summary>
    /// 모든 문제 구현의 기본 클래스
    /// </summary>
    public abstract class BaseProblem : IProblem
    {
        public abstract string ProblemId { get; }
        public abstract string Title { get; }
        public abstract string Description { get; }
        public abstract ProblemSite Site { get; }
        public abstract DifficultyLevel Difficulty { get; }
        public abstract List<ProblemTag> Tags { get; }
        public abstract string Url { get; }

        public abstract object Solve(object input);
        public abstract List<TestCase> GetTestCases();

        public virtual ProblemInfo GetProblemInfo()
        {
            return new ProblemInfo
            {
                Id = ProblemId,
                Title = Title,
                Description = Description,
                Site = Site,
                Difficulty = Difficulty,
                Tags = Tags,
                Url = Url,
                SolvedDate = DateTime.Now,
                SolutionPath = GetType().FullName ?? string.Empty
            };
        }

        /// <summary>
        /// 테스트 케이스를 실행하고 결과를 검증합니다
        /// </summary>
        public virtual void RunTests()
        {
            Console.WriteLine($"=== {Title} 테스트 실행 ===");
            Console.WriteLine($"문제 ID: {ProblemId}");
            Console.WriteLine($"사이트: {Site}");
            Console.WriteLine($"난이도: {Difficulty}");
            Console.WriteLine($"태그: {string.Join(", ", Tags)}");
            Console.WriteLine();

            var testCases = GetTestCases();
            var passedTests = 0;
            var totalTests = testCases.Count;

            for (int i = 0; i < testCases.Count; i++)
            {
                var testCase = testCases[i];
                Console.WriteLine($"테스트 {i + 1}: {testCase.Name}");
                
                try
                {
                    var startTime = DateTime.Now;
                    var result = Solve(testCase.Input);
                    var endTime = DateTime.Now;
                    var executionTime = endTime - startTime;

                    Console.WriteLine($"입력: {testCase.Input}");
                    Console.WriteLine($"예상 출력: {testCase.ExpectedOutput}");
                    Console.WriteLine($"실제 출력: {result}");
                    Console.WriteLine($"실행 시간: {executionTime.TotalMilliseconds:F2}ms");

                    if (AreEqual(result, testCase.ExpectedOutput))
                    {
                        Console.WriteLine("✅ 통과");
                        passedTests++;
                    }
                    else
                    {
                        Console.WriteLine("❌ 실패");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ 오류 발생: {ex.Message}");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"테스트 결과: {passedTests}/{totalTests} 통과");
            Console.WriteLine();
        }

        /// <summary>
        /// 두 객체가 같은지 비교합니다 (기본 구현)
        /// </summary>
        protected virtual bool AreEqual(object actual, object expected)
        {
            if (actual == null && expected == null) return true;
            if (actual == null || expected == null) return false;
            return actual.Equals(expected);
        }
    }
}
