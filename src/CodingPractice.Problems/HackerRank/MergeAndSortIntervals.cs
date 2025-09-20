using System;
using System.Collections.Generic;
using CodingPractice.Core.Base;
using CodingPractice.Core.Models;
using CodingPractice.Core.Interfaces;

public class MergeAndSortIntervals : BaseProblem
{
    public override string ProblemId => "Hackerrank-MergeAndSortIntervals";
    public override string Title => "Merge and Sort Intervals";
    public override string Description => @"Given an array of intervals [startTime, endTime], merge all overlapping intervals and return a sorted array of non-overlapping intervals.";
    public override ProblemSite Site => ProblemSite.HackerRank;
    public override DifficultyLevel Difficulty => DifficultyLevel.Medium;
    public override List<ProblemTag> Tags => new() { ProblemTag.Sorting };
    public override string Url => "https://www.hackerrank.com/contests/software-engineer-prep-kit/challenges/merge-and-sort-intervals/problem?isFullScreen=true";

    public override object Solve(object input)
    {
        if (input is List<List<int>> intervals)
        {
            if (intervals.Count < 1)
            {
                return intervals;
            }

            List<List<int>> mergedList = new List<List<int>>();
            intervals = intervals.OrderBy(x => x[0]).ToList();

            mergedList.Add(intervals[0]);

            foreach (List<int> item in intervals.Skip(1))
            {
                List<int> mergedOne = mergedList.Last();
                if (mergedOne.Last() >= item.First())
                {
                    mergedOne[1] = Math.Max(mergedOne.Last(), item.Last());
                }
                else 
                {
                    mergedList.Add(item);
                }
            }

            object result = String.Join("\n", mergedList.Select(x=>String.Join(" ", x)));
            return result;
        }

        throw new ArgumentException("잘못된 입력 형식입니다.");
    }

    public override List<TestCase> GetTestCases()
    {
        return new List<TestCase>
            {
                new TestCase
                {
                    Name = "기본 테스트1",
                    Input = new List<List<int>>
                            {
                                new List<int> { 1, 3 },
                                new List<int> { 2, 6 },
                                new List<int> { 8, 10 },
                                new List<int> { 15, 18 }
                            },
                    ExpectedOutput = "1 6\n8 10\n15 18",
                    Description = ""
                },
                new TestCase
                {
                    Name = "기본 테스트2",
                    Input = new List<List<int>>
                            {
                                new List<int> { 5, 10 }
                            },
                    ExpectedOutput = "5 10",
                    Description = ""
                }
            };
    }
}
