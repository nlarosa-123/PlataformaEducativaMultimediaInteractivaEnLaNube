import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DiaryComponent } from './diary/diary.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'diary', component: DiaryComponent }
];
