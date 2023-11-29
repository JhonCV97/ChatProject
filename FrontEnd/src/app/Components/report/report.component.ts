import { Component, OnInit } from '@angular/core';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { HistoryService } from 'src/app/Services/history.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {

  token: any;
  report: any;

  constructor(private navBarComponent: NavBarComponent,
              private historyService: HistoryService) {}

  ngOnInit(): void {
    this.token! = localStorage.getItem("token");

    if (this.token) {
      this.navBarComponent.refreshComponent();
    }

    this.reportHistory();

  }

  reportHistory(){
    this.historyService.ReportHistory().subscribe((response: any) => {
      this.report = response.data
    }) 
  }

}
