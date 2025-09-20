using System;
using System.Collections.Generic;
using System.Linq;
using CodingPractice.Core.Models;

namespace CodingPractice.Core.Services
{
    /// <summary>
    /// 문제 등록 및 검색을 위한 서비스 구현체
    /// </summary>
    public class ProblemRegistry : IProblemRegistry
    {
        private readonly Dictionary<string, Interfaces.IProblem> _problems = new();
        private readonly Dictionary<ProblemSite, List<Interfaces.IProblem>> _problemsBySite = new();
        private readonly Dictionary<DifficultyLevel, List<Interfaces.IProblem>> _problemsByDifficulty = new();
        private readonly Dictionary<ProblemTag, List<Interfaces.IProblem>> _problemsByTag = new();

        public void RegisterProblem(Interfaces.IProblem problem)
        {
            if (problem == null)
                throw new ArgumentNullException(nameof(problem));

            _problems[problem.ProblemId] = problem;

            // 사이트별 인덱스
            if (!_problemsBySite.ContainsKey(problem.Site))
                _problemsBySite[problem.Site] = new List<Interfaces.IProblem>();
            _problemsBySite[problem.Site].Add(problem);

            // 난이도별 인덱스
            if (!_problemsByDifficulty.ContainsKey(problem.Difficulty))
                _problemsByDifficulty[problem.Difficulty] = new List<Interfaces.IProblem>();
            _problemsByDifficulty[problem.Difficulty].Add(problem);

            // 태그별 인덱스
            foreach (var tag in problem.Tags)
            {
                if (!_problemsByTag.ContainsKey(tag))
                    _problemsByTag[tag] = new List<Interfaces.IProblem>();
                _problemsByTag[tag].Add(problem);
            }
        }

        public Interfaces.IProblem? GetProblem(string problemId)
        {
            return _problems.TryGetValue(problemId, out var problem) ? problem : null;
        }

        public List<Interfaces.IProblem> GetAllProblems()
        {
            return _problems.Values.ToList();
        }

        public List<Interfaces.IProblem> GetProblemsBySite(ProblemSite site)
        {
            return _problemsBySite.TryGetValue(site, out var problems) ? problems.ToList() : new List<Interfaces.IProblem>();
        }

        public List<Interfaces.IProblem> GetProblemsByDifficulty(DifficultyLevel difficulty)
        {
            return _problemsByDifficulty.TryGetValue(difficulty, out var problems) ? problems.ToList() : new List<Interfaces.IProblem>();
        }

        public List<Interfaces.IProblem> GetProblemsByTag(ProblemTag tag)
        {
            return _problemsByTag.TryGetValue(tag, out var problems) ? problems.ToList() : new List<Interfaces.IProblem>();
        }

        public List<Interfaces.IProblem> SearchProblems(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetAllProblems();

            var lowerKeyword = keyword.ToLowerInvariant();
            
            return _problems.Values
                .Where(p => p.Title.ToLowerInvariant().Contains(lowerKeyword) ||
                           p.Description.ToLowerInvariant().Contains(lowerKeyword) ||
                           p.ProblemId.ToLowerInvariant().Contains(lowerKeyword))
                .ToList();
        }
    }
}
