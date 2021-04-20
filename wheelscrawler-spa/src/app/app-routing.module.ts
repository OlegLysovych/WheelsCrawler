import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CarListDetailedComponent } from './car-list-detailed/car-list-detailed.component';
import { CarListComponent } from './car-list/car-list.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { SearchListComponent } from './search-list/search-list.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'cars', component: CarListComponent, canActivate: [AuthGuard]},
      {path: 'cars/:id', component: CarListDetailedComponent},
      {path: 'search', component: SearchListComponent},
      {path: 'dashboard', component: DashboardComponent},    
    ]
  },
  {path: '**', component: HomeComponent, pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
