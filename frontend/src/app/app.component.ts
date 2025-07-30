import { Component } from '@angular/core';
import { LoanListComponent } from './components/loan-list/loan-list.component'; // ajuste caminho se necessário
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet
    // ,LoanListComponent    // Se estiver usando diretamente (mas no seu caso agora é via rota)
  ],
  templateUrl: './app.component.html'
})
export class AppComponent {}
