using System;
using System.Collections.Generic;
using CodingPractice.Core.Base;
using CodingPractice.Core.Models;
using CodingPractice.Core.Interfaces;

namespace CodingPractice.Problems.Baekjoon
{
    /// <summary>
    /// 백준 1000번: A+B
    /// </summary>
    public class Baekjoon1000 : BaseProblem
    {
        public override string ProblemId => "baekjoon-1000";
        public override string Title => "A+B";
        public override string Description => "두 정수 A와 B를 입력받은 다음, A+B를 출력하는 프로그램을 작성하시오.";
        public override ProblemSite Site => ProblemSite.Baekjoon;
        public override DifficultyLevel Difficulty => DifficultyLevel.Easy;
        public override List<ProblemTag> Tags => new() { ProblemTag.Implementation, ProblemTag.Math };
        public override string Url => "https://www.acmicpc.net/problem/1000";

        public override object Solve(object input)
        {
            if (input is string inputStr)
            {
                var parts = inputStr.Split(' ');
                if (parts.Length >= 2 && 
                    int.TryParse(parts[0], out int a) && 
                    int.TryParse(parts[1], out int b))
                {
                    return a + b;
                }
            }
            
            throw new ArgumentException("잘못된 입력 형식입니다.");
        }

        public override List<TestCase> GetTestCases()
        {
            return new List<TestCase>
            {
                new TestCase
                {
                    Name = "기본 테스트",
                    Input = "1 2",
                    ExpectedOutput = 3,
                    Description = "1 + 2 = 3"
                },
                new TestCase
                {
                    Name = "음수 테스트",
                    Input = "-1 1",
                    ExpectedOutput = 0,
                    Description = "-1 + 1 = 0"
                },
                new TestCase
                {
                    Name = "큰 수 테스트",
                    Input = "1000000 2000000",
                    ExpectedOutput = 3000000,
                    Description = "1000000 + 2000000 = 3000000"
                }
            };
        }
    }
}
