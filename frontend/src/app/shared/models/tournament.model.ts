export interface TournamentDto {
  id: string;
  name: string;
  season: string;
  startDate: string;
  endDate: string;
  isActive: boolean;
}

export interface GameWeekDto {
  id: string;
  tournamentId: string;
  weekNumber: number;
  startDate: string;
  endDate: string;
}

export interface MatchDto {
  id: string;
  gameWeekId: string;
  homeTeam: string;
  awayTeam: string;
  kickoffTime: string;
  stage: string;
  stageMultiplier: number;
  homeScore: number | null;
  awayScore: number | null;
  isFinished: boolean;
}

export interface MatchWithPredictionDto extends MatchDto {
  myPrediction: UserPredictionDto | null;
}

export interface UserPredictionDto {
  id: string;
  homeScore: number;
  awayScore: number;
  pointsEarned: number | null;
}

export interface CreateMatchRequest {
  gameWeekId: string;
  homeTeam: string;
  awayTeam: string;
  kickoffTime: string;
  stage: string;
}

export interface EnterResultRequest {
  homeScore: number;
  awayScore: number;
}

export interface ImportMatchesRequest {
  competition: string;
  matchday?: number;
  season?: number;
  dateFrom?: string;
  dateTo?: string;
}

export interface ImportMatchesResponse {
  imported: number;
  skipped: number;
  details: string[];
}
