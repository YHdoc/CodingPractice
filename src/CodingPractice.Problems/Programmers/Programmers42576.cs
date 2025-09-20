using System;
using System.Collections.Generic;
using System.Linq;
using CodingPractice.Core.Base;
using CodingPractice.Core.Models;
using CodingPractice.Core.Interfaces;

namespace CodingPractice.Problems.Programmers
{
    /// <summary>
    /// 프로그래머스 42576번: 완주하지 못한 선수
    /// </summary>
    public class Programmers42576 : BaseProblem
    {
        public override string ProblemId => "programmers-42576";
        public override string Title => "완주하지 못한 선수";
        public override string Description => "마라톤에 참여한 선수들의 이름이 담긴 배열 participant와 완주한 선수들의 이름이 담긴 배열 completion이 주어질 때, 완주하지 못한 선수의 이름을 return 하도록 solution 함수를 작성해주세요.";
        public override ProblemSite Site => ProblemSite.Programmers;
        public override DifficultyLevel Difficulty => DifficultyLevel.Easy;
        public override List<ProblemTag> Tags => new() { ProblemTag.HashTable, ProblemTag.String, ProblemTag.Sorting };
        public override string Url => "https://programmers.co.kr/learn/courses/30/lessons/42576";

        public override object Solve(object input)
        {
            if (input is (string[] participant, string[] completion))
            {
                return Solution(participant, completion);
            }
            
            throw new ArgumentException("잘못된 입력 형식입니다.");
        }

        public string Solution(string[] participant, string[] completion)
        {
            // 해시맵을 사용한 해결 방법
            var completionCount = new Dictionary<string, int>();
            
            // 완주자 명단을 해시맵에 카운트
            foreach (var name in completion)
            {
                completionCount[name] = completionCount.GetValueOrDefault(name, 0) + 1;
            }
            
            // 참가자 명단을 확인하여 완주하지 못한 선수 찾기
            foreach (var name in participant)
            {
                if (!completionCount.ContainsKey(name) || completionCount[name] == 0)
                {
                    return name;
                }
                completionCount[name]--;
            }
            
            return string.Empty; // 이론적으로 도달하지 않음
        }

        public override List<TestCase> GetTestCases()
        {
            return new List<TestCase>
            {
                new TestCase
                {
                    Name = "기본 테스트",
                    Input = (new[] { "leo", "kiki", "eden" }, new[] { "eden", "kiki" }),
                    ExpectedOutput = "leo",
                    Description = "leo가 완주하지 못함"
                },
                new TestCase
                {
                    Name = "동명이인 테스트",
                    Input = (new[] { "marina", "josipa", "nikola", "vinko", "filipa" }, new[] { "josipa", "filipa", "marina", "nikola" }),
                    ExpectedOutput = "vinko",
                    Description = "vinko가 완주하지 못함"
                },
                new TestCase
                {
                    Name = "동명이인 2명 테스트",
                    Input = (new[] { "mislav", "stanko", "mislav", "ana" }, new[] { "stanko", "ana", "mislav" }),
                    ExpectedOutput = "mislav",
                    Description = "mislav가 2명 중 1명만 완주"
                }
            };
        }
    }
}
