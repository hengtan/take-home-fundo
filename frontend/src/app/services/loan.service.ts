// src/app/services/loan.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Loan {
  id: string;
  applicantName: string;
  amount: number;
  currentBalance: number;
  status: string;
}

export interface HistoryDto {
  description: string;
  created: string;
}

export interface LoanDetailsDto {
  id: string;
  applicantName: string;
  amount: number;
  currentBalance: number;
  status: string;
  histories: HistoryDto[];
}

export interface HistoryDto {
  description: string;
}

@Injectable({ providedIn: 'root' })
export class LoanService {
  private baseUrl = 'http://localhost:8080/loans';

  constructor(private http: HttpClient) {}

  getAllLoans(): Observable<Loan[]> {
    return this.http.get<Loan[]>(this.baseUrl);
  }

  getLoanById(id: string): Observable<LoanDetailsDto> {
    return this.http.get<LoanDetailsDto>(`${this.baseUrl}/${id}`);
  }

  getLoanHistory(loanId: string): Observable<HistoryDto[]> {
    return this.http.get<HistoryDto[]>(`${this.baseUrl}/loan-history/${loanId}`);
  }
}
