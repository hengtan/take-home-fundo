import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { switchMap } from 'rxjs';
import { AuthService } from "../../services/auth.service";
import { Loan, LoanService, LoanDetailsDto, HistoryDto } from "../../services/loan.service";
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-loan-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loan-list.component.html'
})
export class LoanListComponent implements OnInit {
  loans: Loan[] = [];
  selectedLoanId: string | null = null;
  selectedLoan: LoanDetailsDto | null = null;
  loadingDetail = false;

  showHistoryModal = false;
  historyItems: HistoryDto[] = [];
  loadingHistory = false;

  constructor(
    private authService: AuthService,
    private loanService: LoanService
  ) {}

  ngOnInit(): void {
    this.authService.login()
      .pipe(
        switchMap(() => this.loanService.getAllLoans())
      )
      .subscribe({
        next: (data: Loan[]) => {
          this.loans = data;
        },
        error: (err) => {
          console.error('❌ Error fetching loans:', err);
        }
      });
  }

  onRowClick(loan: Loan) {
    if (this.selectedLoanId === loan.id) {
      this.selectedLoanId = null;
      this.selectedLoan = null;
    } else {
      this.selectedLoanId = loan.id;
      this.selectedLoan = null;
      this.loadingDetail = true;
      this.loanService.getLoanById(loan.id).subscribe({
        next: (detail) => {
          this.selectedLoan = detail;
          this.loadingDetail = false;
        },
        error: (_) => {
          this.selectedLoan = null;
          this.loadingDetail = false;
        }
      });
    }
  }
}
