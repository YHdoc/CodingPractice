using System;
using System.Collections.Generic;
using System.Linq;
using CodingPractice.Core.Base;
using CodingPractice.Core.Models;
using CodingPractice.Core.Interfaces;

namespace CodingPractice.Problems.Programmers
{
    /// <summary>
    /// 프로그래머스 388351번: 유연근무제
    /// </summary>
    public class Programmers388351 : BaseProblem
    {
        public override string ProblemId => "programmers-388351";
        public override string Title => "유연근무제";
        public override string Description => "직원들이 설정한 출근 희망 시각과 실제로 출근한 기록을 바탕으로 상품을 받을 직원이 몇 명인지 알수 있도록 solution 함수를 작성해주세요.";
        public override ProblemSite Site => ProblemSite.Programmers;
        public override DifficultyLevel Difficulty => DifficultyLevel.Easy;
        public override List<ProblemTag> Tags => new() { ProblemTag.HashTable, ProblemTag.String, ProblemTag.Sorting };
        public override string Url => "https://school.programmers.co.kr/learn/courses/30/lessons/388351";

        public override object Solve(object input)
        {
            if (input is (int[] schedules, int[,] timelogs, int startday))
            {
                return Solution(schedules, timelogs, startday);
            }

            throw new ArgumentException("잘못된 입력 형식입니다.");
        }
        private int ToMinutes(int hhmm)
        {
            int h = hhmm / 100;
            int m = hhmm % 100;
            return h * 60 + m;
        }

        public string Solution(int[] schedules, int[,] timelogs, int startday)
        {
            int answer = 0;

            for (int i = 0; i < schedules.Length; i++)
            {
                bool allOnTime = true; // 기본 가정: 지각 없음

                for (int j = 0; j < 7; j++)
                {
                    int day = (startday - 1 + j) % 7; // 0=월, 1=화, ..., 6=일
                    if (day >= 5) continue; // 토,일 건너뜀

                    int hope = ToMinutes(schedules[i]);
                    int limit = hope + 10; // 희망 출근 시각 + 10분
                    int actual = ToMinutes(timelogs[i, j]);

                    if (actual > limit) // 평일 중 하루라도 지각이면 탈락
                    {
                        allOnTime = false;
                        break;
                    }
                }

                if (allOnTime) answer++;
            }

            return answer.ToString();
        }


        public override List<TestCase> GetTestCases()
        {
            return new List<TestCase>
            {
                new TestCase
                {
                    Name = "예제 테스트 1",
                    Input = (
                        new[] { 700, 800, 1100 },
                        new int[,] {
                            {710, 2359, 1050, 700, 650, 631, 659},
                            {800, 801, 805, 800, 759, 810, 809},
                            {1105, 1001, 1002, 600, 1059, 1001, 1100}
                        },
                        5
                    ),
                    ExpectedOutput = "3",
                    Description = "모든 직원이 평일에 늦지 않고 출근 → 상품 3명"
                },
                new TestCase
                {
                    Name = "예제 테스트 2",
                    Input = (
                        new[] { 730, 855, 700, 720 },
                        new int[,] {
                            {710, 700, 650, 735, 700, 931, 912},
                            {908, 901, 805, 815, 800, 831, 835},
                            {705, 701, 702, 705, 710, 710, 711},
                            {707, 731, 859, 913, 934, 931, 905}
                        },
                        1
                    ),
                    ExpectedOutput = "2",
                    Description = "첫 번째, 세 번째 직원만 평일 모두 제시간 출근 → 상품 2명"
                },
                new TestCase
                {
                    Name = "주말 시작 테스트",
                    Input = (
                        new[] { 800, 900 },
                        new int[,] {
                            {800, 805, 759, 810, 815, 800, 801},
                            {859, 901, 902, 859, 900, 905, 910}
                        },
                        6
                    ),
                    ExpectedOutput = "1",
                    Description = "이벤트가 토요일에 시작 → 토·일은 무시, 첫 번째 직원만 평일 정상 출근"
                },
                new TestCase
                {
                    Name = "모두 지각 테스트",
                    Input = (
                        new[] { 700, 800 },
                        new int[,] {
                            {715, 720, 800, 900, 1000, 705, 701},
                            {900, 905, 1000, 950, 1200, 850, 900}
                        },
                        1
                    ),
                    ExpectedOutput = "0",
                    Description = "모든 직원이 평일 최소 하루 이상 지각 → 상품 없음"
                },
                new TestCase
                {
                    Name = "한 명만 테스트",
                    Input = (
                        new[] { 1000 },
                        new int[,] {
                            {1005, 1009, 1010, 1002, 1008, 1010, 1000}
                        },
                        3
                    ),
                    ExpectedOutput = "1",
                    Description = "직원 한 명이 평일에 모두 제시간 출근 → 상품 1명"
                }
            };
        }

    }
}
