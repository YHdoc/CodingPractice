# 🚀 코딩 테스트 연습 저장소

개인 코딩 테스트 준비를 위한 문제 저장소입니다. 매일 한 문제 이상씩 풀고 GitHub에 커밋하면서 체계적으로 관리할 수 있습니다.

## 📁 프로젝트 구조

```
CodingPractice/
├── src/
│   ├── CodingPractice.Core/          # 공통 모델, 인터페이스
│   │   ├── Models/                   # 문제 정보, 태그, 난이도 등
│   │   ├── Interfaces/               # IProblem 인터페이스
│   │   ├── Services/                 # 문제 등록 및 검색 서비스
│   │   └── Base/                     # BaseProblem 기본 클래스
│   ├── CodingPractice.Problems/      # 문제별 구현
│   │   ├── Baekjoon/                 # 백준 문제들
│   │   ├── Programmers/              # 프로그래머스 문제들
│   │   ├── LeetCode/                 # 리트코드 문제들
│   │   └── HackerRank/               # 해커랭크 문제들
│   └── CodingPractice.CLI/           # 명령줄 인터페이스
├── docs/                             # 문제별 문서
└── tests/                            # 단위 테스트
```

## 🎯 주요 기능

### 1. 체계적인 문제 분류
- **사이트별 분류**: 백준, 프로그래머스, 리트코드, 해커랭크 등
- **난이도별 분류**: Easy, Medium, Hard, Expert
- **알고리즘 유형별 태그**: Array, HashTable, DynamicProgramming 등

### 2. 자동화된 테스트 실행
- 각 문제마다 테스트 케이스 포함
- 실행 시간 측정
- 자동 검증 및 결과 출력

### 3. 강력한 검색 기능
- 키워드 검색
- 사이트별 필터링
- 난이도별 필터링
- 태그별 필터링

### 4. 문제 메타데이터 관리
- 문제 URL, 설명, 해결 날짜 등 자동 기록
- 시간 복잡도, 공간 복잡도 추적
- 개인 노트 및 복기 내용 저장

## 🚀 사용법

### 기본 명령어

```bash
# 도움말 보기
dotnet run

# 모든 문제 목록 보기
dotnet run list

# 사이트별 문제 목록 보기
dotnet run list baekjoon
dotnet run list programmers
dotnet run list leetcode

# 난이도별 문제 목록 보기
dotnet run list easy
dotnet run list medium
dotnet run list hard

# 특정 문제 실행
dotnet run run baekjoon-1000
dotnet run run programmers-42576

# 문제 검색
dotnet run search array
dotnet run search "dynamic programming"

# 문제 상세 정보 보기
dotnet run info baekjoon-1000
```

### 새 문제 추가하기

1. **적절한 사이트 폴더에 새 클래스 생성**
   ```csharp
   // src/CodingPractice.Problems/Baekjoon/Baekjoon1001.cs
   public class Baekjoon1001 : BaseProblem
   {
       public override string ProblemId => "baekjoon-1001";
       public override string Title => "문제 제목";
       // ... 나머지 구현
   }
   ```

2. **CLI에 문제 등록**
   ```csharp
   // src/CodingPractice.CLI/Program.cs의 RegisterAllProblems() 메서드에 추가
   _problemRegistry.RegisterProblem(new Baekjoon1001());
   ```

3. **테스트 케이스 작성**
   ```csharp
   public override List<TestCase> GetTestCases()
   {
       return new List<TestCase>
       {
           new TestCase
           {
               Name = "기본 테스트",
               Input = "입력값",
               ExpectedOutput = "예상출력",
               Description = "테스트 설명"
           }
       };
   }
   ```

## 📊 문제 분류 체계

### 사이트별 분류
- **Baekjoon**: 백준 온라인 저지
- **Programmers**: 프로그래머스
- **LeetCode**: 리트코드
- **HackerRank**: 해커랭크
- **CodeForces**: 코드포스
- **AtCoder**: 아트코더

### 난이도 분류
- **Easy**: 쉬운 문제
- **Medium**: 중간 문제
- **Hard**: 어려운 문제
- **Expert**: 전문가 수준 문제

### 알고리즘 태그
- **기본 자료구조**: Array, String, HashTable, LinkedList, Stack, Queue, Tree 등
- **알고리즘**: Sorting, Searching, BinarySearch, TwoPointers, Greedy, DynamicProgramming 등
- **그래프**: BFS, DFS, TopologicalSort, ShortestPath 등
- **기타**: Simulation, Implementation, BruteForce 등

## 🔧 개발 환경

- **.NET 8.0** 이상
- **C# 12.0** 이상
- **Visual Studio 2022** 또는 **VS Code** 권장

## 📈 확장 계획

- [ ] 웹 인터페이스 추가
- [ ] 문제 통계 및 분석 기능
- [ ] 자동 문제 추천 시스템
- [ ] GitHub Actions를 통한 자동 빌드 및 테스트
- [ ] 문제별 성능 벤치마크
- [ ] 학습 로드맵 생성

## 🤝 기여하기

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 라이선스

이 프로젝트는 개인 학습 목적으로 만들어졌습니다.

---

**Happy Coding! 🎉**
