export interface LeaderboardEntryDto {
  rank: number;
  userId: string;
  username: string;
  totalPoints: number;
  predictionsMade: number;
  exactScores: number;
  correctWinners: number;
}

export interface WeeklyLeaderboardEntryDto {
  rank: number;
  userId: string;
  username: string;
  weeklyPoints: number;
  weeklyBonus: number;
}

export interface UserStatsDto {
  userId: string;
  username: string;
  overallRank: number;
  totalPoints: number;
  predictionsMade: number;
  totalMatches: number;
  exactScores: number;
  winnerAndDiff: number;
  correctWinners: number;
  oneScore: number;
  misses: number;
}
