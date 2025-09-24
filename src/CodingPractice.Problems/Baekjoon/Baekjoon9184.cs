using System;
using System.Collections.Generic;
using CodingPractice.Core.Base;
using CodingPractice.Core.Models;
using CodingPractice.Core.Interfaces;

namespace CodingPractice.Problems.Baekjoon
{
    /// <summary>
    /// 백준 9184번: 신나는 함수 실행
    /// </summary>
    public class Baekjoon9184 : BaseProblem
    {
        public override string ProblemId => "baekjoon-9184";
        public override string Title => "신나는 함수 실행";
        public override string Description => "재귀함수를 ";
        public override ProblemSite Site => ProblemSite.Baekjoon;
        public override DifficultyLevel Difficulty => DifficultyLevel.Medium;
        public override List<ProblemTag> Tags => new() { ProblemTag.DynamicProgramming, ProblemTag.Recursion };
        public override string Url => "https://www.acmicpc.net/problem/9184";

        public override object Solve(object input)
        {
            if (input is List<List<int>> inputs)
            {
                var results = new List<string>();

                foreach (var line in inputs)
                {
                    if (line.Count != 3)
                        continue;

                    int a = line[0];
                    int b = line[1];
                    int c = line[2];

                    if (a == -1 && b == -1 && c == -1)
                        break;

                    int value = Do(a, b, c);
                    results.Add($"w({a}, {b}, {c}) = {value}");
                }

                return string.Join("\n", results);
            }

            throw new ArgumentException("잘못된 입력 형식입니다.");
        }

        static int[,,] dp = new int[21, 21, 21];

        private int Do(int a, int b, int c)
        {
            if (a <= 0 || b <= 0 || c <= 0) return 1;
            if (a > 20 || b > 20 || c > 20) return Do(20, 20, 20);

            if (dp[a, b, c] != 0) return dp[a, b, c]; // 이미 계산했으면 캐싱된 값 사용

            if (a < b && b < c)
                dp[a, b, c] = Do(a, b, c - 1)
                            + Do(a, b - 1, c - 1)
                            - Do(a, b - 1, c);
            else
                dp[a, b, c] = Do(a - 1, b, c)
                            + Do(a - 1, b - 1, c)
                            + Do(a - 1, b, c - 1)
                            - Do(a - 1, b - 1, c - 1);

            return dp[a, b, c];
        }



        public override List<TestCase> GetTestCases()
        {
            return new List<TestCase>
            {
                new TestCase
                {
                    Name = "예제 입력 1",
                    Input = new List<List<int>>
                    {
                        new List<int>{1, 1, 1},
                        new List<int>{2, 2, 2},
                        new List<int>{10, 4, 6},
                        new List<int>{50, 50, 50},
                        new List<int>{-1, 7, 18},
                        new List<int>{-1, -1, -1}
                    },
                    ExpectedOutput =string.Format("w(1, 1, 1) = 2\nw(2, 2, 2) = 4\nw(10, 4, 6) = 523\nw(50, 50, 50) = 1048576\nw(-1, 7, 18) = 1"),
                    Description = "문제 예제 입력/출력"
                },
                new TestCase
                {
                    Name = "기저 조건 (a<=0)",
                    Input = new List<List<int>>
                    {
                        new List<int>{0, 5, 5},
                        new List<int>{-1, -1, -1}
                    },
                    ExpectedOutput = "w(0, 5, 5) = 1",
                    Description = "a가 0 → 항상 1 반환"
                },
                new TestCase
                {
                    Name = "기저 조건 (b<=0)",
                    Input = new List<List<int>>
                    {
                        new List<int>{5, -3, 7},
                        new List<int>{-1, -1, -1}
                    },
                    ExpectedOutput = "w(5, -3, 7) = 1",
                    Description = "b가 음수 → 항상 1 반환"
                },
                new TestCase
                {
                    Name = "상한 조건 (20 초과)",
                    Input = new List<List<int>>
                    {
                        new List<int>{21, 21, 21},
                        new List<int>{-1, -1, -1}
                    },
                    ExpectedOutput = "w(21, 21, 21) = 1048576",
                    Description = "20 초과는 w(20,20,20)과 동일"
                },
                new TestCase
                {
                    Name = "작은 값 테스트",
                    Input = new List<List<int>>
                    {
                        new List<int>{1, 2, 3},
                        new List<int>{-1, -1, -1}
                    },
                    ExpectedOutput = "w(1, 2, 3) = 2",
                    Description = "a<b<c 분기 테스트"
                },
                new TestCase
                {
                    Name = "일반 케이스",
                    Input = new List<List<int>>
                    {
                        new List<int>{5, 5, 5},
                        new List<int>{-1, -1, -1}
                    },
                    ExpectedOutput = "w(5, 5, 5) = 32",
                    Description = "균등한 값 (5,5,5)"
                },
                new TestCase
                {
                    Name = "비대칭 케이스",
                    Input = new List<List<int>>
                    {
                        new List<int>{2, 7, 10},
                        new List<int>{-1, -1, -1}
                    },
                    ExpectedOutput = "w(2, 7, 10) = 104",
                    Description = "a<b<c 분기 아닌 일반 재귀"
                },
                new TestCase
                {
                    Name = "여러 줄 입력",
                    Input = new List<List<int>>
                    {
                        new List<int>{1, 1, 1},
                        new List<int>{2, 2, 2},
                        new List<int>{3, 3, 3},
                        new List<int>{-1, -1, -1}
                    },
                    ExpectedOutput = string.Format("w(1, 1, 1) = 2\nw(2, 2, 2) = 4\nw(3, 3, 3) = 8"),
                    Description = "여러 줄 연속 처리"
                }
            };
        }


    }
}
