import { Component, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ApiService } from '../../core/services/api.service';
import { PredictionDto } from '../../shared/models/prediction.model';

@Component({
  selector: 'app-my-predictions',
  imports: [DatePipe],
  template: `
    <h1 class="text-xl font-bold mb-4">My Predictions</h1>
    @if (predictions().length === 0) {
      <p class="text-gray-500 text-center py-8">No predictions yet.</p>
    }
    @for (pred of predictions(); track pred.id) {
      <div class="bg-white rounded-lg shadow p-3 mb-2">
        <div class="flex justify-between items-center text-xs text-gray-500 mb-1">
          <span>{{ pred.match?.stage }}</span>
          <span>{{ pred.match?.kickoffTime | date:'MMM d, HH:mm' }}</span>
        </div>
        <div class="flex items-center justify-between">
          <div class="flex-1">
            <p class="text-sm font-medium">{{ pred.match?.homeTeam }} vs {{ pred.match?.awayTeam }}</p>
          </div>
          <div class="text-center">
            <span class="bg-indigo-100 text-indigo-800 px-2 py-1 rounded text-sm font-bold">
              {{ pred.homeScore }} - {{ pred.awayScore }}
            </span>
          </div>
        </div>
        @if (pred.match?.isFinished) {
          <div class="flex justify-between mt-2 text-xs">
            <span class="text-gray-600">Result: {{ pred.match?.homeScore }} - {{ pred.match?.awayScore }}</span>
            <span class="font-bold" [class]="pred.pointsEarned! > 0 ? 'text-green-600' : 'text-red-500'">
              {{ pred.pointsEarned }} pts
            </span>
          </div>
        }
      </div>
    }
  `
})
export class MyPredictionsComponent implements OnInit {
  predictions = signal<PredictionDto[]>([]);

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.api.getMyPredictions().subscribe(p => this.predictions.set(p));
  }
}
