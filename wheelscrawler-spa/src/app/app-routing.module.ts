import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { CarListDetailedComponent } from './car-list-detailed/car-list-detailed.component';
import { CarListComponent } from './car-list/car-list.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrorsComponent } from './errors/test-errors/test-errors.component';
import { HomeComponent } from './home/home.component';
import { SearchListComponent } from './search-list/search-list.component';
import { AdminGuard } from './_guards/admin.guard';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'cars', component: CarListComponent },
      { path: 'cars/:id', component: CarListDetailedComponent },
      { path: 'search', component: SearchListComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard] },
    ],
  },
  {path: 'errors', component: TestErrorsComponent},    
  {path: 'not-found', component: NotFoundComponent},    
  {path: 'server-error', component: ServerErrorComponent},    
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
