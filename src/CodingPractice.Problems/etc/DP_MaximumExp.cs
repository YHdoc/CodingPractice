using System;
using System.Collections.Generic;
using System.Linq;
using CodingPractice.Core.Base;
using CodingPractice.Core.Models;
using CodingPractice.Core.Interfaces;

namespace CodingPractice.Problems.etc
{
    /// <summary>
    /// 프로그래머스 42576번: 완주하지 못한 선수
    /// </summary>
    public class DP_MaximumExp : BaseProblem
    {
        public override string ProblemId => "etc-DP_MaximumExp";
        public override string Title => "몬스터 처치 경험치 최적화 (Knapsack DP)";
        public override string Description => @"
플레이어는 차례대로 N마리의 몬스터를 만난다.
각 몬스터를 처치하면 **경험치(exp)**를 얻을 수 있지만, 그만큼 **피로도(cost)**를 소비한다.
플레이어는 총 M 이하의 피로도만 사용할 수 있다.

플레이어가 얻을 수 있는 최대 경험치를 구하시오.


🔹 입력 형식

첫째 줄: N M
N = 몬스터 수 (1 ≤ N ≤ 100)
M = 플레이어가 감당 가능한 최대 피로도 (1 ≤ M ≤ 1000)
둘째 줄부터 N개의 줄: 각 줄에 exp cost
exp = 몬스터 처치 시 얻는 경험치 (1 ≤ exp ≤ 1000)
cost = 해당 몬스터를 처치할 때 드는 피로도 (1 ≤ cost ≤ 1000)


🔹 출력 형식

플레이어가 얻을 수 있는 최대 경험치를 출력한다.

🔹 예제 1

입력
3 5
10 3
20 2
15 2


출력
35


설명
몬스터 1 (exp=10, cost=3)
몬스터 2 (exp=20, cost=2)
몬스터 3 (exp=15, cost=2)

M=5 피로도로 고를 수 있는 최대 경험치는
몬스터2 + 몬스터3 → cost=4, exp=35";
        public override ProblemSite Site => ProblemSite.Other;
        public override DifficultyLevel Difficulty => DifficultyLevel.Easy;
        public override List<ProblemTag> Tags => new() { ProblemTag.DynamicProgramming };
        public override string Url => "none";

        public override object Solve(object input)
        {
            if (input is List<List<int>> inputs)
            {
                return Solution(inputs);
            }

            throw new ArgumentException("잘못된 입력 형식입니다.");
        }

        public string Solution(List<List<int>> inputs)
        {
            // inputs[0][0] = N, inputs[0][1] = M
            // inputs[1..N] = { exp, cost }
            // 결과: 최대 경험치 (string으로 반환)

            if(inputs == null || inputs.Count < 1)
            {
                return "0";
            }

            int n = inputs[0][0]; // 몬스터 수
            int m = inputs[0][1]; // 최대 피로도

            // 상태 정의 (State Definition)
            // dp[j]: 총 피로도를 j만큼 사용했을 때 얻을 수 있는 최대 경험치
            int[] dp = new int[m + 1];
            

            for (int i = 1; i <= n; i++)
            {
                int exp = inputs[i][0];
                int cost = inputs[i][1];

                // 뒤에서부터 업데이트해야 같은 몬스터를 중복 선택하지 않음
                for (int j = m; j >= cost; j--)
                {
                    //두 가지 선택지
                    // 이 몬스터를 잡지 않는다 : 이미 구해둔 값 그대로: dp[j]
                    // 이 몬스터를 잡는다 : dp[j - cost] + exp
                    // 그러면 이 몬스터의 cost만큼 피로도를 써야 함

                    // dp[j] : 현재까지 고려한 몬스터들로 피로도 j에서 얻을 수 있는 최대 경험치
                    // 남은 피로도는 j - cost, 남은 피로도로 얻을 수 있는 최대 경험치는 dp[j - cost]
                    // dp[j - cost] + exp : 이번 몬스터를 선택했을 때 경험치
                    dp[j] = Math.Max(dp[j], dp[j - cost] + exp); // 점화식
                }
            }


            return dp[m].ToString();

            throw new NotImplementedException("여기에 DP 로직을 구현하세요.");
        }


        public override List<TestCase> GetTestCases()
        {
            return new List<TestCase>
            {
                new TestCase
                {
                    Name = "최소 입력",
                    Input = new List<List<int>> {
                        new List<int>{1, 1},    // N=1, M=1
                        new List<int>{5, 1}     // exp=5, cost=1
                    },
                    ExpectedOutput = "5",
                    Description = "몬스터 1마리, 피로도 1 허용 → 경험치 5"
                },
                new TestCase
                {
                    Name = "한 몬스터만 가능",
                    Input = new List<List<int>> {
                        new List<int>{2, 3},    // N=2, M=3
                        new List<int>{10, 4},
                        new List<int>{20, 3}
                    },
                    ExpectedOutput = "20",
                    Description = "첫 몬스터는 cost=4라 불가, 두 번째 몬스터만 가능"
                },
                new TestCase
                {
                    Name = "두 마리 모두 가능",
                    Input = new List<List<int>> {
                        new List<int>{2, 5},
                        new List<int>{10, 3},
                        new List<int>{20, 2}
                    },
                    ExpectedOutput = "30",
                    Description = "둘 다 잡아도 cost=5, 경험치 합 30"
                },
                new TestCase
                {
                    Name = "최적 조합 필요",
                    Input = new List<List<int>> {
                        new List<int>{3, 5},
                        new List<int>{10, 3},
                        new List<int>{20, 2},
                        new List<int>{15, 2}
                    },
                    ExpectedOutput = "35",
                    Description = "2번+3번 몬스터 선택이 최적"
                },
                new TestCase
                {
                    Name = "큰 경험치 vs 여러 개 작은 경험치",
                    Input = new List<List<int>> {
                        new List<int>{4, 10},
                        new List<int>{50, 9},
                        new List<int>{20, 5},
                        new List<int>{20, 5},
                        new List<int>{15, 4}
                    },
                    ExpectedOutput = "50",
                    Description = "50(exp) vs 20+20=40 → 50 선택"
                },
                new TestCase
                {
                    Name = "모두 선택 가능",
                    Input = new List<List<int>> {
                        new List<int>{4, 15},
                        new List<int>{10, 3},
                        new List<int>{20, 4},
                        new List<int>{30, 5},
                        new List<int>{25, 2}
                    },
                    ExpectedOutput = "85",
                    Description = "모두 선택해도 cost=14 ≤ 15 → 합 85"
                },
                new TestCase
                {
                    Name = "큰 값 조합",
                    Input = new List<List<int>> {
                        new List<int>{5, 10},
                        new List<int>{100, 6},
                        new List<int>{70, 5},
                        new List<int>{50, 4},
                        new List<int>{40, 3},
                        new List<int>{20, 2}
                    },
                    ExpectedOutput = "150",
                    Description = "100+50=150이 최적 조합"
                },
                new TestCase
                {
                    Name = "선택 불가",
                    Input = new List<List<int>> {
                        new List<int>{3, 2},
                        new List<int>{10, 3},
                        new List<int>{20, 4},
                        new List<int>{15, 5}
                    },
                    ExpectedOutput = "0",
                    Description = "모든 cost가 M보다 커서 선택 불가"
                }
            };
        }


    }
}
