using System;
using System.Collections.Generic;

namespace CodingPractice.Core.Interfaces
{
    /// <summary>
    /// 모든 문제 구현이 따라야 하는 기본 인터페이스
    /// </summary>
    public interface IProblem
    {
        /// <summary>
        /// 문제의 고유 식별자
        /// </summary>
        string ProblemId { get; }
        
        /// <summary>
        /// 문제 제목
        /// </summary>
        string Title { get; }
        
        /// <summary>
        /// 문제 설명
        /// </summary>
        string Description { get; }
        
        /// <summary>
        /// 문제 사이트
        /// </summary>
        Models.ProblemSite Site { get; }
        
        /// <summary>
        /// 난이도
        /// </summary>
        Models.DifficultyLevel Difficulty { get; }
        
        /// <summary>
        /// 문제 태그들
        /// </summary>
        List<Models.ProblemTag> Tags { get; }
        
        /// <summary>
        /// 문제 URL
        /// </summary>
        string Url { get; }
        
        /// <summary>
        /// 문제를 실행하는 메서드
        /// </summary>
        /// <param name="input">입력 데이터</param>
        /// <returns>출력 결과</returns>
        object Solve(object input);
        
        /// <summary>
        /// 테스트 케이스들을 반환
        /// </summary>
        /// <returns>테스트 케이스 리스트</returns>
        List<TestCase> GetTestCases();
        
        /// <summary>
        /// 문제 정보를 반환
        /// </summary>
        /// <returns>문제 메타데이터</returns>
        Models.ProblemInfo GetProblemInfo();
    }

    /// <summary>
    /// 테스트 케이스 정보
    /// </summary>
    public class TestCase
    {
        public string Name { get; set; } = string.Empty;
        public object Input { get; set; } = new();
        public object ExpectedOutput { get; set; } = new();
        public string Description { get; set; } = string.Empty;
    }
}
