import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ApiService } from '../../core/services/api.service';
import { MatchDto } from '../../shared/models/tournament.model';
import { PredictionDto, CreatePredictionRequest } from '../../shared/models/prediction.model';

@Component({
  selector: 'app-match-list',
  imports: [FormsModule, DatePipe],
  template: `
    <h1 class="text-xl font-bold mb-4">Upcoming Matches</h1>
    @if (matches().length === 0) {
      <p class="text-gray-500 text-center py-8">No upcoming matches.</p>
    }
    @for (match of matches(); track match.id) {
      <div class="bg-white rounded-lg shadow p-4 mb-3">
        <div class="flex justify-between items-center text-xs text-gray-500 mb-2">
          <span>{{ match.stage }}</span>
          <span>{{ match.kickoffTime | date:'MMM d, HH:mm' }}</span>
        </div>
        <div class="flex items-center justify-between">
          <span class="font-medium flex-1 text-right pr-2">{{ match.homeTeam }}</span>
          @if (getPrediction(match.id); as pred) {
            <div class="flex items-center gap-1">
              <span class="bg-green-100 text-green-800 px-2 py-1 rounded text-sm font-bold">
                {{ pred.homeScore }} - {{ pred.awayScore }}
              </span>
            </div>
          } @else {
            <div class="flex items-center gap-1">
              <input type="number" min="0" max="9"
                [value]="scores[match.id]?.home ?? ''"
                (input)="setScore(match.id, 'home', $event)"
                class="w-10 border rounded text-center py-1">
              <span class="text-gray-400">-</span>
              <input type="number" min="0" max="9"
                [value]="scores[match.id]?.away ?? ''"
                (input)="setScore(match.id, 'away', $event)"
                class="w-10 border rounded text-center py-1">
              <button (click)="submitPrediction(match.id)"
                class="bg-indigo-600 text-white text-xs px-2 py-1 rounded hover:bg-indigo-700 ml-1">
                OK
              </button>
            </div>
          }
          <span class="font-medium flex-1 pl-2">{{ match.awayTeam }}</span>
        </div>
        @if (match.stageMultiplier > 1) {
          <p class="text-xs text-indigo-600 mt-1 text-center">x{{ match.stageMultiplier }} points</p>
        }
      </div>
    }
    @if (message()) {
      <div class="fixed bottom-4 left-1/2 -translate-x-1/2 bg-green-600 text-white px-4 py-2 rounded-lg shadow-lg text-sm">
        {{ message() }}
      </div>
    }
  `
})
export class MatchListComponent implements OnInit {
  matches = signal<MatchDto[]>([]);
  predictions = signal<PredictionDto[]>([]);
  message = signal('');
  scores: Record<string, { home: number | null; away: number | null }> = {};

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.api.getUpcomingMatches().subscribe(m => this.matches.set(m));
    this.api.getMyPredictions().subscribe(p => this.predictions.set(p));
  }

  getPrediction(matchId: string): PredictionDto | undefined {
    return this.predictions().find(p => p.matchId === matchId);
  }

  setScore(matchId: string, team: 'home' | 'away', event: Event): void {
    const val = parseInt((event.target as HTMLInputElement).value);
    if (!this.scores[matchId]) this.scores[matchId] = { home: null, away: null };
    this.scores[matchId][team] = isNaN(val) ? null : val;
  }

  submitPrediction(matchId: string): void {
    const s = this.scores[matchId];
    if (s?.home == null || s?.away == null) return;

    const req: CreatePredictionRequest = { matchId, homeScore: s.home, awayScore: s.away };
    this.api.createPrediction(req).subscribe({
      next: () => {
        this.showMessage('Prediction saved!');
        this.loadData();
      },
      error: (err) => this.showMessage(err.error?.error || 'Failed to save')
    });
  }

  private showMessage(msg: string): void {
    this.message.set(msg);
    setTimeout(() => this.message.set(''), 3000);
  }
}
