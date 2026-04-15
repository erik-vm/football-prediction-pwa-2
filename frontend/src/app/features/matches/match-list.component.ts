import { Component, OnInit, signal, computed } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ApiService } from '../../core/services/api.service';
import { TournamentDto, MatchWithPredictionDto } from '../../shared/models/tournament.model';
import { CreatePredictionRequest, UpdatePredictionRequest } from '../../shared/models/prediction.model';

@Component({
  selector: 'app-match-list',
  imports: [FormsModule, DatePipe],
  template: `
    <div class="flex items-center justify-between mb-4">
      <h1 class="text-xl font-bold">{{ tournament()?.name || 'Matches' }}</h1>
    </div>

    <!-- Stage filter -->
    <div class="mb-4">
      <select [(ngModel)]="selectedStage" (ngModelChange)="onStageChange()" class="w-full border rounded-lg px-3 py-2 text-sm bg-white">
        <option value="">All Stages</option>
        <option value="GROUP_STAGE">League Stage</option>
        <option value="ROUND_OF_16">Round of 16</option>
        <option value="QUARTER_FINALS">Quarter-Finals</option>
        <option value="SEMI_FINALS">Semi-Finals</option>
        <option value="FINAL">Final</option>
      </select>
    </div>

    <!-- View filter tabs -->
    <div class="flex gap-1 mb-4 text-sm">
      <button (click)="viewFilter = 'upcoming'; applyFilter()"
        [class]="viewFilter === 'upcoming' ? 'bg-indigo-600 text-white px-3 py-1 rounded-full' : 'bg-gray-200 px-3 py-1 rounded-full'">
        Upcoming
      </button>
      <button (click)="viewFilter = 'finished'; applyFilter()"
        [class]="viewFilter === 'finished' ? 'bg-indigo-600 text-white px-3 py-1 rounded-full' : 'bg-gray-200 px-3 py-1 rounded-full'">
        Finished
      </button>
      <button (click)="viewFilter = 'all'; applyFilter()"
        [class]="viewFilter === 'all' ? 'bg-indigo-600 text-white px-3 py-1 rounded-full' : 'bg-gray-200 px-3 py-1 rounded-full'">
        All
      </button>
    </div>

    @if (filteredMatches().length === 0) {
      <p class="text-gray-500 text-center py-8">No matches found.</p>
    }

    @for (match of filteredMatches(); track match.id) {
      <div class="bg-white rounded-lg shadow mb-3 overflow-hidden">
        <!-- Match header -->
        <div class="px-4 pt-3 flex justify-between items-center text-xs text-gray-500">
          <span class="bg-gray-100 px-2 py-0.5 rounded">{{ formatStage(match.stage) }}</span>
          <span>{{ match.kickoffTime | date:'EEE, MMM d - HH:mm' }}</span>
        </div>

        <!-- Match score / teams -->
        <div class="px-4 py-3">
          <div class="flex items-center justify-between">
            <span class="font-medium flex-1 text-right pr-3 text-sm">{{ match.homeTeam }}</span>
            @if (match.isFinished) {
              <span class="bg-gray-800 text-white px-3 py-1 rounded font-bold text-sm min-w-[60px] text-center">
                {{ match.homeScore }} - {{ match.awayScore }}
              </span>
            } @else if (isLocked(match)) {
              <span class="bg-gray-300 text-gray-600 px-3 py-1 rounded text-xs min-w-[60px] text-center">
                LIVE
              </span>
            } @else {
              <span class="bg-indigo-50 text-indigo-400 px-3 py-1 rounded text-xs min-w-[60px] text-center">
                vs
              </span>
            }
            <span class="font-medium flex-1 pl-3 text-sm">{{ match.awayTeam }}</span>
          </div>

          @if (match.stageMultiplier > 1) {
            <p class="text-xs text-indigo-500 text-center mt-1">x{{ match.stageMultiplier }} points</p>
          }
        </div>

        <!-- Prediction section -->
        <div class="border-t px-4 py-2 bg-gray-50">
          @if (match.myPrediction) {
            <!-- Has prediction -->
            <div class="flex items-center justify-between">
              <span class="text-xs text-gray-500">My prediction:</span>
              <div class="flex items-center gap-2">
                @if (!isLocked(match) && editingMatch() === match.id) {
                  <!-- Edit mode -->
                  <input type="number" min="0" max="9" [(ngModel)]="editHome"
                    class="w-10 border rounded text-center py-1 text-sm">
                  <span>-</span>
                  <input type="number" min="0" max="9" [(ngModel)]="editAway"
                    class="w-10 border rounded text-center py-1 text-sm">
                  <button (click)="saveEdit(match)" class="text-green-600 text-xs font-bold">Save</button>
                  <button (click)="editingMatch.set(null)" class="text-gray-400 text-xs">Cancel</button>
                } @else {
                  <span class="bg-indigo-100 text-indigo-800 px-2 py-0.5 rounded font-bold text-sm">
                    {{ match.myPrediction.homeScore }} - {{ match.myPrediction.awayScore }}
                  </span>
                  @if (!isLocked(match)) {
                    <button (click)="startEdit(match)" class="text-indigo-500 text-xs hover:underline">Edit</button>
                  }
                  @if (match.isFinished && match.myPrediction.pointsEarned !== null) {
                    <span class="font-bold text-sm"
                      [class]="match.myPrediction.pointsEarned > 0 ? 'text-green-600' : 'text-red-400'">
                      +{{ match.myPrediction.pointsEarned }} pts
                    </span>
                  }
                }
              </div>
            </div>
          } @else if (!isLocked(match)) {
            <!-- No prediction yet - show input -->
            <div class="flex items-center justify-between">
              <span class="text-xs text-gray-500">Your prediction:</span>
              <div class="flex items-center gap-1">
                <input type="number" min="0" max="9"
                  [value]="scores[match.id]?.home ?? ''"
                  (input)="setScore(match.id, 'home', $event)"
                  class="w-10 border rounded text-center py-1 text-sm">
                <span class="text-gray-400">-</span>
                <input type="number" min="0" max="9"
                  [value]="scores[match.id]?.away ?? ''"
                  (input)="setScore(match.id, 'away', $event)"
                  class="w-10 border rounded text-center py-1 text-sm">
                <button (click)="submitPrediction(match.id)"
                  class="bg-indigo-600 text-white text-xs px-3 py-1 rounded hover:bg-indigo-700 ml-1">
                  Predict
                </button>
              </div>
            </div>
          } @else {
            <!-- Locked, no prediction -->
            <p class="text-xs text-gray-400 text-center">No prediction submitted</p>
          }
        </div>
      </div>
    }

    @if (message()) {
      <div class="fixed bottom-4 left-1/2 -translate-x-1/2 bg-green-600 text-white px-4 py-2 rounded-lg shadow-lg text-sm z-50">
        {{ message() }}
      </div>
    }
  `
})
export class MatchListComponent implements OnInit {
  tournament = signal<TournamentDto | null>(null);
  allMatches = signal<MatchWithPredictionDto[]>([]);
  filteredMatches = signal<MatchWithPredictionDto[]>([]);
  message = signal('');
  editingMatch = signal<string | null>(null);
  editHome = 0;
  editAway = 0;
  selectedStage = '';
  viewFilter: 'upcoming' | 'finished' | 'all' = 'upcoming';
  scores: Record<string, { home: number | null; away: number | null }> = {};

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.api.getActiveTournament().subscribe({
      next: (t) => {
        this.tournament.set(t);
        this.loadMatches();
      },
      error: () => this.showMessage('No active tournament found')
    });
  }

  loadMatches(): void {
    const t = this.tournament();
    if (!t) return;

    const stage = this.selectedStage || undefined;
    this.api.getMatchesByTournament(t.id, stage).subscribe(matches => {
      this.allMatches.set(matches);
      this.applyFilter();
    });
  }

  onStageChange(): void {
    this.loadMatches();
  }

  applyFilter(): void {
    const now = new Date().toISOString();
    let matches = this.allMatches();

    if (this.viewFilter === 'upcoming') {
      matches = matches.filter(m => !m.isFinished);
    } else if (this.viewFilter === 'finished') {
      matches = matches.filter(m => m.isFinished).reverse();
    }

    this.filteredMatches.set(matches);
  }

  isLocked(match: MatchWithPredictionDto): boolean {
    return new Date(match.kickoffTime) <= new Date() || match.isFinished;
  }

  formatStage(stage: string): string {
    const map: Record<string, string> = {
      'GROUP_STAGE': 'League Stage',
      'ROUND_OF_16': 'Round of 16',
      'QUARTER_FINALS': 'Quarter-Finals',
      'SEMI_FINALS': 'Semi-Finals',
      'FINAL': 'Final'
    };
    return map[stage] || stage;
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
      next: () => { this.showMessage('Prediction saved!'); this.loadMatches(); },
      error: (err) => this.showMessage(err.error?.error || 'Failed to save')
    });
  }

  startEdit(match: MatchWithPredictionDto): void {
    this.editingMatch.set(match.id);
    this.editHome = match.myPrediction!.homeScore;
    this.editAway = match.myPrediction!.awayScore;
  }

  saveEdit(match: MatchWithPredictionDto): void {
    const req: UpdatePredictionRequest = { homeScore: this.editHome, awayScore: this.editAway };
    this.api.updatePrediction(match.myPrediction!.id, req).subscribe({
      next: () => { this.editingMatch.set(null); this.showMessage('Prediction updated!'); this.loadMatches(); },
      error: (err) => this.showMessage(err.error?.error || 'Failed to update')
    });
  }

  private showMessage(msg: string): void {
    this.message.set(msg);
    setTimeout(() => this.message.set(''), 3000);
  }
}
