import { Component, Injectable, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import { HistoryService } from 'src/app/Services/history.service';

@Injectable()
export class DataSharingService {
    public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
}

declare let M: any;

@Component({
  selector: 'app-excel-load',
  templateUrl: './excel-load.component.html',
  styleUrls: ['./excel-load.component.css']
})
export class ExcelLoadComponent implements OnInit {
  
  token: any;
  selectedFile: File | null = null;

  constructor(private navBarComponent: NavBarComponent,
              private historyService: HistoryService) {}
  
  ngOnInit(): void {

    this.token! = localStorage.getItem("token");

    if (this.token) {
      this.navBarComponent.refreshComponent();
    }

  }

  onDragOver(event: any) {
    event.preventDefault();
    event.stopPropagation();
    // Agrega una clase de estilo para resaltar la zona de arrastre
    document.getElementById('file-drop-area')?.classList.add('drag-over');
  }

  onDragLeave(event: any) {
    event.preventDefault();
    event.stopPropagation();
    // Elimina la clase de estilo cuando el cursor sale de la zona de arrastre
    document.getElementById('file-drop-area')?.classList.remove('drag-over');
  }

  onDrop(event: any) {
    event.preventDefault();
    event.stopPropagation();
    // Elimina la clase de estilo cuando se suelta el archivo
    document.getElementById('file-drop-area')?.classList.remove('drag-over');

    const files = event.dataTransfer.files;
    if (files.length > 0) {
      // Obtén el primer archivo de la lista
      this.selectedFile = files[0];
      //console.log('Archivo seleccionado:', this.selectedFile);
    }
  }

  onFileSelected(event: any){
    this.selectedFile = event.target.files[0];
  }

  uploadFile(){
    if (this.selectedFile) {
      this.historyService.AddDataExcel(this.selectedFile).subscribe((response: any) => {
        if (response.data) {
          M.toast({ html: 'Datos Cargados Correctamente', classes: 'green darken-1'});
          this.selectedFile = null;
        }
      });
    }
  }

}
