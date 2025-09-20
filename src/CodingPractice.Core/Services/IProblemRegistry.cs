using System;
using System.Collections.Generic;
using CodingPractice.Core.Models;

namespace CodingPractice.Core.Services
{
    /// <summary>
    /// 문제 등록 및 검색을 위한 서비스 인터페이스
    /// </summary>
    public interface IProblemRegistry
    {
        /// <summary>
        /// 문제를 등록합니다
        /// </summary>
        /// <param name="problem">등록할 문제</param>
        void RegisterProblem(Interfaces.IProblem problem);
        
        /// <summary>
        /// 문제 ID로 문제를 찾습니다
        /// </summary>
        /// <param name="problemId">문제 ID</param>
        /// <returns>찾은 문제 또는 null</returns>
        Interfaces.IProblem? GetProblem(string problemId);
        
        /// <summary>
        /// 모든 문제를 반환합니다
        /// </summary>
        /// <returns>문제 리스트</returns>
        List<Interfaces.IProblem> GetAllProblems();
        
        /// <summary>
        /// 사이트별로 문제를 필터링합니다
        /// </summary>
        /// <param name="site">문제 사이트</param>
        /// <returns>해당 사이트의 문제들</returns>
        List<Interfaces.IProblem> GetProblemsBySite(ProblemSite site);
        
        /// <summary>
        /// 난이도별로 문제를 필터링합니다
        /// </summary>
        /// <param name="difficulty">난이도</param>
        /// <returns>해당 난이도의 문제들</returns>
        List<Interfaces.IProblem> GetProblemsByDifficulty(DifficultyLevel difficulty);
        
        /// <summary>
        /// 태그별로 문제를 필터링합니다
        /// </summary>
        /// <param name="tag">태그</param>
        /// <returns>해당 태그의 문제들</returns>
        List<Interfaces.IProblem> GetProblemsByTag(ProblemTag tag);
        
        /// <summary>
        /// 키워드로 문제를 검색합니다
        /// </summary>
        /// <param name="keyword">검색 키워드</param>
        /// <returns>검색된 문제들</returns>
        List<Interfaces.IProblem> SearchProblems(string keyword);
    }
}
