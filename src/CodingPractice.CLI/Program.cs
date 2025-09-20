using System;
using System.Collections.Generic;
using System.Linq;
using CodingPractice.Core.Services;
using CodingPractice.Core.Interfaces;
using CodingPractice.Core.Models;
using CodingPractice.Core.Base;
using CodingPractice.Problems.Baekjoon;
using CodingPractice.Problems.Programmers;
using CodingPractice.Problems.etc;

namespace CodingPractice.CLI
{
    class Program
    {
        private static readonly IProblemRegistry _problemRegistry = new ProblemRegistry();

        static void Main(string[] args)
        {
            // 문제들을 자동으로 등록
            RegisterAllProblems();

            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            var command = args[0].ToLowerInvariant();

            switch (command)
            {
                case "list":
                    ListProblems(args);
                    break;
                case "run":
                    RunProblem(args);
                    break;
                case "search":
                    SearchProblems(args);
                    break;
                case "info":
                    ShowProblemInfo(args);
                    break;
                case "help":
                case "--help":
                case "-h":
                    ShowHelp();
                    break;
                default:
                    Console.WriteLine($"알 수 없는 명령어: {command}");
                    ShowHelp();
                    break;
            }
        }

        private static void RegisterAllProblems()
        {
            // 수동으로 문제들을 등록
            try
            {
                _problemRegistry.RegisterProblem(new Baekjoon1000());
                _problemRegistry.RegisterProblem(new Programmers42576());
                _problemRegistry.RegisterProblem(new MergeAndSortIntervals());
                _problemRegistry.RegisterProblem(new DP_MaximumExp());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"문제 등록 중 오류 발생: {ex.Message}");
            }
        }

        private static void ListProblems(string[] args)
        {
            var problems = _problemRegistry.GetAllProblems();
            
            if (args.Length > 1)
            {
                var filter = args[1].ToLowerInvariant();
                switch (filter)
                {
                    case "baekjoon":
                        problems = _problemRegistry.GetProblemsBySite(ProblemSite.Baekjoon);
                        break;
                    case "programmers":
                        problems = _problemRegistry.GetProblemsBySite(ProblemSite.Programmers);
                        break;
                    case "leetcode":
                        problems = _problemRegistry.GetProblemsBySite(ProblemSite.LeetCode);
                        break;
                    case "hackerrank":
                        problems = _problemRegistry.GetProblemsBySite(ProblemSite.HackerRank);
                        break;
                    case "easy":
                        problems = _problemRegistry.GetProblemsByDifficulty(DifficultyLevel.Easy);
                        break;
                    case "medium":
                        problems = _problemRegistry.GetProblemsByDifficulty(DifficultyLevel.Medium);
                        break;
                    case "hard":
                        problems = _problemRegistry.GetProblemsByDifficulty(DifficultyLevel.Hard);
                        break;
                }
            }

            Console.WriteLine($"총 {problems.Count}개의 문제가 있습니다.\n");
            
            foreach (var problem in problems.OrderBy(p => p.Site).ThenBy(p => p.Difficulty))
            {
                Console.WriteLine($"[{problem.Site}] {problem.ProblemId} - {problem.Title} ({problem.Difficulty})");
                Console.WriteLine($"  태그: {string.Join(", ", problem.Tags)}");
                Console.WriteLine($"  URL: {problem.Url}");
                Console.WriteLine();
            }
        }

        private static void RunProblem(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("사용법: run <문제ID>");
                return;
            }

            var problemId = args[1];
            var problem = _problemRegistry.GetProblem(problemId);

            if (problem == null)
            {
                Console.WriteLine($"문제를 찾을 수 없습니다: {problemId}");
                return;
            }

            if (problem is BaseProblem baseProblem)
            {
                baseProblem.RunTests();
            }
            else
            {
                Console.WriteLine($"문제 실행: {problem.Title}");
                var testCases = problem.GetTestCases();
                
                foreach (var testCase in testCases)
                {
                    Console.WriteLine($"테스트: {testCase.Name}");
                    var result = problem.Solve(testCase.Input);
                    Console.WriteLine($"입력: {testCase.Input}");
                    Console.WriteLine($"출력: {result}");
                    Console.WriteLine();
                }
            }
        }

        private static void SearchProblems(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("사용법: search <키워드>");
                return;
            }

            var keyword = string.Join(" ", args.Skip(1));
            var problems = _problemRegistry.SearchProblems(keyword);

            Console.WriteLine($"'{keyword}' 검색 결과: {problems.Count}개\n");
            
            foreach (var problem in problems)
            {
                Console.WriteLine($"[{problem.Site}] {problem.ProblemId} - {problem.Title}");
            }
        }

        private static void ShowProblemInfo(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("사용법: info <문제ID>");
                return;
            }

            var problemId = args[1];
            var problem = _problemRegistry.GetProblem(problemId);

            if (problem == null)
            {
                Console.WriteLine($"문제를 찾을 수 없습니다: {problemId}");
                return;
            }

            var info = problem.GetProblemInfo();
            Console.WriteLine($"=== {info.Title} ===");
            Console.WriteLine($"ID: {info.Id}");
            Console.WriteLine($"사이트: {info.Site}");
            Console.WriteLine($"난이도: {info.Difficulty}");
            Console.WriteLine($"태그: {string.Join(", ", info.Tags)}");
            Console.WriteLine($"URL: {info.Url}");
            Console.WriteLine($"설명: {info.Description}");
            Console.WriteLine();
        }

        private static void ShowHelp()
        {
            Console.WriteLine("=== 코딩 테스트 연습 도구 ===");
            Console.WriteLine();
            Console.WriteLine("프로젝트 구조:");
            Console.WriteLine("  CodingPractice.CLI (시작 프로젝트) ⭐");
            Console.WriteLine("  CodingPractice.Core - 공통 모델 및 인터페이스");
            Console.WriteLine("  CodingPractice.Problems - 문제 구현들");
            Console.WriteLine();
            Console.WriteLine("실행 방법:");
            Console.WriteLine("  루트 디렉토리에서: dotnet run --project src/CodingPractice.CLI [명령어]");
            Console.WriteLine("  CLI 디렉토리에서:  cd src/CodingPractice.CLI && dotnet run [명령어]");
            Console.WriteLine();
            Console.WriteLine("사용법:");
            Console.WriteLine("  list [필터]           - 모든 문제 목록 보기");
            Console.WriteLine("    필터: baekjoon, programmers, leetcode, hackerrank, easy, medium, hard");
            Console.WriteLine("  run <문제ID>          - 특정 문제 실행");
            Console.WriteLine("  search <키워드>       - 문제 검색");
            Console.WriteLine("  info <문제ID>         - 문제 상세 정보 보기");
            Console.WriteLine("  help                  - 도움말 보기");
            Console.WriteLine();
            Console.WriteLine("예시 (루트 디렉토리에서):");
            Console.WriteLine("  dotnet run --project src/CodingPractice.CLI list");
            Console.WriteLine("  dotnet run --project src/CodingPractice.CLI list baekjoon");
            Console.WriteLine("  dotnet run --project src/CodingPractice.CLI run baekjoon-1000");
            Console.WriteLine("  dotnet run --project src/CodingPractice.CLI search array");
            Console.WriteLine("  dotnet run --project src/CodingPractice.CLI info baekjoon-1000");
            Console.WriteLine();
            Console.WriteLine("예시 (CLI 디렉토리에서):");
            Console.WriteLine("  cd src/CodingPractice.CLI");
            Console.WriteLine("  dotnet run list");
            Console.WriteLine("  dotnet run run baekjoon-1000");
            Console.WriteLine("  dotnet run search array");
        }
    }
}