import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ApiService } from '../../core/services/api.service';
import { TournamentDto, GameWeekDto, MatchDto } from '../../shared/models/tournament.model';

@Component({
  selector: 'app-admin',
  imports: [FormsModule, DatePipe],
  template: `
    <h1 class="text-xl font-bold mb-4">Admin Panel</h1>

    <div class="space-y-6">
      <!-- Tournament -->
      <section class="bg-white rounded-lg shadow p-4">
        <h2 class="font-bold mb-3">Tournaments</h2>
        @for (t of tournaments(); track t.id) {
          <div class="flex justify-between items-center border-b py-2 text-sm">
            <span [class.font-bold]="t.isActive">{{ t.name }} ({{ t.season }})</span>
            <button (click)="selectTournament(t)" class="text-indigo-600 text-xs hover:underline">Select</button>
          </div>
        }
        <div class="mt-3 space-y-2">
          <input [(ngModel)]="newTournament.name" placeholder="Name" class="w-full border rounded px-2 py-1 text-sm">
          <input [(ngModel)]="newTournament.season" placeholder="Season" class="w-full border rounded px-2 py-1 text-sm">
          <div class="flex gap-2">
            <input type="date" [(ngModel)]="newTournament.startDate" class="flex-1 border rounded px-2 py-1 text-sm">
            <input type="date" [(ngModel)]="newTournament.endDate" class="flex-1 border rounded px-2 py-1 text-sm">
          </div>
          <button (click)="createTournament()" class="bg-indigo-600 text-white text-sm px-3 py-1 rounded">Create</button>
        </div>
      </section>

      <!-- Game Weeks -->
      @if (selectedTournament()) {
        <section class="bg-white rounded-lg shadow p-4">
          <h2 class="font-bold mb-3">Game Weeks - {{ selectedTournament()!.name }}</h2>
          @for (gw of gameWeeks(); track gw.id) {
            <div class="flex justify-between items-center border-b py-2 text-sm">
              <span>Week {{ gw.weekNumber }}</span>
              <button (click)="selectGameWeek(gw)" class="text-indigo-600 text-xs hover:underline">Select</button>
            </div>
          }
          <div class="mt-3 flex gap-2">
            <input type="number" [(ngModel)]="newWeekNumber" placeholder="Week #" class="w-20 border rounded px-2 py-1 text-sm">
            <input type="date" [(ngModel)]="newWeekStart" class="flex-1 border rounded px-2 py-1 text-sm">
            <input type="date" [(ngModel)]="newWeekEnd" class="flex-1 border rounded px-2 py-1 text-sm">
            <button (click)="createGameWeek()" class="bg-indigo-600 text-white text-sm px-3 py-1 rounded">Add</button>
          </div>
        </section>
      }

      <!-- Matches -->
      @if (selectedGameWeek()) {
        <section class="bg-white rounded-lg shadow p-4">
          <h2 class="font-bold mb-3">Matches - Week {{ selectedGameWeek()!.weekNumber }}</h2>
          @for (m of matches(); track m.id) {
            <div class="border-b py-2">
              <div class="flex justify-between text-sm">
                <span>{{ m.homeTeam }} vs {{ m.awayTeam }}</span>
                <span class="text-gray-500">{{ m.kickoffTime | date:'MMM d HH:mm' }}</span>
              </div>
              @if (!m.isFinished) {
                <div class="flex gap-2 mt-1 items-center">
                  <input type="number" min="0" [(ngModel)]="resultScores[m.id].home" class="w-12 border rounded text-center text-sm py-1">
                  <span>-</span>
                  <input type="number" min="0" [(ngModel)]="resultScores[m.id].away" class="w-12 border rounded text-center text-sm py-1">
                  <button (click)="enterResult(m.id)" class="bg-green-600 text-white text-xs px-2 py-1 rounded">Enter Result</button>
                </div>
              } @else {
                <p class="text-xs text-green-600 mt-1">Final: {{ m.homeScore }} - {{ m.awayScore }}</p>
              }
            </div>
          }
          <div class="mt-3 space-y-2">
            <div class="flex gap-2">
              <input [(ngModel)]="newMatch.homeTeam" placeholder="Home Team" class="flex-1 border rounded px-2 py-1 text-sm">
              <input [(ngModel)]="newMatch.awayTeam" placeholder="Away Team" class="flex-1 border rounded px-2 py-1 text-sm">
            </div>
            <div class="flex gap-2">
              <input type="datetime-local" [(ngModel)]="newMatch.kickoffTime" class="flex-1 border rounded px-2 py-1 text-sm">
              <select [(ngModel)]="newMatch.stage" class="border rounded px-2 py-1 text-sm">
                <option value="GROUP_STAGE">Group Stage</option>
                <option value="ROUND_OF_16">Round of 16</option>
                <option value="QUARTER_FINALS">Quarter Finals</option>
                <option value="SEMI_FINALS">Semi Finals</option>
                <option value="FINAL">Final</option>
              </select>
            </div>
            <button (click)="createMatch()" class="bg-indigo-600 text-white text-sm px-3 py-1 rounded">Add Match</button>
          </div>
        </section>
      }
    </div>

    @if (message()) {
      <div class="fixed bottom-4 left-1/2 -translate-x-1/2 bg-green-600 text-white px-4 py-2 rounded-lg shadow-lg text-sm">
        {{ message() }}
      </div>
    }
  `
})
export class AdminComponent implements OnInit {
  tournaments = signal<TournamentDto[]>([]);
  selectedTournament = signal<TournamentDto | null>(null);
  gameWeeks = signal<GameWeekDto[]>([]);
  selectedGameWeek = signal<GameWeekDto | null>(null);
  matches = signal<MatchDto[]>([]);
  message = signal('');

  newTournament = { name: '', season: '', startDate: '', endDate: '' };
  newWeekNumber = 1;
  newWeekStart = '';
  newWeekEnd = '';
  newMatch = { homeTeam: '', awayTeam: '', kickoffTime: '', stage: 'GROUP_STAGE' };
  resultScores: Record<string, { home: number; away: number }> = {};

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.loadTournaments();
  }

  loadTournaments(): void {
    this.api.getTournaments().subscribe(t => this.tournaments.set(t));
  }

  selectTournament(t: TournamentDto): void {
    this.selectedTournament.set(t);
    this.selectedGameWeek.set(null);
    this.matches.set([]);
    this.api.getGameWeeks(t.id).subscribe(gw => this.gameWeeks.set(gw));
  }

  selectGameWeek(gw: GameWeekDto): void {
    this.selectedGameWeek.set(gw);
    this.api.getMatchesByGameWeek(gw.id).subscribe(m => {
      this.matches.set(m);
      m.forEach(match => {
        if (!this.resultScores[match.id]) {
          this.resultScores[match.id] = { home: 0, away: 0 };
        }
      });
    });
  }

  createTournament(): void {
    this.api.createTournament({
      ...this.newTournament,
      isActive: true
    } as any).subscribe({
      next: () => { this.loadTournaments(); this.showMessage('Tournament created'); },
      error: (err) => this.showMessage(err.error?.error || 'Failed')
    });
  }

  createGameWeek(): void {
    const t = this.selectedTournament();
    if (!t) return;
    this.api.createGameWeek({
      tournamentId: t.id,
      weekNumber: this.newWeekNumber,
      startDate: this.newWeekStart,
      endDate: this.newWeekEnd
    }).subscribe({
      next: () => { this.selectTournament(t); this.showMessage('Game week created'); },
      error: (err) => this.showMessage(err.error?.error || 'Failed')
    });
  }

  createMatch(): void {
    const gw = this.selectedGameWeek();
    if (!gw) return;
    this.api.createMatch({
      gameWeekId: gw.id,
      homeTeam: this.newMatch.homeTeam,
      awayTeam: this.newMatch.awayTeam,
      kickoffTime: new Date(this.newMatch.kickoffTime).toISOString(),
      stage: this.newMatch.stage
    }).subscribe({
      next: () => { this.selectGameWeek(gw); this.showMessage('Match created'); },
      error: (err) => this.showMessage(err.error?.error || 'Failed')
    });
  }

  enterResult(matchId: string): void {
    const s = this.resultScores[matchId];
    this.api.enterResult(matchId, { homeScore: s.home, awayScore: s.away }).subscribe({
      next: () => {
        this.selectGameWeek(this.selectedGameWeek()!);
        this.showMessage('Result entered, points calculated');
      },
      error: (err) => this.showMessage(err.error?.error || 'Failed')
    });
  }

  private showMessage(msg: string): void {
    this.message.set(msg);
    setTimeout(() => this.message.set(''), 3000);
  }
}
