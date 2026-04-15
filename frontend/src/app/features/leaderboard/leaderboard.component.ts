import { Component, OnInit, signal } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { LeaderboardEntryDto } from '../../shared/models/leaderboard.model';

@Component({
  selector: 'app-leaderboard',
  imports: [],
  template: `
    <h1 class="text-xl font-bold mb-4">Leaderboard</h1>
    @if (entries().length === 0) {
      <p class="text-gray-500 text-center py-8">No data yet.</p>
    } @else {
      <div class="bg-white rounded-lg shadow overflow-hidden">
        <table class="w-full text-sm">
          <thead class="bg-indigo-50">
            <tr>
              <th class="px-3 py-2 text-left">#</th>
              <th class="px-3 py-2 text-left">Player</th>
              <th class="px-3 py-2 text-right">Points</th>
              <th class="px-3 py-2 text-right">Exact</th>
              <th class="px-3 py-2 text-right">Played</th>
            </tr>
          </thead>
          <tbody>
            @for (entry of entries(); track entry.userId) {
              <tr class="border-t hover:bg-gray-50"
                [class.bg-yellow-50]="entry.rank === 1"
                [class.bg-gray-50]="entry.rank === 2"
                [class.bg-orange-50]="entry.rank === 3">
                <td class="px-3 py-2 font-bold">
                  @if (entry.rank <= 3) {
                    {{ ['', '🥇', '🥈', '🥉'][entry.rank] }}
                  } @else {
                    {{ entry.rank }}
                  }
                </td>
                <td class="px-3 py-2 font-medium">{{ entry.username }}</td>
                <td class="px-3 py-2 text-right font-bold text-indigo-600">{{ entry.totalPoints }}</td>
                <td class="px-3 py-2 text-right">{{ entry.exactScores }}</td>
                <td class="px-3 py-2 text-right">{{ entry.predictionsMade }}</td>
              </tr>
            }
          </tbody>
        </table>
      </div>
    }
  `
})
export class LeaderboardComponent implements OnInit {
  entries = signal<LeaderboardEntryDto[]>([]);

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.api.getOverallLeaderboard().subscribe(e => this.entries.set(e));
  }
}
