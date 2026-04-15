import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { TournamentDto, GameWeekDto, MatchDto, MatchWithPredictionDto, CreateMatchRequest, EnterResultRequest, ImportMatchesRequest, ImportMatchesResponse } from '../../shared/models/tournament.model';
import { PredictionDto, CreatePredictionRequest, UpdatePredictionRequest, MatchPredictionDto } from '../../shared/models/prediction.model';
import { LeaderboardEntryDto, WeeklyLeaderboardEntryDto, UserStatsDto } from '../../shared/models/leaderboard.model';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private readonly api = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getTournaments(): Observable<TournamentDto[]> {
    return this.http.get<TournamentDto[]>(`${this.api}/admin/tournaments`);
  }

  createTournament(data: Partial<TournamentDto>): Observable<TournamentDto> {
    return this.http.post<TournamentDto>(`${this.api}/admin/tournaments`, data);
  }

  updateTournament(id: string, data: Partial<TournamentDto>): Observable<TournamentDto> {
    return this.http.put<TournamentDto>(`${this.api}/admin/tournaments/${id}`, data);
  }

  deleteTournament(id: string): Observable<void> {
    return this.http.delete<void>(`${this.api}/admin/tournaments/${id}`);
  }

  getGameWeeks(tournamentId: string): Observable<GameWeekDto[]> {
    return this.http.get<GameWeekDto[]>(`${this.api}/admin/tournaments/${tournamentId}/gameweeks`);
  }

  createGameWeek(data: Partial<GameWeekDto> & { tournamentId: string }): Observable<GameWeekDto> {
    return this.http.post<GameWeekDto>(`${this.api}/admin/gameweeks`, data);
  }

  importMatches(gameWeekId: string, data: ImportMatchesRequest): Observable<ImportMatchesResponse> {
    return this.http.post<ImportMatchesResponse>(`${this.api}/admin/gameweeks/${gameWeekId}/import-matches`, data);
  }

  getActiveTournament(): Observable<TournamentDto> {
    return this.http.get<TournamentDto>(`${this.api}/tournaments/active`);
  }

  getMatchesByTournament(tournamentId: string, stage?: string): Observable<MatchWithPredictionDto[]> {
    const params = stage ? `?stage=${stage}` : '';
    return this.http.get<MatchWithPredictionDto[]>(`${this.api}/matches/tournament/${tournamentId}${params}`);
  }

  getUpcomingMatches(): Observable<MatchDto[]> {
    return this.http.get<MatchDto[]>(`${this.api}/matches/upcoming`);
  }

  getMatchesByGameWeek(gameWeekId: string): Observable<MatchDto[]> {
    return this.http.get<MatchDto[]>(`${this.api}/matches/gameweek/${gameWeekId}`);
  }

  createMatch(data: CreateMatchRequest): Observable<MatchDto> {
    return this.http.post<MatchDto>(`${this.api}/admin/matches`, data);
  }

  enterResult(matchId: string, data: EnterResultRequest): Observable<MatchDto> {
    return this.http.put<MatchDto>(`${this.api}/admin/matches/${matchId}/result`, data);
  }

  getMyPredictions(): Observable<PredictionDto[]> {
    return this.http.get<PredictionDto[]>(`${this.api}/predictions/my`);
  }

  createPrediction(data: CreatePredictionRequest): Observable<PredictionDto> {
    return this.http.post<PredictionDto>(`${this.api}/predictions`, data);
  }

  updatePrediction(id: string, data: UpdatePredictionRequest): Observable<PredictionDto> {
    return this.http.put<PredictionDto>(`${this.api}/predictions/${id}`, data);
  }

  getMatchPredictions(matchId: string): Observable<MatchPredictionDto[]> {
    return this.http.get<MatchPredictionDto[]>(`${this.api}/predictions/match/${matchId}`);
  }

  getOverallLeaderboard(): Observable<LeaderboardEntryDto[]> {
    return this.http.get<LeaderboardEntryDto[]>(`${this.api}/leaderboard/overall`);
  }

  getWeeklyLeaderboard(gameWeekId: string): Observable<WeeklyLeaderboardEntryDto[]> {
    return this.http.get<WeeklyLeaderboardEntryDto[]>(`${this.api}/leaderboard/weekly/${gameWeekId}`);
  }

  getUserStats(userId: string): Observable<UserStatsDto> {
    return this.http.get<UserStatsDto>(`${this.api}/leaderboard/user/${userId}`);
  }
}
