import { MatchDto } from './tournament.model';

export interface PredictionDto {
  id: string;
  userId: string;
  matchId: string;
  homeScore: number;
  awayScore: number;
  pointsEarned: number | null;
  createdAt: string;
  updatedAt: string;
  match: MatchDto | null;
}

export interface CreatePredictionRequest {
  matchId: string;
  homeScore: number;
  awayScore: number;
}

export interface UpdatePredictionRequest {
  homeScore: number;
  awayScore: number;
}

export interface MatchPredictionDto {
  id: string;
  username: string;
  homeScore: number;
  awayScore: number;
  pointsEarned: number | null;
}
