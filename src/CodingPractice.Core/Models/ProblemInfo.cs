using System;
using System.Collections.Generic;

namespace CodingPractice.Core.Models
{
    /// <summary>
    /// 문제 정보를 담는 메타데이터 클래스
    /// </summary>
    public class ProblemInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProblemSite Site { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public List<ProblemTag> Tags { get; set; } = new();
        public string Url { get; set; } = string.Empty;
        public DateTime SolvedDate { get; set; }
        public TimeSpan? SolveTime { get; set; }
        public string SolutionPath { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int MemoryUsage { get; set; }
        public int TimeComplexity { get; set; }
        public int SpaceComplexity { get; set; }
    }

    /// <summary>
    /// 문제 사이트 열거형
    /// </summary>
    public enum ProblemSite
    {
        Baekjoon,
        Programmers,
        LeetCode,
        HackerRank,
        CodeForces,
        AtCoder,
        Other
    }

    /// <summary>
    /// 난이도 레벨
    /// </summary>
    public enum DifficultyLevel
    {
        Easy = 1,
        Medium = 2,
        Hard = 3,
        Expert = 4
    }

    /// <summary>
    /// 문제 태그 (알고리즘 유형)
    /// </summary>
    public enum ProblemTag
    {
        // 기본 자료구조
        Array,
        String,
        HashTable,
        LinkedList,
        Stack,
        Queue,
        Tree,
        BinaryTree,
        BinarySearchTree,
        Graph,
        Heap,
        Trie,
        
        // 알고리즘
        Sorting,
        Searching,
        BinarySearch,
        TwoPointers,
        SlidingWindow,
        Greedy,
        DynamicProgramming,
        Backtracking,
        Recursion,
        DivideAndConquer,
        BitManipulation,
        Math,
        
        // 그래프 알고리즘
        BFS,
        DFS,
        TopologicalSort,
        ShortestPath,
        MinimumSpanningTree,
        UnionFind,
        
        // 기타
        Simulation,
        Implementation,
        BruteForce,
        PrefixSum,
        MonotonicStack,
        MonotonicQueue
    }
}
