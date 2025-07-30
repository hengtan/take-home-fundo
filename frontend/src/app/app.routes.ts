import { Routes } from '@angular/router';
import { LoanListComponent } from './components/loan-list/loan-list.component';
import {HomeComponent} from './components/home/ home.component';
import {LoanDetailComponent} from './components/loan-detail/loan-detail.component';
import {CreateLoanComponent} from './components/create-loan/create-loan.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },           // página inicial
  { path: 'loans', component: LoanListComponent },  // rota de lista de empréstimos
  { path: 'loan-detail', component: LoanDetailComponent }, // acesso via input
  { path: 'create-loan', component: CreateLoanComponent }
];
