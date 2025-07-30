import { Routes } from '@angular/router';
import { LoanListComponent } from './components/loan-list/loan-list.component';
import {HomeComponent} from './components/home/ home.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },           // página inicial
  { path: 'loans', component: LoanListComponent },  // rota de lista de empréstimos
  // futuras rotas:
  // { path: 'another-endpoint', component: AnotherComponent },
];
